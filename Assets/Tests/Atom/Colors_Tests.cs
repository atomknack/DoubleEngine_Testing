using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;
using DoubleEngine;
using DoubleEngine.Atom;


public class Colors_Tests
{
    static byte[] Rand4bytes() => TestGenerators.RandBytes(4);
    static IEnumerable rand20arraysBy4bytes => Enumerable.Range(0, 20).Select(_=> Rand4bytes());

    [TestCaseSource(nameof(rand20arraysBy4bytes))]
    public void InversionRGB_Tests(byte[] bytes)
    {
        ColorRGBA32 colorRGBA32 = ColorRGBA32.Create(bytes[0], bytes[1], bytes[2], bytes[3]);
        var invertedRGB = colorRGBA32.InvertRGB();
        (byte invR, byte invG, byte invB, byte a) = invertedRGB;
        Assert.AreEqual((byte)(255 - bytes[0]), invR);
        Assert.AreEqual((byte)(255 - bytes[1]), invG);
        Assert.AreEqual((byte)(255 - bytes[2]), invB);
        Assert.AreEqual(bytes[3], a);
    }
    [TestCaseSource(nameof(rand20arraysBy4bytes))]
    public void InversionRGBA_Tests(byte[] bytes)
    {
        ColorRGBA32 colorRGBA32 = ColorRGBA32.Create(bytes[0], bytes[1], bytes[2], bytes[3]);
        var invertedRGBA = colorRGBA32.InvertAll();
        (byte invR, byte invG, byte invB, byte invA) = invertedRGBA;
        Assert.AreEqual((byte)(255 - bytes[0]), invR);
        Assert.AreEqual((byte)(255 - bytes[1]), invG);
        Assert.AreEqual((byte)(255 - bytes[2]), invB);
        Assert.AreEqual((byte)(255 - bytes[3]), invA);
    }

    [TestCaseSource(nameof(rand20arraysBy4bytes))]
    public void Deconstruct_Tests(byte[] bytes)
    {
        Assert.That(bytes.Length, Is.EqualTo(4));
        ColorRGBA32 colorRGBA32 = ColorRGBA32.Create(bytes[0], bytes[1], bytes[2], bytes[3]);
        (byte r1, byte g1, byte b1, byte a1) = colorRGBA32;
        Assert.AreEqual(bytes[0], r1);
        Assert.AreEqual(bytes[1], g1);
        Assert.AreEqual(bytes[2], b1);
        Assert.AreEqual(bytes[3], a1);
        (byte r2, byte g2, byte b2) = colorRGBA32;
        Assert.AreEqual(r1, r1);
        Assert.AreEqual(g1, g2);
        Assert.AreEqual(b1, b2);
        TestContext.WriteLine(String.Join(',', bytes));
    }

    [TestCaseSource(nameof(rand20arraysBy4bytes))]
    public void ToIntAndBack_Tests(byte[] bytes)
    {
        ColorRGBA32 colorRGBA32 = ColorRGBA32.Create(bytes[0], bytes[1], bytes[2], bytes[3]);
        Assert.AreEqual(BitConverter.ToInt32(bytes, 0), colorRGBA32.ToInt());
        ColorRGBA32 color2 = ColorRGBA32.Create(colorRGBA32.ToInt());
        Assert.AreEqual(colorRGBA32, color2);
    }


        [TestCaseSource(nameof(rand20arraysBy4bytes))]
    public void UColor32CompareByBytes_Tests(byte[] bytes)
    {
        ColorRGBA32 colorRGBA32 = ColorRGBA32.Create(bytes[0], bytes[1], bytes[2], bytes[3]);
        Color32 uColor = new Color32(bytes[0], bytes[1], bytes[2], bytes[3]);
        Assert.AreEqual(uColor.ToBytesArray(), colorRGBA32.ToBytesArray());
    }

}
