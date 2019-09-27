using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest {
    public class Msvcrt {
        const string dll = "msvcrt.dll";
        [DllImport(dll, EntryPoint = "memset", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr memset(IntPtr _Dst, int _Val, IntPtr _Size);
        [DllImport(dll, EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr memcpy(IntPtr _Dst, IntPtr _Src, IntPtr _Size);
    }

    public class Native {
        [DllImport("Native.dll")]
        public static extern int Add(int a, int b);
    }
}
