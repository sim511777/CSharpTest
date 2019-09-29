using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest {
    public class Msvcrt {
        const string dll = "msvcrt.dll";
        [DllImport(dll)] public static extern IntPtr memset(IntPtr _Dst, int _Val, IntPtr _Size);
        [DllImport(dll)] public static extern IntPtr memcpy(IntPtr _Dst, IntPtr _Src, IntPtr _Size);
    }

    public class Native {
        const string dll = "Native.dll";
        [DllImport(dll)] public static extern int Add(int a, int b);
        [DllImport(dll)] public static extern IntPtr NewBuffer(long cb);
        [DllImport(dll)] public static extern void DeleteBuffer(IntPtr buffer);
        [DllImport(dll)] public static extern IntPtr MallocBuffer(long cb);
        [DllImport(dll)] public static extern void FreeBuffer(IntPtr buffer);
    }
}
