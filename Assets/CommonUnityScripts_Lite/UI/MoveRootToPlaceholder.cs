#if USES_DOUBLEENGINE
using DoubleEngine;
using CollectionLike;
using CollectionLike.Enumerables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MoveRootToPlaceholder : MonoBehaviour
{
    [SerializeField]
    private string parentUIDocumentPlaceholderName;
    public VisualElement UIRootUnderParent => _thisRootVisualElement;
    private VisualElement _thisRootVisualElement;

    private void OnEnable()
    {
        string placeholderName = parentUIDocumentPlaceholderName;
        if (placeholderName.IsNullOrEmpty())
            throw new ArgumentException($"{nameof(placeholderName)} can not be null or empty");
        UIDocument uiDocument = GetComponent<UIDocument>();
        UIDocument parent = uiDocument.parentUI;
        if (parent == null)
            throw new ArgumentException($"This script reqires UIDocument to be child of another UIDocument"); ///https://docs.unity3d.com/ScriptReference/UIElements.UIDocument-parentUI.html
        VisualElement placeholder = parent.rootVisualElement.Q<VisualElement>(placeholderName);
        if (placeholder == null)
            throw new ArgumentException($"{nameof(placeholderName)}, value:{placeholderName} - is not found");

        _thisRootVisualElement = uiDocument.rootVisualElement;
        parent.rootVisualElement.Remove(_thisRootVisualElement);
        placeholder.Add(_thisRootVisualElement);
    }

    /*
    private void OnDisable()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        UIDocument parent = uiDocument.parentUI;
        VisualElement thisDocumentRootUI = uiDocument.rootVisualElement;
        parent.rootVisualElement.Remove(thisDocumentRootUI);
    }
    */
}
#endif