using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using DoubleEngine;
using CollectionLike;
using CollectionLike.Enumerables;
using DoubleEngine.Atom;
using System.IO;
using Newtonsoft.Json;
//using static Assertions;
using System.CodeDom.Compiler;
using System.CodeDom;

public static partial class TestHelpers
{
    private static System.Random s_rand = new System.Random();
    private static IEnumerable s_tenRandomArrays => Enumerable.Range(0, 10).Select(x => TestGenerators.RandArray(s_rand.Next(10, 100), -20, 25));

    [TestCaseSource(nameof(s_tenRandomArrays))]
    public static void Shuffle_SelfDemonstration(int[] array)
    {
        var copy = array.AsReadOnlySpan().ToArray();
        copy.Shuffle();
        //TestContext.WriteLine(String.Join(',',array));
        //TestContext.WriteLine(String.Join(',',copy));
        if (array.AsReadOnlySpan().AreEqual(copy.AsReadOnlySpan()))
            Assert.Inconclusive("Arrays are equal after shuffle");
        Assert.That(array, Is.SubsetOf(copy));
        Assert.That(copy, Is.SubsetOf(array));
    }
    public static void Shuffle<T>(this IList<T> list)
    {
        int count = list.Count;
        int rounds = count + s_rand.Next(count);
        for(int i = 0; i < rounds; ++i)
        {
            int one = s_rand.Next(count);
            int two = s_rand.Next(count);
            (list[one],list[two]) = (list[two],list[one]);
        }
    }

    public static bool AreEqual<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other)
    {
        if(span.Length!=other.Length)
            return false;
        for (int i = 0; i < span.Length; i++)
        {
            if (false == span[i].Equals(other[i]))
                return false;
        }
        return true;
    }
}
