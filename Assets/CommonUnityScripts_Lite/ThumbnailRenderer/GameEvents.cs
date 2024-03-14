#if USES_DOUBLEENGINE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using Collections.Pooled;
using DoubleEngine;
using DoubleEngine.Atom;
using DoubleEngine.AtomEvents;
using VectorCore;

namespace AtomKnackGame {
    public static class GameEvents
    {
        public static readonly AtomEventWithStoredValue<short> placeholderCellChanged = new();
        public static readonly AtomEventWithStoredValue<byte> placeholderMaterialChanged = new();
        public static readonly AtomEventWithStoredValue<Vec3I> placeholderMovedTo = new();
        public static readonly AtomEvent<SpaceCell> playerAsksPutCell = new();
        public static readonly AtomEventWithStoredValue<ScaleInversionPerpendicularRotation3> placeholderOrientationChanged = new();

        public static readonly AtomEventWithStoredValue<IReadOnlyList<short>> uiUpdatePrevCells_after_playerAsksPutCells = new(new short[] {0,1});

    public static readonly AtomEvent someNotGenericActionHappened_Test = new(); //TODO: Remove

}
    /*
    public class EventWithMemory<T1>: EventType<T1>
    {
        private T1 _lastValue = default(T1);
        public T1 LastValue => _lastValue;
        new public void Publish(T1 value)
        {
            _lastValue = value;
            base.Publish(value);
        }
    }*/
    /*
    public class EventType<T1>
    {
        private event Action<T1> _action;

        public void Subscribe(Action<T1> action)
        {
            _action += action;
        }

        public void UnSubscribe(Action<T1> action)
        {
            _action -=action;
        }
        public void Publish(T1 value)
        {
            _action?.Invoke(value);
        }
    }
    */
    /*
    public class EventType
    {
        private event Action _action;

        public void Subscribe(Action action)
        {
            _action += action;
        }

        public void UnSubscribe(Action action)
        {
            _action -= action;
        }
        public void Publish()
        {
            _action?.Invoke();
        }
    }
    */


    /*
    public static partial class EventsClass<D, En> where En : struct, Enum
    {


        //public enum GameEvent
        //{
        //    Reserved = Action,
        //    Unknown = Action,
        //    PlaceholderCellChanged
        //}

        private static int s_enumMaxValue;
        private static LookUpTable<Action<D>> s_events;

        static EventsClass()
        {
            s_enumMaxValue = GetMaxValueOfEnum<En>();
            s_events = new LookUpTable<Action<D>>(s_enumMaxValue);
        }

        public static void Subscribe(En eventType, Action<D> deleg)
        {
            s_events.AddItem((int)(ValueType)eventType, deleg);
        }
        [Obsolete("Not Implemented")]
        public static void UnSubscribe(En eventType, Action<D> deleg)
        {
            //s_events.((int)(ValueType)eventType, deleg);
        }
        public static void Invoke(En eventType, D value)
        {
            var actions = s_events.GetValues((int)(ValueType)eventType);
            foreach (var action in actions)
            {
                action(value);
            }
        }

        [Obsolete("need testing")]
        private static int GetMaxValueOfEnum<T>()
            where T : struct, Enum
        {
            return (int)(ValueType)Enum.GetValues(typeof(T)).Cast<T>().Max();
        }
    }*/
}
#endif