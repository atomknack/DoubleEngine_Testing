using System;
#if USES_DOUBLEENGINE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AtomKnackGame
{
    [Obsolete("Not used, not tested")]
    public static partial class UI_MeshId_Selector_Static
    {
        private static Dictionary<short, short> s_left = new();
        private static Dictionary<short, short> s_right = new();
        private static Dictionary<short, short> s_up = new();
        private static Dictionary<short, short> s_down = new();
        private static Dictionary<short, GameObject> s_tables = new();

        private static GameObject currentTable = null;

        public static void RegisterMeshId(short meshId, GameObject table) => s_tables.Add(meshId, table);
        public static void RegisterLeftNeighbour(short meshId, short neighbour)
        { if (neighbour != -1) s_left.Add(meshId, neighbour); }
        public static void RegisterRightNeighbour(short meshId, short neighbour)
        { if (neighbour != -1) s_right.Add(meshId, neighbour); }
        public static void RegisterUpNeighbour(short meshId, short neighbour)
        { if (neighbour != -1) s_up.Add(meshId, neighbour); }
        public static void RegisterDownNeighbour(short meshId, short neighbour)
        { if (neighbour != -1) s_down.Add(meshId, neighbour); }

        private static short GetCurrentMeshId => GameEvents.placeholderCellChanged.Value;
        public static void MoveSelection_Left()
        {
            if (s_left.TryGetValue(GetCurrentMeshId, out short neighbour))
                AtomKnackGame.GameEvents.placeholderCellChanged.Publish(neighbour);

        }
        public static void MoveSelection_Right()
        {
            if (s_right.TryGetValue(GetCurrentMeshId, out short neighbour))
                AtomKnackGame.GameEvents.placeholderCellChanged.Publish(neighbour);
        }
        public static void MoveSelection_Up()
        {
            if (s_up.TryGetValue(GetCurrentMeshId, out short neighbour))
                AtomKnackGame.GameEvents.placeholderCellChanged.Publish(neighbour);
        }
        public static void MoveSelection_Down()
        {
            if (s_down.TryGetValue(GetCurrentMeshId, out short neighbour))
                AtomKnackGame.GameEvents.placeholderCellChanged.Publish(neighbour);
        }

    }

}
#endif