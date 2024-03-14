//using AtomKnackGame;
using AtomKnackGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

[RequireComponent(typeof(ThumbnailAssignerDataSource))]
[DefaultExecutionOrder(-10)]
public class ThumbnailTableGenerator : MonoBehaviour
{
    private class NeighboursFinder
    {
        private short[,] table;
        public NeighboursFinder(int x, int y)
        {
            table = new short[offset(offset(x)), offset(offset(y))];
            for (int xi = 0; xi < table.GetLength(0); xi++)
                for (int yj = 0; yj < table.GetLength(1); yj++)
                    table[xi, yj] = -1;
        }
        private int offset(int a) => a + 1;
        public void SetItem(int x, int y, short item) => table[offset(x), offset(y)] = item;
        public short GetLeft(int x, int y) => table[offset(x)-1, offset(y)];
        public short GetRight(int x, int y) => table[offset(x)+1, offset(y)];
        public short GetUp(int x, int y) => table[offset(x), offset(y)-1];
        public short GetDown(int x, int y) => table[offset(x), offset(y) + 1];

    }

    [SerializeField]
    private string TablePlaceholderName;
    [SerializeField]
    private int maxNumberOfElementsInRow = 3;
    [SerializeField]
    private VisualTreeAsset EmptyButton;

    private readonly string _buttonStyleClassName = "thumbnail";

    private VisualElement placeholdersChildTable;

    private void Awake()
    {
        //var rootUI = GetComponent<UIDocument>().rootVisualElement;

        var dataSource = GetComponent<ThumbnailAssignerDataSource>();
        if (dataSource == null)
            throw new ArgumentNullException();
        placeholdersChildTable = new VisualElement();
        GenerateTable(placeholdersChildTable, dataSource.Prefix, dataSource.MeshIds);
    }

    private void OnEnable()
    {
        var rootUI = GetComponent<UIDocument>().rootVisualElement;
        //Debug.Log(rootUI == null);
        var placeholder = rootUI.Q<VisualElement>(TablePlaceholderName);
        if (placeholder == null)
            throw new ArgumentException(nameof(TablePlaceholderName));
        placeholder.Add(placeholdersChildTable);
    }

    private void GenerateTable(VisualElement placeholder, string prefix, ReadOnlySpan<short> meshIds)
    {
        var finder = new NeighboursFinder(maxNumberOfElementsInRow, (meshIds.Length / maxNumberOfElementsInRow) +1);
        VisualElement lastRow = CreateNewRow(placeholder);
        int inRow = 0;
        int line = 0;
        for (int i = 0; i < meshIds.Length; i++)
        {
            var currentId = meshIds[i];
            if (inRow >= maxNumberOfElementsInRow)
            {
                inRow = 0;
                lastRow = CreateNewRow(placeholder);
                line++;
            }
            CreateNewButton(lastRow, prefix, currentId);
            finder.SetItem(inRow, line, currentId);
            inRow++;
        }
        
        inRow = 0;
        line = 0;
        for (int i = 0; i < meshIds.Length; i++)
        {
            var currentId = meshIds[i];
            if (inRow >= maxNumberOfElementsInRow)
            {
                inRow = 0;
                line++;
            }
#if USES_DOUBLEENGINE
            UI_MeshId_Selector_Static.RegisterMeshId(currentId, gameObject);
            UI_MeshId_Selector_Static.RegisterLeftNeighbour(currentId, finder.GetLeft(inRow, line));
            UI_MeshId_Selector_Static.RegisterRightNeighbour(currentId, finder.GetRight(inRow, line));
            UI_MeshId_Selector_Static.RegisterUpNeighbour(currentId, finder.GetUp(inRow, line));
            UI_MeshId_Selector_Static.RegisterDownNeighbour(currentId, finder.GetDown(inRow, line));
#endif
            inRow++;
        }
        
    }

    private static VisualElement CreateNewRow(VisualElement placeholder)
    {
        VisualElement lastRow = new VisualElement();
        lastRow.style.flexDirection = FlexDirection.Row;
        placeholder.Add(lastRow);
        return lastRow;
    }

    private void CreateNewButton(VisualElement row, string prefix, int i)
    {
        if (i == 0)
        {
            CreateButtonFromTemplate(EmptyButton.Instantiate(), row, prefix + i);
            return;
        }

        CreateNewButton(row, prefix + i);
    }

    private void CreateButtonFromTemplate(TemplateContainer template, VisualElement row, string name)
    {
        var button = template.Q<Button>();
        button.name = name;
        row.Add(template);
    }

    private void CreateNewButton(VisualElement row, string name)
    {
        Button newItem = new Button();
        newItem.AddToClassList(_buttonStyleClassName);
        newItem.name = name;
        row.Add(newItem);
    }
}