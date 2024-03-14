using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleEngine;
using DoubleEngine.Atom;
using FluentAssertions;

partial class Assert : NUnit.Framework.Assert
{
    /*
    public static void AreEqual<T>(T[] array, ReadOnlySpan<T> span)
    {
        Assert.AreEqual(array.Length, span.Length);
        Assert.AreEqual(array, span);
        for (int i = 0; i < span.Length; ++i)
        {
            Assert.AreEqual(array[i], span[i]);
        }
    }*/
    /*
    public static void MeshNotEmpty(IMeshFragmentWithMaterials<Vec3D> mesh)
    {
        mesh.Faces.Length.Should().BeGreaterThan(0);
        mesh.Triangles.Length.Should().BeGreaterThan(0);
        mesh.Faces.Length.Should().Be(mesh.Triangles.Length / 3);
        mesh.FaceMaterials.Length.Should().Be(mesh.Faces.Length);
    }

    public static void AreContainsEqualElements(IMeshFragmentWithMaterials<Vec3D> expected, IMeshFragmentWithMaterials<Vec3D> actual)
    {
        Assert.AreContainsEqualElements((IMeshFragment<Vec3D>)expected, (IMeshFragment<Vec3D>)actual);
        Assert.AreContainsEqualElements(expected.FaceMaterials, actual.FaceMaterials);
    }
    public static void AreContainsEqualElementsOnSamePlaces(IMeshFragmentWithMaterials<Vec3D> expected, IMeshFragmentWithMaterials<Vec3D> actual)
    {
        Assert.AreContainsEqualElementsOnSamePlaces((IMeshFragment<Vec3D>)expected, (IMeshFragment<Vec3D>)actual);
        Assert.AreContainsEqualElementsOnSamePlaces(expected.FaceMaterials, actual.FaceMaterials);
    }

    public static void AreContainsEqualElements(IMeshFragment<Vec3D> expected, IMeshFragment<Vec3D> actual)
    {
        Assert.AreContainsEqualElements(expected.Vertices, actual.Vertices);
        Assert.AreContainsEqualElements(expected.Triangles, actual.Triangles);
        Assert.AreContainsEqualElements(expected.Faces, actual.Faces);
    }
    public static void AreContainsEqualElementsOnSamePlaces(IMeshFragment<Vec3D> expected, IMeshFragment<Vec3D> actual)
    {
        Assert.AreContainsEqualElementsOnSamePlaces(expected.Vertices, actual.Vertices);
        Assert.AreContainsEqualElementsOnSamePlaces(expected.Triangles, actual.Triangles);
        Assert.AreContainsEqualElementsOnSamePlaces(expected.Faces, actual.Faces);
    }

    public static void AreContainsEqualElements<T>(ReadOnlySpan<T> expected, ReadOnlySpan<T> actual)
    {
        AreEqual(expected.Length, actual.Length);
        for(int i = 0; i < expected.Length; i++)
        {
            var item = expected[i];
            AreEqual(expected.Count(x => x.Equals(item)), actual.Count(x=>x.Equals(item)));
        }
    }

    public static void AreContainsEqualElementsOnSamePlaces<T>(ReadOnlySpan<T> expected, ReadOnlySpan<T> actual)
    {
        actual.Length.Should().Be(expected.Length);
        for (int i = 0; i < expected.Length; i++)
        {
            actual[i].Should().Be(expected[i]);
        }
    }



    public static void AreEqual(ThreeDimensionalCell cell, ThreeDimensionalCell other)
    {
        Assert.AreEqual(cell.GetHashCode(), other.GetHashCode());
        Assert.AreEqual(cell.ToBytesArray(), other.ToBytesArray());
        Assert.True(cell == other);
        NUnit.Framework.Assert.AreEqual(cell, other);
    }
    public static void AreNotEqual(ThreeDimensionalCell cell, ThreeDimensionalCell other)
    {
        Assert.AreNotEqual(cell.GetHashCode(), other.GetHashCode());
        Assert.AreNotEqual(cell.ToBytesArray(), other.ToBytesArray());
        Assert.False(cell == other);
        NUnit.Framework.Assert.AreNotEqual(cell, other);
    }

    public static void AreEqual<T>(Span<T> expected, Span<T> actual) =>
        AreEqual((ReadOnlySpan<T>)expected, (ReadOnlySpan<T>)actual);
    public static void AreEqual<T>(ReadOnlySpan<T> expected, ReadOnlySpan<T> actual)
    {
        AreEqual(expected.Length, actual.Length);
        for (int i = 0; i < expected.Length; i++)
        {
            AreEqual(expected[i], actual[i]);
        }
        //Debug.Log($"AssertAreEqual seccessfully for {expected.Length} items");
    }
    public static void AreEqual(IThreeDimensionalGrid expected, IThreeDimensionalGrid actual)
    {
        var dimensions = actual.GetDimensions();
        Assert.AreEqual(expected.GetDimensions(), dimensions);
        for(int xi = 0; xi<dimensions.x; ++xi)
            for(int yi = 0; yi<dimensions.y; ++yi)
                for(int zi = 0; zi<dimensions.z; ++zi)
                    Assert.AreEqual(expected.GetCell(xi, yi, zi), actual.GetCell(xi, yi, zi));
    }
    */
}
