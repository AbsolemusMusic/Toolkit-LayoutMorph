using UnityEditor;
using UnityEngine.UI;

public class ImageASLControllerEditorItem : ASLControllerEditorItem
{
    private Image image;

    public override string EditorItemName => "Image";

    public ImageASLControllerEditorItem(Image image)
    {
        this.image = image;
        InitBase();
        Init();
    }

    public override void InitContent()
    {
        image.color = EditorGUILayout.ColorField("Color", image.color);
    }
}