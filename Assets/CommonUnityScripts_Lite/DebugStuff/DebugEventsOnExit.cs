#if USES_DOUBLEENGINE
using System;
using System.Threading;
using UnityEngine;
using DoubleEngine.AtomEvents;
using AtomKnackGame;
using CollectionLike.Enumerables;

[DefaultExecutionOrder(100501)]
public class DebugEventsOnExit : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void OnDestroy()
    {
        CreateNewThread();
    }

    private static void CheckRemainingInvocationList<T>(IAtomEvent<T> @event, string nameOfEvent = "")
    {
        if (@event is null)
            throw new ArgumentNullException(nameOfEvent);
        CheckRemainingItems(nameOfEvent, @event.GetInvocationList());
    }
    private static void CheckRemainingInvocationList(IAtomEvent @event, string nameOfEvent = "")
    {
        if (@event is null)
            throw new ArgumentNullException(nameOfEvent);
        CheckRemainingItems(nameOfEvent, @event.GetInvocationList());
    }

    private static void CheckRemainingItems(string nameOfEvent, Delegate[] items)
    {
        if (items.IsNullOrEmpty())
            return;
        Debug.LogError($"{nameOfEvent} has {items.Length} delegates:");
        int count = 0;
        foreach (var item in items)
        {
            //Debug.Log($"{nameOfEvent} {item?.Target?.ToString()} {item?.Method?.Name}");
            Debug.Log($"{nameOfEvent} delegate number {count}: {item?.Method?.Name} {item?.Target?.GetType().ToString()}");
            count++;
        }
    }


    public void CreateNewThread()
    {
        // Spawn new thread to do concurrent work
        Thread newWorkerThread = new Thread(new ThreadStart(Worker.DoWork));
        newWorkerThread.Start();
    }

    public class Worker
    {
        // Method called by ThreadStart delegate to do concurrent work
        public static void DoWork()
        {
            int seconds = 1;
            for (int i = 0; i < seconds; i++)
            {
                Debug.Log($"onDestroy worker will run event counting in {seconds - i} seconds");
                Thread.Sleep(1000);
            }
            CheckRemainingInvocationList(GameEvents.placeholderCellChanged, nameof(GameEvents.placeholderCellChanged));
            CheckRemainingInvocationList(GameEvents.placeholderMaterialChanged, nameof(GameEvents.placeholderMaterialChanged));
            CheckRemainingInvocationList(GameEvents.placeholderMovedTo, nameof(GameEvents.placeholderMovedTo));
            CheckRemainingInvocationList(GameEvents.placeholderOrientationChanged, nameof(GameEvents.placeholderOrientationChanged));
            CheckRemainingInvocationList(GameEvents.playerAsksPutCell, nameof(GameEvents.playerAsksPutCell));

            CheckRemainingInvocationList(GameEvents.uiUpdatePrevCells_after_playerAsksPutCells, nameof(GameEvents.uiUpdatePrevCells_after_playerAsksPutCells));

            CheckRemainingInvocationList(GameEvents.someNotGenericActionHappened_Test, nameof(GameEvents.someNotGenericActionHappened_Test));
            Debug.Log("Finished checking and counting remaining events");

        }
    }
}
#endif