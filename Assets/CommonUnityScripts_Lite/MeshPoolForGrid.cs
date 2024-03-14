#if USES_DOUBLEENGINE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleEngine;
using System;
using CollectionLike;

[Obsolete("Never used, Not tested")]
public static class MeshPoolForGrid 
{
    private static int s_createNewPoolCounter = 0;
    //private static int s_poolGetMeshCounter = 0;
    //private static int s_poolLastReturnedCounter = 0;
    private static List<Mesh> _pool = new List<Mesh>(10000);
    public static void ReturnToPool(Mesh mesh)
    {
#if DEBUG
        foreach (var m in _pool)
            if (m == mesh)
                throw new ArgumentException($"trying to return mesh: {nameof(mesh)} that already in pool");
#endif
        mesh.Clear();
        _pool.Add(mesh);
    }
    public static Mesh GetMesh()
    {
        if (_pool.Count == 0)
            return CreateNew();
        //return _pool.ExtractLast();
        return _pool.PopLast();
    }
    private static Mesh CreateNew()
    {
        Mesh mesh = new Mesh();
        mesh.name = $"grid pool mesh _{s_createNewPoolCounter}_ _";
        return mesh;
    }
}
#endif