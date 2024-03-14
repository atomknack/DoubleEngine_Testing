#if USES_DOUBLEENGINE
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleEngine;
using DoubleEngine.Atom;
using CollectionLike;
using AtomKnackGame;

[Obsolete("WIP")]
public class UiEvent_Update_PrevCellTypes : MonoBehaviour
{
    public int maxCapacity = 20;
    private List<short> prevCells;

    private void OnEnable()
    {
        GameEvents.playerAsksPutCell.Subscribe(UpdateQueue);
    }
    private void OnDisable()
    {
        GameEvents.playerAsksPutCell.UnSubscribe(UpdateQueue);
    }

    private void UpdateQueue(SpaceCell newCell)
    {
        short meshId = newCell.cell.GetMeshId();
        prevCells.RemoveAll(meshId);
        prevCells.InsertAtStart(meshId);
        prevCells.RemoveAllElementsStartingFromIndex(5);

        GameEvents.uiUpdatePrevCells_after_playerAsksPutCells.Publish(prevCells);
    }

    void Awake()
    {
        prevCells = new List<short>(maxCapacity);
    }
}
#endif