using UnityEditor;

public abstract class ASLControllerEditorItem
{
    private DeviceType.Device[] devices = new DeviceType.Device[] { DeviceType.Device.Iphone, DeviceType.Device.IphoneX, DeviceType.Device.Ipad };
    private const string TitleFormat = "{0} {1}";
    public abstract string EditorItemName { get; }

    public virtual void Init()
    {
        foreach (DeviceType.Device device in devices)
        {
            //if (device == DeviceType.GetDeviceType())
                Init(device);
        }
    }

    public virtual void InitBase()
    {
        EditorGUILayout.LabelField("Base");
        EditorGUILayout.Space();
        InitBaseContent();
        EditorGUILayout.Space();
    }

    public virtual void Init(DeviceType.Device device)
    {
        EditorGUILayout.LabelField(string.Format(TitleFormat, device, EditorItemName));
        InitContent();
        EditorGUILayout.Space(25);
    }

    public virtual void InitBaseContent() { }
    public virtual void InitContent() { }
}