using DoubleEngine;
using DoubleEngine.Atom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorCore;

public class ThreeDimensionalOneElementTestingGrid : IThreeDimensionalGrid
{
    Vec3I _cellPos;
    ThreeDimensionalCell _cell;

    public Vec3I OnlyCellPos => _cellPos;
    public ThreeDimensionalCell OnlyCell => _cell;

    public ThreeDimensionalCell GetCell(int x, int y, int z)
    {
        if (_cellPos == new Vec3I(x, y, z))
            return _cell;
        throw new System.ArgumentException($"no cell at requested coords ({x},{y},{z}) only cell is at {_cellPos}");
    }
    IEnumerable<(Vec3I pos, ThreeDimensionalCell cell)> IThreeDimensionalGrid.GetAllMeaningfullCells()
    {
        yield return (_cellPos, _cell);
    }

    void IThreeDimensionalGrid.Clear()
    {
        _cellPos = Vec3I.zero;
        _cell = ThreeDimensionalCell.Empty;
    }

    public Vec3I GetDimensions()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateCell(int x, int y, int z, ThreeDimensionalCell cell)
    {
        _cellPos = new Vec3I(x, y, z);
        _cell = cell;
    }

    public ThreeDimensionalOneElementTestingGrid()
    {
        _cellPos = Vec3I.zero;
        _cell = ThreeDimensionalCell.Empty;
    }
}
