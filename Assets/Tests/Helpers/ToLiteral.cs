using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using DoubleEngine;
using DoubleEngine.Atom;
using System.IO;
using Newtonsoft.Json;
//using static Assertions;
using System.CodeDom.Compiler;
using System.CodeDom;

public static partial class TestHelpers
{
    [Test]
    public static void ToLiteralSimple_SelfDemonstration()
    {
        var str = JsonHelpers.ArrayToJsonString( Enumerable.Range(0, 9).ToArray());
        TestContext.WriteLine(str);
        TestContext.WriteLine(ToLiteral(str));
        str = JsonHelpers.ArrayToJsonString( Enumerable.Range(0, 9).Select(x=>x.ToString()).ToArray());
        TestContext.WriteLine(str);
        TestContext.WriteLine(ToLiteral(str));
    }
    private static string ToLiteral(string input)
    {
        using (var writer = new StringWriter())
        {
            using (var provider = CodeDomProvider.CreateProvider("CSharp"))
            {
                provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
                return writer.ToString();
            }
        }
    }
}
