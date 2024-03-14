#if USES_DOUBLEENGINE
using DoubleEngine.Atom;
using DoubleEngine.UHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.Values;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
[DefaultExecutionOrder(1090)]
public abstract class AbstractUiMarkCurrent : MonoBehaviour
{
    public int MarkedBorderSize_AsInitValueForOnEnable;
    public int DefaultBorderSize_AsInitValueForOnEnable;
    public Color DefaultBorderColor_AsInitValueForOnEnable;

    [SerializeField]
    [ValidReference]
    private SOValue<short> _placeholderCellChanged;

    [SerializeField]
    [ValidReference]
    private SOValue<byte> _placeholderMaterialChanged;

    protected VisualElement _rootUI;
    private VisualElement _current;
    private StyleColor _currentColor;
    private StyleColor _defaultColor;
    private int _defaultBorderSize;
    private int _markedBorderSize;
    protected void OnEnable()
    {
        _defaultColor = new StyleColor(DefaultBorderColor_AsInitValueForOnEnable);
        _defaultBorderSize = DefaultBorderSize_AsInitValueForOnEnable;
        _markedBorderSize = MarkedBorderSize_AsInitValueForOnEnable;

        _rootUI = GetComponent<UIDocument>().rootVisualElement;
        _current = null;

        _placeholderCellChanged.Subscribe(MarkCurrent);
        _placeholderMaterialChanged.Subscribe(MaterialChanged);

        MarkCurrent(_placeholderCellChanged.GetValue());
        MaterialChanged(_placeholderMaterialChanged.GetValue());
    }

    protected void OnDisable()
    {
        UnselectCurrent();
        _placeholderCellChanged.UnsubscribeNullSafe(MarkCurrent);
        _placeholderMaterialChanged.UnsubscribeNullSafe(MaterialChanged);
        _rootUI = null;
    }

    private void UnselectCurrent()
    {
        if (_current == null)
            return;
        SetBorderForVisualElement(_current, _defaultBorderSize, _defaultColor);
        _current = null;
    }

    protected void MarkCurrent(short current)
    {
        UnselectCurrent();
        _current = GetUIElementForMeshId(current);
        SetCurrentBorder();
    }

    public abstract VisualElement GetUIElementForMeshId(short meshId);

    private void MaterialChanged(byte newMaterial)
    {
        _currentColor = GetStyleColorForMaterial(newMaterial);
        SetCurrentBorder();
    }

    private void SetCurrentBorder()
    {
        //Debug.Log(_current == null);
        if (_current == null)
            return;
        SetBorderForVisualElement(_current, _markedBorderSize, _currentColor);
    }

    public static StyleColor GetStyleColorForMaterial(byte material) => 
        new StyleColor((Color)UMaterials.GetUnityAlbedo(material));

    public static void SetBorderForVisualElement(VisualElement element, int borderSize, StyleColor borderColor)
    {
        element.style.borderTopWidth = borderSize;
        element.style.borderRightWidth = borderSize;
        element.style.borderBottomWidth = borderSize;
        element.style.borderLeftWidth = borderSize;

        element.style.borderTopColor = borderColor;
        element.style.borderRightColor = borderColor;
        element.style.borderBottomColor = borderColor;
        element.style.borderLeftColor = borderColor;
    }

}
/*
public class ReadOnlyWrap<T>
{
    private T _value;
    private bool _hasValue;

    public override string ToString()
    {
        return _hasValue ? _value.ToString() : "";
    }
    public T Value
    {
        get
        {
            if (!_hasValue) throw new InvalidOperationException("Value was never set to be able retrieve");
            return _value;
        }
        set
        {
            if (_hasValue) throw new InvalidOperationException("Value was already set, but can be set only once");
            this._value = value;
            this._hasValue = true;
        }
    }

}
*/
#endif