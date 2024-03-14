#if USES_DOUBLEENGINE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(ThumbnailAssigner_AssignBackgroundImages))]
[DefaultExecutionOrder(3000)]
public class ThumbnailAssigner_ChangeMeshOnClick : MonoBehaviour
{
    public UnityEvent<short> MeshChanger;
    private VisualElement _rootUI;
    private ThumbnailAssignerDataSource _dataSource;
    private ThumbnailAssigner_AssignBackgroundImages _thumbnailsAssigner;
    private EventCallback<ClickEvent>[] _clickEvents;
    //private short[] _meshIds;
    private void Awake()
    {
        _dataSource = GetComponent<ThumbnailAssignerDataSource>();
        _thumbnailsAssigner = GetComponent<ThumbnailAssigner_AssignBackgroundImages>();
        var meshIds = _dataSource.MeshIds;
        _clickEvents = new EventCallback<ClickEvent>[meshIds.Length];
        for (int i = 0; i < meshIds.Length; ++i)
        {
            short meshId = meshIds[i];
            _clickEvents[i] = ev => MeshChanger.Invoke(meshId);
        }

    }

    private void OnEnable()
    {
        _rootUI = GetComponent<UIDocument>().rootVisualElement;
        var meshIds = _dataSource.MeshIds;
        for (int i = 0; i < meshIds.Length; ++i)
        {
            short meshId = meshIds[i];
            Button _button = GetButtonForMeshId(meshId);
            //_button.focusable = false;
            //_clickEvents[i] = ev => MeshChanger.Invoke(meshId);
            _button.RegisterCallback<ClickEvent>(_clickEvents[i]);
        }
    }
    public Button GetButtonForMeshId(int index)
    {
        var thumbnailName = _thumbnailsAssigner.GetThumbnailName(index);
        var _button = _rootUI.Q<Button>(thumbnailName);
        return _button;
    }

    private void OnDisable()
    {
        var meshIds = _dataSource.MeshIds;
        for (int i = 0; i < _clickEvents.Length; ++i)
        {
            Button _button = GetButtonForMeshId(meshIds[i]);
            _button.UnregisterCallback<ClickEvent>(_clickEvents[i]);
            //_clickEvents[i] = null;
        }
    }
}
#endif