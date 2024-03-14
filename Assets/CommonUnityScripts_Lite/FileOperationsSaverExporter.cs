#if USES_DOUBLEENGINE && SIMPLEFILEBROWSER
using SimpleFileBrowser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using DoubleEngine;
using DoubleEngine.Atom;
using UKnack;
using UKnack.Attributes;
using UKnack.Events;

public class FileOperationsSaverExporter : MonoBehaviour
{
    [SerializeField]
    private AbstractGridMeshGenerator gridMeshGenerator;
    [SerializeField]
    [ValidReference]
    private SOEvent<string> _loadGrid;
    [SerializeField]
    [ValidReference]
    private SOEvent<string> _loadGridAdditive;
    [SerializeField]
    [ValidReference]
    private SOEvent<string> _saveGrid;
    [SerializeField]
    [ValidReference]
    private SOEvent<string> _exportUnDecimatedMesh3D;
    [SerializeField]
    [ValidReference]
    private SOEvent<string> _exportDecimatedMesh3D;
    [SerializeField]
    [ValidReference]
    private SOEvent<string> _exportObj;

    private void OnEnable()
    {
        if (gridMeshGenerator == null)
        {
            Debug.Log("No gridMeshGenerator component");
            throw new MissingComponentException(nameof(gridMeshGenerator));
        }
        _loadGrid.Subscribe(LoadGrid);
        _loadGridAdditive.Subscribe(LoadGridBrushAdditive);
        _saveGrid.Subscribe(SaveGrid);
        _exportUnDecimatedMesh3D.Subscribe(ExportUnDecimated);
        _exportDecimatedMesh3D.Subscribe(ExportDecimated);
        _exportObj.Subscribe(ExportObj);
    }

    private void OnDisable()
    {
        _loadGrid.UnsubscribeNullSafe(LoadGrid);
        _loadGridAdditive.UnsubscribeNullSafe(LoadGridBrushAdditive);
        _saveGrid.UnsubscribeNullSafe(SaveGrid);
        _exportUnDecimatedMesh3D.UnsubscribeNullSafe(ExportUnDecimated);
        _exportDecimatedMesh3D.UnsubscribeNullSafe(ExportDecimated);
        _exportObj.UnsubscribeNullSafe(ExportObj);
    }

    void SaveGrid(string path)
    {
        Try.Action(x => gridMeshGenerator.SaveGrid(x), path, "save to bg file");
    }
    void LoadGrid(string path)
    {
        Try.Action(x =>
        {
            gridMeshGenerator.LoadGrid(x);
            gridMeshGenerator.UpdateAfterLoad();
        }, path, "load from bg file");
    }

    void LoadGridBrushAdditive(string path)
    {
        Try.Action(x =>
        {
            var brush = LoadNewBrush(path);
            foreach (var item in brush.GetAllMeaningfullCells())
                gridMeshGenerator.Put(item.pos.x, item.pos.y, item.pos.z, item.cell);
            gridMeshGenerator.UpdateAfterLoad();
        }, path, "load additive from file");
    }

    private IThreeDimensionalGrid LoadNewBrush(string path)
    {
        IThreeDimensionalGrid newGrid = ThreeDimensionalGrid.Create(65, 65, 65);
        var offsetter = ThreeDimensionalGridOffsetter.Create(newGrid);
        offsetter.SetOffset(new Vec3I(32,32, 32));
        GridLoaders.LoadGrid(offsetter, path);
        return offsetter;
    }

    void ExportUnDecimated(string path)
    {
        Try.Action(ExportModel, path, $"export UnDecimatedMesh3D file");
        void ExportModel(string path)
        {
            JsonHelpers.SaveToJsonFile(gridMeshGenerator.GetMeshFragmentVec3D(), path);
        }
    }

    void ExportDecimated(string path)
    {
        Debug.Log($"Export path chosen: {path}");
        Try.Action(ExportModel, path, $"export Decimated file");
        void ExportModel(string path)
        {
            JsonHelpers.SaveToJsonFile(gridMeshGenerator.GetDecimatedMeshFragmentVec3D(), path);
        }
    }

    void ExportObj(string path)
    {
        Try.Action(ExportModel, path, "export to obj file");
        void ExportModel(string path)
        {
            File.WriteAllText(path, gridMeshGenerator.GetDecimatedMeshFragmentVec3D().SerializeAsOBJFormatString());//JsonHelpers.SaveToJsonFile(deshelled, path);
        }
    }


}

#endif