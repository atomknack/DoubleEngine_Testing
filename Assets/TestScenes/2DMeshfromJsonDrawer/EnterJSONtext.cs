using DoubleEngine;
using DoubleEngine.UHelpers;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EnterJSONtext : MonoBehaviour
{
    private TextField _textField;
    private VisualElement _rootUI;
    public GameObject mesh2dPlaceholder;

    public void OnButtonClick()
    {
        Debug.Log(_textField.text);
        _rootUI.visible = false;
        MeshFragmentVec2D m = JsonConvert.DeserializeObject<MeshFragmentVec2D>(_textField.text);
        MeshFragmentVec3D m3d = MeshFragmentVec3D.CreateMeshFragment(m.Vertices.ConvertXYtoXYZArray(0), m.Triangles.ToArray());
        mesh2dPlaceholder.GetComponent<MeshFilter>().sharedMesh = m3d.ToNewUnityMesh();
        StartCoroutine(MakeUIVisibleLater());
    }

    IEnumerator MakeUIVisibleLater()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        _rootUI.visible = true;
    }

    private void OnEnable()
    {
        _rootUI = GetComponent<UIDocument>().rootVisualElement;
        _textField = _rootUI.Q<TextField>("textField_id");
    }
}
