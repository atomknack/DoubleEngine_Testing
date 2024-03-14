#if USES_DOUBLEENGINE
using AtomKnackGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UiPrevCells))]
public class UiPrevCells_MarkCurrent : AbstractUiMarkCurrent
{
    private UiPrevCells _uiPrevCells;
    private void Awake()
    {
        _uiPrevCells = GetComponent<UiPrevCells>();
    }
    public override VisualElement GetUIElementForMeshId(short meshId) => 
        _uiPrevCells.GetUIElementForMeshId(meshId);

    public new void OnEnable()
    {
        base.OnEnable();
        GameEvents.uiUpdatePrevCells_after_playerAsksPutCells.Subscribe(MarkLast);

    }

    private void MarkLast(IReadOnlyList<short> list)
    {
        MarkCurrent(list[0]);
    }

    public new void OnDisable()
    {
        GameEvents.uiUpdatePrevCells_after_playerAsksPutCells.UnSubscribe(MarkLast);
        base.OnDisable();
    }
}
#endif