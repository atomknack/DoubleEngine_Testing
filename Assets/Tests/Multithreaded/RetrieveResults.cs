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
using static FluentAssertions_Extensions.Assertions;
using System.Threading;
using Newtonsoft.Json.Linq;
using FluentAssertions;
using DoubleEngine.Atom.Multithreading;

namespace AtomTests.Multithreaded
{
    [TestFixture]
    public partial class AbstractOverlord_Tests
    {
        private static int DoWorkWhileCan(IWorker worker)
        {
            int i = 0;
            while (worker.TryDoTheWork()) { ++i; }
            return i;
        }

        [Category("Multithreaded Special")]
        [Test]
        public static void RetrieveResults_Test()
        {
            IntToDecimalOverlordQueue7Items_MultiplyBy1000 overlord = new IntToDecimalOverlordQueue7Items_MultiplyBy1000();
            IWorker worker = overlord.CreateSubordinateWorker();
            overlord.UnPauseProcessingInput();

            for (int i = 0; i < 100; i++)
                overlord.AddInput(i);
            int workCycles = DoWorkWhileCan(overlord);
            TestContext.WriteLine($"overlord workCycles {workCycles}");
            overlord.ResultsReady().Should().BeFalse();
            workCycles = DoWorkWhileCan(worker);
            TestContext.WriteLine($"worker workCycles {workCycles}");
            workCycles = DoWorkWhileCan(overlord);
            TestContext.WriteLine($"overlord workCycles {workCycles}");

            overlord.ResultsReady(out int resultsCount, out int processedInputCount).Should().BeTrue();
            resultsCount.Should().Be(7);
            processedInputCount.Should().Be(7);
            Decimal[] resultsBuffer = new Decimal[resultsCount];
            int[] processedInputBuffer = new int[processedInputCount];
            overlord.RetrieveResults(resultsBuffer, processedInputBuffer);
            //resultsBuffer.Should().BeEquivalentTo();
            //processedInputBuffer.Should().BeEquivalentTo();
            overlord.ResultsReady(out resultsCount, out processedInputCount).Should().BeFalse();
            resultsCount.Should().Be(0);
            processedInputCount.Should().Be(0);
            Action action = () => { var _ = new Decimal[10]; var __ = new int[10]; overlord.RetrieveResults(_, __); };
            action.Should().ThrowExactly<InvalidOperationException>();
        }
    }
}
