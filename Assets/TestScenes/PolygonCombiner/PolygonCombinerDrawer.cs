using DoubleEngine;
using DoubleEngine.UHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VectorCore;

public class PolygonCombinerDrawer : MonoBehaviour
{
    public static Vec2D[][] polys = new Vec2D[][] {
            new Vec2D[]{ new Vec2D(0,0), new Vec2D(3,5), new Vec2D(5,1) },
            new Vec2D[]{ new Vec2D(1,1), new Vec2D(2,-2), new Vec2D(1, -1), new Vec2D(-1,-3),  new Vec2D(-0.7,1)},
            new Vec2D[]{new Vec2D(0,3),new Vec2D(0.5, 1.5),new Vec2D(3,1.5),new Vec2D(1,1),new Vec2D(2,-1),new Vec2D(0,0),new Vec2D(-2,-1),new Vec2D(-1,1),new Vec2D(-3,1.5),new Vec2D(-0.5,1.5)},
            new Vec2D[]{ new Vec2D(0,2), new Vec2D(2, 0), new Vec2D(-2,-2) },
            new Vec2D[]{ new Vec2D(-1,2), new Vec2D(3, 5), new Vec2D(0,-7) },
            new Vec2D[]{ new Vec2D(-1,-1), new Vec2D(-1,1), new Vec2D(1, 1), new Vec2D(1,-1)},

            //backwards poly 1
            //new Vec2D[]{ new Vec2D(-0.7,1), new Vec2D(-1, -3), new Vec2D(1, -1), new Vec2D(2, -2), new Vec2D(1,1),},
        };

    private Camera cam;
    public GameObject poly1Placeholder;
    public GameObject poly2Placeholder;
    public GameObject assembledPlaceholder;//

    int index = 0;
    int indexPoly2 = 0;

    Vec2D _poly2Center = new Vec2D(0,0);
    //GameObject go = null;
    //PolygonCollider2D customCollider = null;
    EdgeVec2D[] debugEdges = new EdgeVec2D[0];
    EdgeVec2D[] subtractingSplittedEdges = new EdgeVec2D[0];
    List<Vec2D[]> assembledPolys = new List<Vec2D[]>();


    Vec2D[] cutOutPolyVertices = null;
    Collections.Pooled.PooledList<EdgeIndexed> singleEdges = null;
    public void OneTimeInit()
    {
        //var go = new GameObject();
        //go.AddComponent<PolygonCollider2D>();
        //customCollider = go.GetComponent<PolygonCollider2D>();
        //go.AddComponent<MeshRenderer>();
        //go.AddComponent<MeshFilter>();
        UpdatePolys();
    }

    public void OneTimeCleanup()
    {
        //UnityEngine.GameObject.DestroyImmediate(customCollider);
        //UnityEngine.GameObject.DestroyImmediate(go);
    }

    public void EveryTestCleanup()
    {
        //customCollider.points = null;
    }

    void Start()
    {
        DoubleEngine.InternalTesting.EntryPoint();

        cam = Camera.main;
        OneTimeInit();
    }

