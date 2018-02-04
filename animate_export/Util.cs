using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace animate_export
{
    class Util
    {
        public static string readString(IntPtr address)
        {
            IntPtr name = new IntPtr(Marshal.ReadInt32(address));
            int len = 0;
            while (Marshal.ReadByte(name, len) != 0) len++;
            byte[] bytes = new byte[len];
            for (int i = 0; i < len; i++) bytes[i] = Marshal.ReadByte(name, i);
            return Encoding.UTF8.GetString(bytes);
        }

        public static Color addHue(Color color, int hue)
        {
            int a = color.A;
            float h = color.GetHue();
            float s = color.GetSaturation();
            float b = color.GetBrightness();

            h += hue;
            if (h >= 360) h -= 360;

            float fMax, fMid, fMin;
            int iSextant, iMax, iMid, iMin;

            if (0.5 < b)
            {
                fMax = b - (b * s) + s;
                fMin = b + (b * s) - s;
            }
            else
            {
                fMax = b + (b * s);
                fMin = b - (b * s);
            }

            iSextant = (int)Math.Floor(h / 60f);
            if (300f <= h)
            {
                h -= 360f;
            }
            h /= 60f;
            h -= 2f * (float)Math.Floor(((iSextant + 1f) % 6f) / 2f);
            if (0 == iSextant % 2)
            {
                fMid = h * (fMax - fMin) + fMin;
            }
            else
            {
                fMid = fMin - h * (fMax - fMin);
            }

            iMax = Convert.ToInt32(fMax * 255);
            iMid = Convert.ToInt32(fMid * 255);
            iMin = Convert.ToInt32(fMin * 255);

            Color nColor;
            switch (iSextant)
            {
                case 1:
                    nColor = Color.FromArgb(a, iMid, iMax, iMin);
                    break;
                case 2:
                    nColor = Color.FromArgb(a, iMin, iMax, iMid);
                    break;
                case 3:
                    nColor = Color.FromArgb(a, iMin, iMid, iMax);
                    break;
                case 4:
                    nColor = Color.FromArgb(a, iMid, iMin, iMax);
                    break;
                case 5:
                    nColor = Color.FromArgb(a, iMax, iMin, iMid);
                    break;
                default:
                    nColor = Color.FromArgb(a, iMax, iMid, iMin);
                    break;
            }
            return nColor;
        }

        public static string compressPng(Bitmap bitmap, int ratio)
        {
            Bitmap map=new Bitmap(bitmap.Width/ratio, bitmap.Height/ratio);

            Graphics graphics=Graphics.FromImage(map);
            graphics.DrawImage(bitmap, 0, 0, map.Width, map.Height);
            graphics.Dispose();

            MemoryStream stream=new MemoryStream();
            map.Save(stream, ImageFormat.Png);
            byte[] bytes = stream.ToArray();

            string base64 = "data:image/png;base64," + Convert.ToBase64String(bytes);

            stream.Dispose();
            map.Dispose();

            return base64;

        }


    }
}
