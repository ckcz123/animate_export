using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace animate_export
{
    class RGSSReader
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int RGSSEval(IntPtr pScripts);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RGSSInitialize(IntPtr hRgssDll);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr RGSSDefineModule(IntPtr moduleName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr RGSSDefineModuleFunction(IntPtr module, IntPtr funcName, IntPtr funcAddress, int argcount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr OnAnimationsInfoDown(IntPtr obj, IntPtr dataString);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr OnAnimationFrameDataDown(IntPtr obj, IntPtr dataString);

        public IntPtr onAnimationFrameDataDown(IntPtr obj, IntPtr dataString)
        {

            Animation animation = animations[id];

            int frame_max = animation.frame_max;
            IntPtr basePtr = new IntPtr((dataString.ToInt32() - 1) / 2);

            List<Animation.Frame> list = new List<Animation.Frame>();

            for (int n = 0; n < frame_max; n++)
            {

                IntPtr frameData = new IntPtr(Marshal.ReadInt32(IntPtr.Add(basePtr, n * 4)));
                int cell_max = Marshal.ReadInt32(frameData);
                // Console.WriteLine("cell_max: " + cell_max);
                IntPtr cells = new IntPtr(Marshal.ReadInt32(IntPtr.Add(frameData, 4)));

                List<Animation.FrameInfo> infos = new List<Animation.FrameInfo>();

                for (int i = 0; i < cell_max; i++)
                {
                    IntPtr data = new IntPtr(Marshal.ReadInt32(IntPtr.Add(cells, i * 4)));
                    int type = Marshal.ReadInt32(data);
                    int xpos = Marshal.ReadInt32(IntPtr.Add(data, 4));
                    int ypos = Marshal.ReadInt32(IntPtr.Add(data, 8));
                    int zoom = Marshal.ReadInt32(IntPtr.Add(data, 12));
                    int angle = Marshal.ReadInt32(IntPtr.Add(data, 16));
                    bool mirror = Marshal.ReadInt32(IntPtr.Add(data, 20)) > 0;
                    int opacity = Marshal.ReadInt32(IntPtr.Add(data, 24));
                    int blend = Marshal.ReadInt32(IntPtr.Add(data, 28));
                    // Console.WriteLine("[" + type + "," + xpos + "," + ypos + "," + zoom + "," + angle + "," + mirror + "," + opacity + "," + blend);

                    infos.Add(new Animation.FrameInfo(type, xpos, ypos, zoom, angle, mirror, opacity, blend));

                }
                list.Add(new Animation.Frame(cell_max, infos));
            }

            animation.setFrames(list);

            return new IntPtr(4);
        }

        public IntPtr onAnimationsInfoDown(IntPtr obj, IntPtr dataString)
        {
            IntPtr basePtr = new IntPtr((dataString.ToInt32() - 1) / 2);
            int length = Marshal.ReadInt32(basePtr);
            // Console.WriteLine("Length: " + length);
            IntPtr datas = new IntPtr(Marshal.ReadInt32(IntPtr.Add(basePtr, 4)));

            animations = new List<Animation>();

            for (int i = 0; i < length; i++)
            {
                IntPtr data = new IntPtr(Marshal.ReadInt32(IntPtr.Add(datas, i * 4)));
                int id = Marshal.ReadInt32(data);
                if (id == 0) continue;
                string name = Util.readString(IntPtr.Add(data, 4));
                string animation_name = Util.readString(IntPtr.Add(data, 8));
                int animation_hue = Marshal.ReadInt32(IntPtr.Add(data, 12));
                int position = Marshal.ReadInt32(IntPtr.Add(data, 16));
                int frame_max = Marshal.ReadInt32(IntPtr.Add(data, 20));
                // Console.WriteLine("No." + id + ":" + name + "," + animation_name + "," + animation_hue + "," + position + "," + frame_max);

                animations.Add(new Animation(id, name, animation_name, animation_hue, position, frame_max));

            }
            return new IntPtr(4);
        }


        private string rxdataPath;
        private DllInvoke dll;

        private List<Animation> animations;
        private int id;

        private RGSSEval eval;

        private OnAnimationFrameDataDown dataDown;
        private OnAnimationsInfoDown infoDown;

        public RGSSReader()
        {
            infoDown = onAnimationsInfoDown;
            dataDown = onAnimationFrameDataDown;
            rxdataPath = null;
            animations = null;
            id = 0;

            dll = new DllInvoke("rgss.dll");
            ((RGSSInitialize)dll.Invoke("RGSSInitialize", typeof(RGSSInitialize)))(dll.getLib());

            IntPtr rb_define_module = IntPtr.Add(dll.getLib(), 0x20400);
            RGSSDefineModule rbDefineModule =
                (RGSSDefineModule)Marshal.GetDelegateForFunctionPointer(rb_define_module, typeof(RGSSDefineModule));

            IntPtr fux2 = rbDefineModule(Marshal.StringToCoTaskMemAnsi("Fux2"));

            IntPtr rb_define_module_function = IntPtr.Add(dll.getLib(), 0x20B50);
            RGSSDefineModuleFunction rbDefineModuleFunction =
                (RGSSDefineModuleFunction)Marshal.GetDelegateForFunctionPointer(rb_define_module_function,
                    typeof(RGSSDefineModuleFunction));

            rbDefineModuleFunction(fux2, Marshal.StringToCoTaskMemAnsi("sendAnimationsInfo"),
                Marshal.GetFunctionPointerForDelegate(infoDown), 1);
            rbDefineModuleFunction(fux2, Marshal.StringToCoTaskMemAnsi("sendAnimationFramesData"),
                Marshal.GetFunctionPointerForDelegate(dataDown), 1);

            eval = (RGSSEval)dll.Invoke("RGSSEval", typeof(RGSSEval));

        }

        public void setPath(string _rxdataPath)
        {
            // copy
            if (File.Exists(_rxdataPath))
            {
                try
                {
                    File.Delete("Animations.rxdata");
                    File.Copy(_rxdataPath, "Animations.rxdata", true);
                    File.SetAttributes("Animations.rxdata", FileAttributes.Hidden | FileAttributes.System);
                    rxdataPath = _rxdataPath;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            init();
        }

        private void init()
        {
            animations = null;
            if (rxdataPath != null)
            {
                try
                {
                    string code = string.Format(INFO, "Animations.rxdata");
                    eval(Marshal.StringToCoTaskMemAnsi(code));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    animations = null;
                }
            }
        }

        public List<Animation> GetAnimations()
        {
            return animations;
        }

        public Animation selectId(int _id)
        {
            if (animations != null && _id<animations.Count)
            {
                id = _id;
                try
                {
                    string code = string.Format(DATA, id+1);
                    eval(Marshal.StringToCoTaskMemAnsi(code));
                    return animations[id];
                }
                catch (Exception)
                {
                    return null;
                }

            }
            return null;
        }

        private const string INFO = @"
$ani = load_data(""{0}"")
cinfos = $ani.map{{|a| a ? [a.id,a.name,a.animation_name,a.animation_hue,a.position,a.frame_max].pack(""lpplll"") : [0,0,0,0,0,0].pack(""L*"")}}.pack(""p*"")
info = [$ani.size,cinfos].pack(""Lp"")
ptr = [info].pack(""p"").unpack(""L"")[0]
Fux2.sendAnimationsInfo(ptr)
";

        private const string DATA = @"
frames = $ani[{0}].frames.map do |fr|
    con = fr.cell_data
    y_max = con.ysize-1
    x_max = con.xsize-1
    arrDatas = []
    con.xsize.times do |x|
        data = []
        con.ysize.times{{|y| data<<con[x,y]}}
        arrDatas<<data.pack(""l*"")
    end
    [fr.cell_max, arrDatas.pack(""p*"")].pack(""Lp"")
end
frames = frames.pack(""p*"")
ptr = [frames].pack(""p"").unpack(""L"")[0]
Fux2.sendAnimationFramesData(ptr)
";

    }


}
