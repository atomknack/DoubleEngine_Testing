#if USES_DOUBLEENGINE
using DoubleEngine.Atom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
[RequireComponent(typeof(ThumbnailAssignerDataSource))]
[DefaultExecutionOrder(500)]
public class ThumbnailAssigner_AssignBackgroundImages : MonoBehaviour
{
    private VisualElement _rootUI;
    private Dictionary<int, StyleBackground> _renderedBackgrounds;

    private ThumbnailAssignerDataSource _dataSource;
    private void Awake()
    {
        _renderedBackgrounds = new Dictionary<int, StyleBackground>();
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        _dataSource = GetComponent<ThumbnailAssignerDataSource>();
        _rootUI = GetComponent<UIDocument>().rootVisualElement;
        foreach(var thumbnailId in _dataSource.MeshIds)
        {
            SetBackgroundForMeshId(thumbnailId);
        }
    }

    public string GetThumbnailName(int thumbnailId) => _dataSource.Prefix + thumbnailId;

    private void SetBackgroundForMeshId(int thumbnailId)
    {
        var thumbnail = _rootUI.Q<VisualElement>(GetThumbnailName(thumbnailId));
        if (_renderedBackgrounds.ContainsKey(thumbnailId))
        {
            thumbnail.style.backgroundImage = _renderedBackgrounds[thumbnailId];
            return;
        }
        //Debug.Log(GetThumbnailName(thumbnailId));
        //Debug.Log(thumbnailId);
        //Debug.Log(thumbnail == null);
        var texture = ThreeDimensionalCellThumbnail.GridCellRenderedTexture((short)thumbnailId);
        var background = new StyleBackground(
            texture); //.RenderMeshSometime(ThreeDimensionalCellMeshes.GetUnityMesh((short)thumbnailId)));
        _renderedBackgrounds[thumbnailId] = background;
        thumbnail.style.backgroundImage = background;
    }
}
#endif