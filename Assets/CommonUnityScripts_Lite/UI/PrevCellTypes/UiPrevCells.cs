#if USES_DOUBLEENGINE
using AtomKnackGame;
using DoubleEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class UiPrevCells : MonoBehaviour
{
    private VisualElement _rootUI;
    private const int prevCount = 5;
    private short[] _buttonsMeshIds;
    private Button[] _buttons;
    private EventCallback<ClickEvent>[] _clickEvents;

    private void ButtonNumberWasClicked(int number)
    {
        GameEvents.placeholderCellChanged.Publish(_buttonsMeshIds[number]);
    }

    private void PrevChanged(IReadOnlyList<short> newPrevs)
    {
        int minCount = Math.Min(newPrevs.Count, prevCount);
        int minNotRendered = Math.Max(0, minCount - 1);
        for (int i = minNotRendered; i < prevCount; i++)
        {
            _buttons[i].visible = false;
        }
        for(int i = 0; i < minCount; i++)
        {
            _buttonsMeshIds[i] = newPrevs[i];
            SetBackgroundForButton(i);
            _buttons[i].visible = true;
        }


    }

    private void OnEnable()
    {
        _buttonsMeshIds = new short[prevCount];
        _buttons = new Button[prevCount];
        _clickEvents = new EventCallback<ClickEvent>[prevCount]; 
        _rootUI = GetComponent<UIDocument>().rootVisualElement;

        GameEvents.uiUpdatePrevCells_after_playerAsksPutCells.Subscribe(PrevChanged);

        for (int i = 0; i < prevCount; ++i)
        {
            _buttons[i] = GetButtonForPrev(i);
            int closuredI = i;
            _clickEvents[i] = ev => ButtonNumberWasClicked(closuredI);
            _buttons[i].RegisterCallback<ClickEvent>(_clickEvents[i]);
        }

        PrevChanged(GameEvents.uiUpdatePrevCells_after_playerAsksPutCells.Value);
    }

    private void OnDisable()
    {
        GameEvents.uiUpdatePrevCells_after_playerAsksPutCells.UnSubscribe(PrevChanged);
        for (int i = 0; i < prevCount; ++i)
        {
            _buttons[i].UnregisterCallback<ClickEvent>(_clickEvents[i]);
            _clickEvents[i] = null;
        }
    }

    private void SetBackgroundForButton(int index)
    {

        var background = new StyleBackground(
            ThreeDimensionalCellThumbnail.GridCellRenderedTexture(_buttonsMeshIds[index]));
        _buttons[index].style.backgroundImage = background;
    }

    public Button GetButtonForPrev(int index)
    {
        var thumbnailName = $"prev{index}";
        var _button = _rootUI.Q<Button>(thumbnailName);
        return _button;
    }
    public Button GetUIElementForMeshId(short meshId)
    {
        for(int i = 0; i < _buttonsMeshIds.Length; ++i)
        {
            if (_buttonsMeshIds[i] == meshId)
                return _buttons[i];
        }
        return null;
    }
}
#endif