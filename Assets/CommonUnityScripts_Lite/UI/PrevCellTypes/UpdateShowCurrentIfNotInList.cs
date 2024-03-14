#if USES_DOUBLEENGINE

using AtomKnackGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


[Obsolete("WIP")]
//[RequireComponent(typeof(MoveRootToPlaceholder))]
[RequireComponent(typeof(UIDocument))]
public class UpdateShowCurrentIfNotInList : MonoBehaviour
{
    private readonly string _currentItem = "currentItem";
    private readonly int _selectedBorderSize = 3;
    private readonly static StyleColor _unselectedColor = new StyleColor(new Color());
    private readonly int _unSelectedBorder = 0;

    //bool debugVisible = true;
    //MoveRootToPlaceholder movedRootScript;
    private Button _button;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>();
        _button = root.rootVisualElement.Q<Button>(_currentItem);
        if (_button == null)
            throw new ArgumentException($"UIDocument should contain button: {_currentItem}");

        GameEvents.placeholderCellChanged.Subscribe(UpdateOnPlaceholderCellChanged);
        GameEvents.uiUpdatePrevCells_after_playerAsksPutCells.Subscribe(UpdateOnUIListChanged);
        GameEvents.placeholderMaterialChanged.Subscribe(UpdateOnPlaceholderMaterialChanged);
    }

    private void UpdateOnPlaceholderMaterialChanged(byte _) => DebugVisibility();
    private void UpdateOnUIListChanged(IReadOnlyList<short> _) => DebugVisibility();
    private void UpdateOnPlaceholderCellChanged(short _) => DebugVisibility();

    private void DebugVisibility()
    {
        short current = GameEvents.placeholderCellChanged.Value;
        IReadOnlyList<short> list = GameEvents.uiUpdatePrevCells_after_playerAsksPutCells.Value;

        if (list.Contains(current) == true)
        {
            _button.visible = false;
            AbstractUiMarkCurrent.SetBorderForVisualElement(_button, _unSelectedBorder, _unselectedColor);

            return;
        }

        var material = AbstractUiMarkCurrent.GetStyleColorForMaterial(GameEvents.placeholderMaterialChanged.Value);
        AbstractUiMarkCurrent.SetBorderForVisualElement(_button, _selectedBorderSize, material);
        var background = new StyleBackground(
            ThreeDimensionalCellThumbnail.GridCellRenderedTexture(current));
        _button.style.backgroundImage = background;
        _button.visible = true;

        //UIDocument uiDocument = GetComponent<UIDocument>();
        //debugVisible = !debugVisible;
        //movedRootScript.UIRootUnderParent.Children().First().visible = debugVisible;
        //uiDocument.rootVisualElement.visible = debugVisible;
    }


    private void OnDisable()
    {
        GameEvents.placeholderCellChanged.UnSubscribe(UpdateOnPlaceholderCellChanged);
        GameEvents.uiUpdatePrevCells_after_playerAsksPutCells.UnSubscribe(UpdateOnUIListChanged);
        GameEvents.placeholderMaterialChanged.UnSubscribe(UpdateOnPlaceholderMaterialChanged);
    }
}

#endif