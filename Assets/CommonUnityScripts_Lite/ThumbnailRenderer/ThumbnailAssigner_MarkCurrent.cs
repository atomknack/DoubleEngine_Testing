#if USES_DOUBLEENGINE
//using AtomKnackGame;
using DoubleEngine.Atom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(ThumbnailAssigner_AssignBackgroundImages))]
//[RequireComponent(typeof(UIDocument))]
//[DefaultExecutionOrder(1090)]
public class ThumbnailAssigner_MarkCurrent : AbstractUiMarkCurrent
{
    private ThumbnailAssigner_AssignBackgroundImages _assigner;

    new void OnEnable()
    {
        _assigner = GetComponent<ThumbnailAssigner_AssignBackgroundImages>();
        base.OnEnable();
    }

    new void OnDisable()
    {
        base.OnDisable();
        _assigner = null;
    }

    public override VisualElement GetUIElementForMeshId(short meshId)
    {
        //Debug.Log(_assigner.GetThumbnailName(meshId));
        return _rootUI.Q<VisualElement>(_assigner.GetThumbnailName(meshId));
    }



    /*private void MarkCurrent(short current)
    {
        UnselectCurrent();
        _current = _rootUI.Q<VisualElement>(_assigner.GetThumbnailName(current));
        SetCurrentBorder();
    }

    private void MaterialChanged(byte newMaterial)
    {
        _currentColor = new StyleColor((Color)DEMaterials.GetUnityAlbedo(newMaterial));
        SetCurrentBorder();
    }
        */
}
#endif