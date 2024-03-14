#if USES_DOUBLEENGINE
using AtomKnackGame;
using DoubleEngine.Atom;
using DoubleEngine.UHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UISelectMaterial : MonoBehaviour
{
    private VisualElement _rootUI;
    private List<(Button, EventCallback<ClickEvent>)> _buttonsWithCallbacks;
    void OnEnable()
    {
        _rootUI = GetComponent<UIDocument>().rootVisualElement;
        _buttonsWithCallbacks = new List<(Button, EventCallback<ClickEvent>)>();
        _rootUI.Query<Button>().ForEach(SetUpButton);
        GameEvents.placeholderMaterialChanged.Subscribe(MaterialChanged);
    }

    void SetUpButton(Button button)
    {
        try
        {
            byte materialId = GetMaterialIdFromButtonName(button.name);
            button.style.backgroundColor = new StyleColor(UMaterials.GetUnityAlbedo(materialId));
            EventCallback<ClickEvent> onClick = ev => GameEvents.placeholderMaterialChanged.Publish(materialId);
            button.RegisterCallback <ClickEvent>(onClick);
            _buttonsWithCallbacks.Add((button, onClick));
        }
        catch
        {

        };

    }

    public static byte GetMaterialIdFromButtonName(string name) => (byte)int.Parse(name.Split('_')[1]);

    private void OnDisable()
    {
        GameEvents.placeholderMaterialChanged.UnSubscribe(MaterialChanged);
        foreach (var buttonWithCallback in _buttonsWithCallbacks)
        {
            buttonWithCallback.Item1.UnregisterCallback<ClickEvent>(buttonWithCallback.Item2);
        }
        _buttonsWithCallbacks.Clear();
        _rootUI = null;
    }
    private void MaterialChanged(byte newMaterial)
    {
        var button = _rootUI.Q<Button>($"Material_{newMaterial}");
        if (button == null)
            return;
        button.Focus();
    }
}
#endif