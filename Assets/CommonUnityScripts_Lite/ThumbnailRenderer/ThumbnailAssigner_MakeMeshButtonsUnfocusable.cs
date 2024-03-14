///replaced by MakeAllButtonsUnfocusable
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ThumbnailAssigner_ChangeMeshOnClick))]
[RequireComponent(typeof(ThumbnailAssigner))]
[DefaultExecutionOrder(70)]
public class ThumbnailAssigner_MakeMeshButtonsUnfocusable : MonoBehaviour
{
    private void OnEnable()
    {
        var assigner = GetComponent<ThumbnailAssigner>();
        var changeOnClick = GetComponent<ThumbnailAssigner_ChangeMeshOnClick>();
        for (int i = 0; i< assigner.thumbnailMeshIds.Length; ++i)
        {
            var button = changeOnClick.GetButtonForMeshId(assigner.thumbnailMeshIds[i]);
            button.focusable = false;
        }
    }

}
*/
