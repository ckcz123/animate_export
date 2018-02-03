using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace animate_export
{
    unsafe class BitmapWrapper
    {
        private readonly Bitmap bmp;
        private readonly BitmapData bmpData;

        private readonly byte* scan0;
        private readonly int byteCount;

        public BitmapWrapper(Bitmap bitmap)
        {
            bmp = bitmap;
            bmpData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            scan0 = (byte*)bmpData.Scan0;
            // byteCount = bmpData.Stride / bmpData.Width;
            byteCount = bmpData.PixelFormat.ToString().IndexOf("32") > 0 ? 4 : 3;
        }
        public Bitmap UnWrapper()
        {
            bmp.UnlockBits(bmpData);
            return bmp;
        }
        public void SetPixel(Point point, Color color)
        {
            int offset = point.X * byteCount + point.Y * bmpData.Stride;
            scan0[offset] = color.B;
            scan0[offset + 1] = color.G;
            scan0[offset + 2] = color.R;
            if (byteCount == 4)
                scan0[offset + 3] = color.A;
        }
        public Color GetPixel(Point point)
        {
            int offset = point.X * byteCount + point.Y * bmpData.Stride;
            Color color = Color.FromArgb(
                scan0[offset + 2],
                scan0[offset + 1],
                scan0[offset]
            );
            if (byteCount == 4)
                color = Color.FromArgb(scan0[offset + 3], color);
            return color;
        }
    }
}
