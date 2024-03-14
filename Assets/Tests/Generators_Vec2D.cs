using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using DoubleEngine;
using Newtonsoft.Json;
using System.Linq;
using VectorCore;

public partial class TestGenerators
{
    public static Vec2D RandVec2D(System.Random rand, Vec2D min, Vec2D max) =>
        new Vec2D(((max.x - min.x) * rand.NextDouble()) + min.x, ((max.y - min.y) * rand.NextDouble()) + min.y);

    public static Vec2D MinComponentsVec2D(Vec2D[] items) => new Vec2D(items.Select(v => v.x).Min(), items.Select(v => v.y).Min());
    public static Vec2D MaxComponentsVec2D(Vec2D[] items) => new Vec2D(items.Select(v => v.x).Max(), items.Select(v => v.y).Max());

}
