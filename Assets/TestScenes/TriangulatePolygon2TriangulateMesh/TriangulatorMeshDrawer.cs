using DoubleEngine;
using DoubleEngine.UHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;
using VectorCore;

public class TriangulatorMeshDrawer : MonoBehaviour
{
    MeshFragmentVec2D[] _ones;
    MeshFragmentVec2D[] _twos;
    IEnumerable<MeshFragmentVec2D> _mores;
    List<MeshFragmentVec2D> _atypical;
    ImmutableArray<MeshFragmentVec2D> meshes { get => _mores.Concat(_atypical).ToImmutableArray(); }//_mores.Concat(_twos).Concat(_atypical).ToImmutableArray(); }

    MeshFragmentVec2D mesh2D;
    //Vec2D[] poly;
    //Vec2D[] hole_first;

    private Camera cam;

    IndexedPolyVec2D indexedPoly;

    int index = 0;
    GameObject go = null;
    PolygonCollider2D customCollider = null;


    void Start()
    {
        //var a = new DoubleEngine.IndexedPolyVec2D.Sliver().holes[0].;

        _ones = JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D[]>(
            "C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/Meshes2D_With_one_subpoly.json");
        _twos = JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D[]>(
            "C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/Meshes2D_With_two_subpoly.json");
        _mores = JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D[]>(
            "C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/Meshes2D_With_more_subpoly.json");
        _mores = _mores.Append(JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
            "C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/mesh2D_testcases/20220318_mesh2d_id20_2poly1hole.json"));
        _mores = _mores.Append(JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
            "C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/mesh2D_testcases/20220318_mesh2d_id23_4poly3hole.json"));
        _mores = _mores.Append(JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
            "C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/mesh2D_testcases/20220318_mesh2d_id24_1poly2hole.json"));

        _mores = _mores.Append(JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
            "C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/mesh2D_testcases/20220318_mesh2d_id21_5poly4hole.json"));//.JoinedClosestVertices());

        _atypical = new List<MeshFragmentVec2D>();
        _atypical.Add(JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
            "C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/mesh2D_testcases/20220320_mesh2d_id25_atypical.json"));
        _atypical.Add(JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
            "C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/mesh2D_testcases/20220320_mesh2d_id26_atypical.json"));
        _atypical.Add(JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
            "C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/mesh2D_testcases/20220320_mesh2d_id27_atypical.json"));
        _atypical.Add(JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
            "C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/mesh2D_testcases/20220320_mesh2d_id28_atypical.json"));
        _atypical.Add(JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
            "C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/mesh2D_testcases/20220320_mesh2d_id29_atypical.json"));
        _atypical.Add(JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
            "C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/mesh2D_testcases/20220320_mesh2d_id30_atypical.json"));
_atypical.Add(JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
"C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/mesh2D_testcases/20220321_mesh2d_id32_atypical_NotFinished.json"));
        _atypical.Add(JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
"C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/mesh2D_testcases/20220321_mesh2d_id36_like32_atypical.json"));
        _atypical.Add(JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
"C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/mesh2D_testcases/20220321_mesh2d_id34_atypical_NotFinished.json"));
        _atypical.Add(JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
"C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/mesh2D_testcases/20220321_mesh2d_id39_like34_atypical.json"));
        _atypical.Add(JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
"C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/mesh2D_testcases/20220321_mesh2d_id37_atypical.json"));
        _atypical.Add(JsonHelpers.LoadFromJsonFile<MeshFragmentVec2D>(
"C:/Projects/DoubleLibrary_Testing/Assets/Tests/Meshes/2D/mesh2D_testcases/20220321_mesh2d_id40_atypical.json"));


        cam = Camera.main;
        OneTimeInit();
        ChangeMeshPolygon();
    }

    public void OneTimeInit()
    {
        var go = new GameObject();
        go.AddComponent<PolygonCollider2D>();
        customCollider = go.GetComponent<PolygonCollider2D>();
        go.AddComponent<MeshRenderer>();
        go.AddComponent<MeshFilter>();
    }
    
    public void OneTimeCleanup()
    {
        UnityEngine.GameObject.DestroyImmediate(customCollider);
        UnityEngine.GameObject.DestroyImmediate(go);
    }

    public void EveryTestCleanup()
    {
        customCollider.points = null;
    }

    public void NextPoly() //WIP
    {
        index.NextIntCyclicRef(meshes.Length);
        ChangeMeshPolygon();
    }
    private void ChangeMeshPolygon()
    {
        mesh2D = meshes[index];

        ReadOnlySpan<Vec2D> vertices = mesh2D.Vertices;
        //var mesh = fragment.JoinedClosestVertices();
        var singleEdges = EdgeIndexed.SingleEdgesFromTriangles(mesh2D.Triangles.ToArray());
        var iEPoly = IndexedEdgePoly.DebugIndexedEdgePolyFromSingleEdges( singleEdges, vertices);
        /*
        var corners = IndexedEdgePoly.GetCorners(iEPoly, 0);
        poly = vertices.AssembleIndices(corners);
        if (iEPoly.subPolygons.Length > 1)
            hole_first = vertices.AssembleIndices(IndexedEdgePoly.GetCorners(iEPoly, 1));
        else
            hole_first = null;
        var cornersList = new List<int>(corners);
        */
        indexedPoly = new IndexedPolyVec2D(mesh2D);
        List<IndexedTri> triangulated = indexedPoly.Triangulate();

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ConvertXYtoXYZArray(0).ToArrayVector3();
        mesh.triangles = triangulated.ToTriangles();
        GameObject.Find("Cube").GetComponent<MeshFilter>().mesh = mesh;
        Debug.Log($"{index}, vertices: {mesh2D.Vertices.Length}, faces: {((double)mesh2D.Triangles.Length)/3}");
    }
    public void Click(Vector2 mousePos)
    {
        Vector3 camPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        Vec2D world2Dpoint = camPoint.ToVec3D().ConvertXYZtoXY();

        Debug.ClearDeveloperConsole();
        var vertices = indexedPoly.GetVertices();
        foreach (var sliver in indexedPoly.GetSlivers())
        {
            Vec2D[] poly = sliver.GetBodyPoly().CornersVec2D(vertices);
            Debug.Log($"{CollisionDiscrete2D.PointInsidePolygon(poly, world2Dpoint)}, mouse: {world2Dpoint.ToString("F20")}");
        }
        //Debug.Log($"{CollisionDiscrete2D.PointInsidePolygon(poly, world2Dpoint)}, mouse: {world2Dpoint.ToString("F20")}");

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
        if (indexedPoly != null)
        {
            var vertices = indexedPoly.GetVertices();
            foreach (var sliver in indexedPoly.GetSlivers())
            {
                Vec2D[] poly = sliver.GetBodyPoly().CornersVec2D(vertices);
                DebugDrawer.DrawPolygonContour(poly.ToArrayVector2(), 0, transform.localToWorldMatrix, Color.yellow, Color.magenta, 0, 0.3f);
                foreach(var holeSubpoly in sliver.GetHoles())
                {
                    DebugDrawer.DrawPolygonContour(holeSubpoly.CornersVec2D(vertices).ToArrayVector2(), 0, transform.localToWorldMatrix, Color.white, Color.black, 0, 0.3f);
                }
            }
        }
    }
    
}
