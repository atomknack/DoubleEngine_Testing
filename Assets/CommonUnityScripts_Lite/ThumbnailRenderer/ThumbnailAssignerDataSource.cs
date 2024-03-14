using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
[DefaultExecutionOrder(-10000)]
public class ThumbnailAssignerDataSource: MonoBehaviour
{
    [SerializeField]
    protected string thumbnailIdPrefix;
    public string Prefix => thumbnailIdPrefix;
    [SerializeField]
    protected short[] thumbnailMeshIds;
    public ReadOnlySpan<short> MeshIds => thumbnailMeshIds;
}