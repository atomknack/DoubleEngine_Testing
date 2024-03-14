using FluentAssertions;
using FluentAssertions_Extensions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SelfAssertions
{
    public class SelfAssertion_SpanEquality_Tests
    {
        public static long[][] longs = new long[][] {
            new long[]{}, //0
            new long[]{ 0}, //1
            new long[]{ 0, 0}, //2
            new long[]{ 0, 0, 0}, //3
            new long[]{ 1, 2, 3}, //4
            new long[]{ 2, 3, 1}, //5
            new long[]{ 3, 1, 2}, //6
            new long[]{ 3, 2, 1}, //7
            new long[]{ 1, 2, 3, 1}, //8
            new long[]{ 2, 3, 1}, //9 equal to 5
        };
        [Test]
        public void Span_ShouldEqual_Empty_Test()
        {
            Span_ShouldEqual_Test(Array.Empty<long>(), longs[0]);
            Span_ShouldEqual_Test(longs[0], new long[0]);
            Span_ShouldEqual_Test(longs[0], new ReadOnlySpan<long>(null));
        }

        [TestCase(0, 0)][TestCase(1, 1)][TestCase(2, 2)][TestCase(3, 3)][TestCase(4, 4)]
        [TestCase(5, 5)][TestCase(6, 6)][TestCase(7, 7)][TestCase(8, 8)][TestCase(9, 9)]
        [TestCase(9, 5)][TestCase(5, 9)]
        public void Span_ShouldEqual_positive_Test(int arr1, int arr2)
        {
            Span_ShouldEqual_Test(longs[arr1], longs[arr2]);
        }

        [TestCase(0, 1)][TestCase(1, 0)][TestCase(1, 2)][TestCase(2, 3)][TestCase(3, 0)]
        [TestCase(4, 5)][TestCase(5, 6)][TestCase(7, 6)][TestCase(4, 7)][TestCase(6, 4)]
        [TestCase(4, 8)][TestCase(5, 8)][TestCase(8, 6)][TestCase(8, 7)][TestCase(7, 8)]
        public void Span_ShouldEqual_negative_Test(int arr1, int arr2)
        {
            Span_ShouldNotEqual_Test(longs[arr1], longs[arr2]);
        }
        public void Span_ShouldEqual_Test(ReadOnlySpan<long> arr1, ReadOnlySpan<long> arr2)
        {
            arr1.ShouldEqual(arr2);
            arr1.ToArray().Should().Equal(arr2.ToArray());
            Assert.That(arr1.ToArray(), Is.EqualTo( arr2.ToArray()));
        }
        public void Span_ShouldNotEqual_Test(ReadOnlySpan<long> span1, ReadOnlySpan<long> span2)
        {
            long[] arr1 = span1.ToArray();
            long[] arr2 = span2.ToArray();
            Action act = () => FluentAssertions_Extensions.Assertions.ShouldEqual<long>(arr1,arr2);
            act.Should().ThrowExactly<AssertionException>();
            span1.ToArray().Should().NotEqual(span2.ToArray());
            Assert.That(span1.ToArray(), Is.Not.EqualTo(span2.ToArray()));
        }
    }
}