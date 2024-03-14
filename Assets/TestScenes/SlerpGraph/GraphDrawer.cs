using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FunctionBehavior))]
public class GraphDrawer : MonoBehaviour
{
    public Transform PointPrefab;
    public float ValueStart = 0f;
    public float ValueEnd = 1f;
    public Color ColorStart = Color.black;
    public Color ColorEnd = Color.white;

    private FunctionBehavior _dataSource;
    // Start is called before the first frame update
    void Start()
    {
        _dataSource = GetComponent<FunctionBehavior>();
        InitPoints();
    }

    private Transform[] points;
    void InitPoints()
    {
        int resolution = 200;
        points = new Transform[resolution];
        float step = Mathf.Abs(ValueEnd-ValueStart) / resolution;

        //var position = Vector3.zero;
        //var scale = Vector3.one * step;

        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i] = Instantiate(PointPrefab);
            point.localPosition = _dataSource.VirtualFuction(i * step);
            point.GetComponent<Renderer>().material.color = Color.Lerp(ColorStart, ColorEnd, ((float)i) / 100f);
            //position.x = (i + 0.5f) * step - 1f;
            //point.localPosition = position;
            //point.localScale = scale;
            point.SetParent(transform, false);
        }
    }
}