    private void UpdatePolys()
    {
        Vec2D[] poly = polys[index];

        List<IndexedTri> iTris = new List<IndexedTri>();
        var corners = new List<int>(Enumerable.Range(0, poly.Length));
        IndexedPolyVec2D.TrianglesBuilder.Triangulator triangulator =
            new IndexedPolyVec2D.TrianglesBuilder.Triangulator(poly, corners.ToArray(), corners, new List<List<int>>());
        IndexedPolyVec2D.TrianglesBuilder.TriangulatePolyCorners(ref corners, ref iTris, triangulator);
        Mesh mesh = new Mesh();
        mesh.vertices = poly.ConvertXYtoXYZArray(0).ToArrayVector3();
        mesh.triangles = iTris.ToTriangles();
        poly1Placeholder.GetComponent<MeshFilter>().mesh = mesh;
        //Debug.Log($"{index}, vertices: {mesh.vertices.Length}, faces: {((double)mesh.triangles.Length) / 3}");

        Vec2D[] poly2 = new Vec2D[polys[indexPoly2].Length];
        for (int i = 0; i < poly2.Length; i++)
            poly2[i] = polys[indexPoly2][i] + _poly2Center;

        List<IndexedTri> iTris2 = new List<IndexedTri>();
        var corners2 = new List<int>(Enumerable.Range(0, poly2.Length));
        IndexedPolyVec2D.TrianglesBuilder.Triangulator triangulator2 =
            new IndexedPolyVec2D.TrianglesBuilder.Triangulator(poly2, corners2.ToArray(), corners2, new List<List<int>>());
        IndexedPolyVec2D.TrianglesBuilder.TriangulatePolyCorners(ref corners2, ref iTris2, triangulator2);
        Mesh mesh2 = new Mesh();
        mesh2.vertices = poly2.ConvertXYtoXYZArray(0).ToArrayVector3();
        mesh2.triangles = iTris2.ToTriangles();
        poly2Placeholder.GetComponent<MeshFilter>().mesh = mesh2;
        //Debug.Log($"{index}, vertices: {mesh.vertices.Length}, faces: {((double)mesh.triangles.Length) / 3}");

        //(debugEdges, subtractingSplittedEdges, assembledPolys) = 
        //    IndexedPolyVec2D.Subtracter.DebugTestCutOutFromPoly(poly, poly2);


        if (singleEdges != null)
            singleEdges.Dispose();
        singleEdges = null;
        Mesh triangulatedMesh = new Mesh();
        if (poly != null && poly2 != null)
        if (IndexedPolyVec2D.Subtracter.CutOutIfOverlapsPoly(poly, poly2, out cutOutPolyVertices, out singleEdges))
        {
                //Debug.Log($"single edges {singleEdges.Count}");
            IndexedPolyVec2D indexedPoly = IndexedPolyVec2D.CreateIndexedPolyVec2D(cutOutPolyVertices, IndexedEdgePoly.
                DebugIndexedEdgePolyFromSingleEdges(singleEdges.Span, cutOutPolyVertices));
            List<IndexedTri> triangulated = indexedPoly.Triangulate();


            triangulatedMesh.vertices = cutOutPolyVertices.ConvertXYtoXYZArray(0).ToArrayVector3();
            triangulatedMesh.triangles = triangulated.ToTriangles();

        }
        GameObject.Find("Cube").GetComponent<MeshFilter>().mesh = triangulatedMesh;
        //Debug.Log(assembledPolys.Count);
        //debugEdges = poly2.EdgesVec2DFromCornersVec2D();
    }

    public void NextPoly() //WIP
    {
        index.NextIntCyclicRef(polys.Length);
        UpdatePolys();
    }
    public void NextPoly2() //WIP
    {
        indexPoly2.NextIntCyclicRef(polys.Length);
        UpdatePolys();
    }

    public void Click(Vector2 mousePos)
    {
        Vector3 camPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        Vec2D world2Dpoint = camPoint.ToVec3D().ConvertXYZtoXY();
        _poly2Center = world2Dpoint;
        UpdatePolys();

        Debug.ClearDeveloperConsole();
        //Debug.Log($"{CollisionDiscrete2D.PointInsidePolygon(polys[index], world2Dpoint)}, mouse: {world2Dpoint.ToString("F20")}");


        EveryTestCleanup();
    }

    void OnGUI()
    {
        Event currentEvent = Event.current;
        if (Input.GetMouseButtonUp(0) && currentEvent.type == EventType.MouseUp)
            Click(new Vector2(currentEvent.mousePosition.x, cam.pixelHeight - currentEvent.mousePosition.y));
    }

    // Update is called once per frame
    void Update()
    {
        /*
        foreach (var poly in assembledPolys)
        {
            DebugDrawer.DrawPolygonContour(poly, 0d, null, Color.cyan, Color.red, 0, 0.7d);
        }
        */
        if (cutOutPolyVertices != null && singleEdges != null)
            DebugDrawer.DrawEdges(cutOutPolyVertices, singleEdges.Span, Color.cyan, Color.black, 0, 0.7f);

    }
}
