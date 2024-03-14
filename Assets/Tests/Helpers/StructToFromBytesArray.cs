using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using DoubleEngine;
using DoubleEngine.Atom;
using System.IO;
using Newtonsoft.Json;
//using static Assertions;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Runtime.InteropServices;

public static partial class TestHelpers
{
    public static byte[] ToBytesArray(this object obj)
    {
        int size = Marshal.SizeOf(obj);
        byte[] result = new byte[size];
        IntPtr ptr = Marshal.AllocHGlobal(size);

        Marshal.StructureToPtr(obj, ptr, true);
        Marshal.Copy(ptr, result, 0, size);
        Marshal.FreeHGlobal(ptr);

        return result;
    }
    public static byte[] ToBytesArray<T>(this T t)
    {
        int size = Marshal.SizeOf(t);
        byte[] result = new byte[size];
        IntPtr ptr = Marshal.AllocHGlobal(size);

        Marshal.StructureToPtr<T>(t, ptr, true);
        Marshal.Copy(ptr, result, 0, size);
        Marshal.FreeHGlobal(ptr);

        return result;
    }

    public static T FromBytesArray<T>(this byte[] arr)
    {
        T result = default(T);

        int size = Marshal.SizeOf(result);
        IntPtr ptr = Marshal.AllocHGlobal(size);

        Marshal.Copy(arr, 0, ptr, size);

        result = (T)Marshal.PtrToStructure(ptr, result.GetType());
        Marshal.FreeHGlobal(ptr);

        return result;
    }
}
