using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace animate_export
{
    class DllInvoke
    {
        [DllImport("kernel32.dll")]
        private extern static IntPtr LoadLibrary(string path);

        [DllImport("kernel32.dll")]
        private extern static IntPtr GetProcAddress(IntPtr lib, string funcName);

        [DllImport("kernel32.dll")]
        private extern static bool FreeLibrary(IntPtr lib);
        

        private IntPtr hLib;        
        public DllInvoke(String DLLPath)
        {
            hLib = LoadLibrary(DLLPath);
        }

        ~DllInvoke()
        {
            FreeLibrary(hLib);            
        }

        public IntPtr getLib()
        {
            return hLib;
        }

        public Delegate Invoke(string APIName,Type t)  
        {
            return Marshal.GetDelegateForFunctionPointer(getptr(APIName), t);
        }

        public IntPtr getptr(string APIName)
        {
            return GetProcAddress(hLib, APIName);
        }
    }
}
