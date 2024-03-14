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

public class IntToDecimalOverlordQueue7Items_MultiplyBy1000 : AbstractOverlord<int, long, decimal>
{
    public const long workMultipliyer = 10;
    public const long resultMultipliyer = 100;
    public static System.Random s_random = new System.Random();
    public class LongToDecimalWorker : AbstractWorker
    {
        public LongToDecimalWorker(AbstractOverlord<int, long, decimal> overlord) : base(overlord) 
        {
            //Debug.Log(_overlord.GetHashCode());
        }

        public override decimal DoTheActualWork(long work)
        {
            //Debug.Log($"worker work called {index} {work}");
            int timeToWait = s_random.Next(0,100);
            //Thread.Sleep(timeToWait);
            return work * resultMultipliyer;
        }
    }
    public IntToDecimalOverlordQueue7Items_MultiplyBy1000() : base(7, 30) {
        //Debug.Log(this.GetHashCode());
    }

    public override AbstractWorker CreateSubordinateWorker() => new LongToDecimalWorker(this);

    protected override void AddWork()
    {
        while (TryGetInputForWork(out int input))
        {
            long work = (long)input * workMultipliyer;
            AddWorkItemToBeProcessed(work);
        }
        //Debug.Log($"AddWorkMethodFinished {_workToBeProcessed.Count}");
    }
}
