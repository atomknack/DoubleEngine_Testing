using DoubleEngine;
using DoubleEngine.UHelpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VectorCore;

public class TriangulatorAndPolygonDrawer : MonoBehaviour
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


    int index = 0;
    GameObject go = null;
    PolygonCollider2D customCollider = null;

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

    void Start()
    {
        cam = Camera.main;
        OneTimeInit();
    }


    public void NextPoly() //WIP
    {
        index.NextIntCyclicRef(polys.Length);
        List<IndexedTri> iTris = new List<IndexedTri>();
        var corners = new List<int>(Enumerable.Range(0, polys[index].Length));
        IndexedPolyVec2D.TrianglesBuilder.Triangulator triangulator = 
            new IndexedPolyVec2D.TrianglesBuilder.Triangulator( polys[index],corners.ToArray(),corners, new List<List<int>>() );
        IndexedPolyVec2D.TrianglesBuilder.TriangulatePolyCorners(ref corners, ref iTris, triangulator);
        /*
        IndexedEdgePoly.TriangulatePolyCorners(
            ref corners,
            polys[index],
            ref iTris);
        */
        Mesh mesh = new Mesh();
        mesh.vertices = polys[index].ConvertXYtoXYZArray(0).ToArrayVector3();
        mesh.triangles = iTris.ToTriangles();
        GameObject.Find("Cube").GetComponent<MeshFilter>().mesh = mesh;
        Debug.Log($"{index}, vertices: {mesh.vertices.Length}, faces: {((double)mesh.triangles.Length)/3}");

        Mesh polyToMesh = new Mesh();
        MeshFragmentVec2D polyToMeshVec2D = MeshFragmentVec2D.FromPolyCopy(polys[index]);
        polyToMesh.vertices = polyToMeshVec2D.Vertices.ToArray().ConvertXYtoXYZArray(0).ToArrayVector3();
        polyToMesh.triangles = polyToMeshVec2D.Triangles.ToArray();
        GameObject.Find("PolyToMeshVec2D_Placeholder").GetComponent<MeshFilter>().mesh = polyToMesh;
    }
    public void Click(Vector2 mousePos)
    {
        Vector3 camPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        Vec2D world2Dpoint = camPoint.ToVec3D().ConvertXYZtoXY();

        Debug.ClearDeveloperConsole();
        Debug.Log($"{CollisionDiscrete2D.PointInsidePolygon(polys[index], world2Dpoint)}, mouse: {world2Dpoint.ToString("F20")}");

        customCollider.points = polys[index].ToArrayVector2();
        Collider2D unityCollision = Physics2D.OverlapPoint(world2Dpoint.ToVector2());
        bool unityInside = unityCollision != null;
        Debug.Log($"UnityPointInside:{unityInside}, point:{world2Dpoint.ToVector2().ToString("F10")}");

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

        DebugDrawer.DrawPolygonContour( polys[index].ToArrayVector2(), 0, transform.localToWorldMatrix, Color.yellow, Color.magenta, 0, 0.3f );
    }
}
