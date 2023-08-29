using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[CustomEditor(typeof(ASLController))]
public class ASLControllerEditor : Editor
{
    private ASLController ASLController;
    private List<ASLControllerEditorItem> items = new List<ASLControllerEditorItem>();


    public override void OnInspectorGUI()
    {
        items.Clear();
        Debug.Log("Create Inspector Gui");
        ASLController = (ASLController)target;
        CheckComponents(ASLController.GetASLComponents());
        base.OnInspectorGUI();
    }

    private void CheckComponents(Component[] components)
    {
        foreach (Component component in components)
            CheckComponent(component);
    }

    private void CheckComponent(Component component)
    {
        Debug.Log($"Available valid {component}");

        if (component.GetType() == typeof(Image))
        {
            Debug.Log($"Create Image Editor");
            items.Add(new ImageASLControllerEditorItem((Image)component));
        }

        if (component.GetType() == typeof(RectTransform))
        {
            Debug.Log("Create RectTransform Editor");
            items.Add(new RectTransformASLControllerEditorItem((RectTransform)component));
        }
    }
}