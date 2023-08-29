using UnityEditor;
using UnityEngine;

public class RectTransformASLControllerEditorItem : ASLControllerEditorItem
{
    private RectTransform rect;
    private Vector3 position;
    private Vector3 rotation;
    private Vector3 scale;

    public override string EditorItemName => "Rect Transform";

    public RectTransformASLControllerEditorItem(RectTransform rect)
    {
        this.rect = rect;
        InitBase();
        Init();
    }

    public override void InitBaseContent()
    {
        position = rect.localPosition;
        EditorGUILayout.LabelField(string.Format("Position x:{0} y:{1} z:{2}", position.x, position.y, position.z));

        rotation = rect.localRotation.eulerAngles;
        EditorGUILayout.LabelField(string.Format("Rotation x:{0} y:{1} z:{2}", rotation.x, rotation.y, rotation.z));

        scale = rect.localScale;
        EditorGUILayout.LabelField(string.Format("Scale x:{0} y:{1} z:{2}", scale.x, scale.y, scale.z));
    }

    public override void InitContent()
    {
        rect.localPosition = EditorGUILayout.Vector3Field("Position", rect.localPosition);
        rect.localRotation = Quaternion.Euler(EditorGUILayout.Vector3Field("Rotation", rect.localRotation.eulerAngles));
        rect.localScale = EditorGUILayout.Vector3Field("Scale", rect.localScale);
    }
}