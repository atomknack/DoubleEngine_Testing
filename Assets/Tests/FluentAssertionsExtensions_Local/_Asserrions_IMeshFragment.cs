using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine;
using DoubleEngine;
using DoubleEngine.Atom;
using FluentAssertions;
using VectorCore;

namespace FluentAssertions_Extensions
{
    public static partial class Assertions
    {
        public static void ShouldNotBeEmpty(this IMeshFragmentWithMaterials<Vec3D> mesh)
        {
            mesh.Should().NotBeNull();
            mesh.Faces.Length.Should().BeGreaterThan(0);
            mesh.Triangles.Length.Should().BeGreaterThan(0);
            mesh.Faces.Length.Should().Be(mesh.Triangles.Length / 3);
            mesh.FaceMaterials.Length.Should().Be(mesh.Faces.Length);
        }

        public static void ShouldContainsEqualElements(this IMeshFragmentWithMaterials<Vec3D> actual, IMeshFragmentWithMaterials<Vec3D> expected)
        {
            ((IMeshFragment<Vec3D>)actual).ShouldContainEqualElements((IMeshFragment<Vec3D>)expected);
            actual.FaceMaterials.ShouldContainEqualElements(expected.FaceMaterials);
        }
        public static void ShouldEqual(this IMeshFragmentWithMaterials<Vec3D> actual, IMeshFragmentWithMaterials<Vec3D> expected)
        {
            ((IMeshFragment<Vec3D>)actual).ShouldEqual((IMeshFragment<Vec3D>)expected);
            actual.FaceMaterials.ShouldEqual(expected.FaceMaterials);
        }

        public static void ShouldContainEqualElements(this IMeshFragment<Vec3D> actual, IMeshFragment<Vec3D> expected)
        {
            actual.Vertices.ShouldContainEqualElements(expected.Vertices);
            actual.Triangles.ShouldContainEqualElements(expected.Triangles);
            actual.Faces.ShouldContainEqualElements(expected.Faces);
        }
        public static void ShouldEqual(this IMeshFragment<Vec3D> actual, IMeshFragment<Vec3D> expected)
        {
            actual.Vertices.ShouldEqual(expected.Vertices);
            actual.Triangles.ShouldEqual(expected.Triangles);
            actual.Faces.ShouldEqual(expected.Faces);
        }
    }
}