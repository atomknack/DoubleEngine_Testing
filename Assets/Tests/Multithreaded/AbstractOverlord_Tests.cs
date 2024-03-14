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
using DoubleEngine.Atom.Multithreading;

namespace AtomTests.Multithreaded
{
    [TestFixture]
    public partial class AbstractOverlord_Tests
    {
        [Category("Multithreaded Special")]
        [Test]
        public static void IntOverlordSimple_TestRun()
        {
            var overlord = new IntToDecimalOverlordQueue7Items_MultiplyBy1000();
            SimpleBoolCondition runThreads = new SimpleBoolCondition(true);
            WorkersConditionRunner overlordRunner = new WorkersConditionRunner(new IWorker[] { overlord }, runThreads);
            WorkersConditionRunner workersOne = new WorkersConditionRunner(new IWorker[] { overlord.CreateSubordinateWorker() }, runThreads);
            WorkersConditionRunner workersTwo = new WorkersConditionRunner(new IWorker[] { overlord.CreateSubordinateWorker(), overlord.CreateSubordinateWorker() }, runThreads);


            var overlordThread = new Thread( () => overlordRunner.SimpleRunWorkersWhileConditionTrue());
            var workersOneThread = new Thread(() => workersOne.SimpleRunWorkersWhileConditionTrue());
            var workersTwoThread = new Thread(() => workersTwo.SimpleRunWorkersWhileConditionTrue());
            Assert.True(true);
            Decimal value = 0;
            int multiplyer = 1000;
            Assert.True(multiplyer == IntToDecimalOverlordQueue7Items_MultiplyBy1000.workMultipliyer*IntToDecimalOverlordQueue7Items_MultiplyBy1000.resultMultipliyer);
            for (long i = 10000; i < 10500; i++)
            {
                value = value + (i * multiplyer);
                overlord.AddInput((int)i);
            }
            overlordThread.Start();
            workersOneThread.Start();
            workersTwoThread.Start();

            TestContext.WriteLine(value.ToString());
            overlord.UnPauseProcessingInput();
            Decimal value2 = 0;
            Decimal value3 = 0;
            Span<Decimal> resultsBuffer = new Decimal[50];
            Span<int> processedInputBuffer = new int[100];
            for (int i = 0; i < 200; i++)
            {
                //Debug.Log($"o {overlordThread.ThreadState.ToString()} w {workersOneThread.ThreadState.ToString()} w {workersTwoThread.ThreadState.ToString()}");
                if (overlord.ResultsReady(out int resultsCount, out int processedInputCount))
                {
                    overlord.RetrieveResults(resultsBuffer, processedInputBuffer);
                    //TestContext.WriteLine($"RETRIEVED RESULTS {resultsCount.ToString()}");
                    for (int j = 0; j < resultsCount; j++)
                    {
                        value2 = value2 + resultsBuffer[j];
                    }
                    TestContext.WriteLine(value2.ToString());
                    for (int k = 0; k <processedInputCount; k++)
                    {
                        value3 = value3 + (processedInputBuffer[k] * multiplyer);
                    }
                }
                Thread.Sleep(1);
            }
            runThreads.SetValue(false);
            TestContext.WriteLine(value2.ToString());
            Assert.AreEqual( value, value2);
            TestContext.WriteLine(value3.ToString());
            Assert.AreEqual(value, value3);
            //TestContext.Write((int)value3);
        }

        [Category("Multithreaded Special")]
        [Test]
        public static void IntOverlordSimple_UnpauseBeforeInput_TestRun()
        {
            var overlord = new IntToDecimalOverlordQueue7Items_MultiplyBy1000();
            SimpleBoolCondition runThreads = new SimpleBoolCondition(true);
            WorkersConditionRunner overlordRunner = new WorkersConditionRunner(new IWorker[] { overlord }, runThreads);
            WorkersConditionRunner workersOne = new WorkersConditionRunner(new IWorker[] { overlord.CreateSubordinateWorker() }, runThreads);
            WorkersConditionRunner workersTwo = new WorkersConditionRunner(new IWorker[] { overlord.CreateSubordinateWorker(), overlord.CreateSubordinateWorker() }, runThreads);


            var overlordThread = new Thread(() => overlordRunner.SimpleRunWorkersWhileConditionTrue());
            var workersOneThread = new Thread(() => workersOne.SimpleRunWorkersWhileConditionTrue());
            var workersTwoThread = new Thread(() => workersTwo.SimpleRunWorkersWhileConditionTrue());
            Assert.True(true);
            Decimal value = 0;
            int multiplyer = 1000;
            Assert.True(multiplyer == IntToDecimalOverlordQueue7Items_MultiplyBy1000.workMultipliyer * IntToDecimalOverlordQueue7Items_MultiplyBy1000.resultMultipliyer);
            overlordThread.Start();
            workersOneThread.Start();
            workersTwoThread.Start();
            overlord.UnPauseProcessingInput();
            for (long i = 18; i < 100; i++)
            {
                value = value + (i * multiplyer);
                overlord.AddInput((int)i);
                if(i == 70)
                    overlord.PauseProcessingInput();
            }
            overlord.UnPauseProcessingInput();
            TestContext.WriteLine(value.ToString());
            Decimal value2 = 0;
            Decimal value3 = 0;
            Span<Decimal> resultsBuffer = new Decimal[50];
            Span<int> processedInputBuffer = new int[100];
            for (int i = 0; i < 50; i++)
            {
                //Debug.Log($"o {overlordThread.ThreadState.ToString()} w {workersOneThread.ThreadState.ToString()} w {workersTwoThread.ThreadState.ToString()}");
                if (overlord.ResultsReady(out int resultsCount, out int processedInputCount))
                {
                    overlord.RetrieveResults(resultsBuffer, processedInputBuffer);
                    //TestContext.WriteLine($"RETRIEVED RESULTS {resultsCount.ToString()}");
                    for (int j = 0; j < resultsCount; j++)
                    {
                        value2 = value2 + resultsBuffer[j];
                    }
                    TestContext.WriteLine(value2.ToString());
                    for (int k = 0; k < processedInputCount; k++)
                    {
                        value3 = value3 + (processedInputBuffer[k] * multiplyer);
                    }
                }
                Thread.Sleep(1);
            }
            runThreads.SetValue(false);
            TestContext.WriteLine(value2.ToString());
            Assert.AreEqual(value, value2);
            TestContext.WriteLine(value3.ToString());
            Assert.AreEqual(value, value3);
        }

    }
}