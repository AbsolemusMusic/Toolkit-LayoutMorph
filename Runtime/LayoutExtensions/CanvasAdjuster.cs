using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
[ExecuteInEditMode]
public class CanvasAdjuster : MonoBehaviour
{

#if UNITY_EDITOR
    [SerializeField]
    private bool editorCanvas;
#endif

    [Space]
    [SerializeField]
    private Vector2 referenceResolutionIphone;

    [SerializeField]
    [Range(0, 1)]
    private float matchWOHIphone = 0.5f;

    [Space]
    [SerializeField]
    private Vector2 referenceResolutionIpad;

    [SerializeField]
    [Range(0, 1)]
    private float matchWOHIpad = 1f;

    [Space]
    [SerializeField]
    private Vector2 referenceResolutionIphoneX;

    [SerializeField]
    [Range(0, 1)]
    float matchWOHIphoneX = 0;

    bool setToIpad = false;

    public System.Action<CanvasType> OnAspectChange = delegate { };

    public enum CanvasType
    {
        Ipad,
        IphoneX,
        Other,
        None
    }

    public CanvasType canvasType { get; private set; } = CanvasType.None;

    private void Awake()
    {

        var scaler = GetComponent<CanvasScaler>();
        if (referenceResolutionIphoneX == Vector2.zero)
        {
            referenceResolutionIphoneX = scaler.referenceResolution;
        }
        if (referenceResolutionIpad == Vector2.zero)
        {
            referenceResolutionIpad = scaler.referenceResolution;
        }
        if (referenceResolutionIphone == Vector2.zero)
        {
            referenceResolutionIphone = scaler.referenceResolution;
        }
        SetCanvas();

#if UNITY_EDITOR
        if (editorCanvas)
        {
            canvasType = CanvasType.Other;
            scaler.referenceResolution = referenceResolutionIphone;
            scaler.matchWidthOrHeight = matchWOHIphone;
            OnAspectChange(canvasType);
        }
#endif
    }

    private bool IsIpad
    {
        get
        {
            return DeviceType.GetDeviceType() == DeviceType.Device.Ipad;
        }
    }

    private bool IsIphoneX
    {
        get
        {
            return DeviceType.GetDeviceType() == DeviceType.Device.IphoneX;
        }
    }

    private bool IsOther
    {
        get
        {
            return DeviceType.GetDeviceType() != DeviceType.Device.Ipad && DeviceType.GetDeviceType() != DeviceType.Device.IphoneX;
        }
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        SetCanvas();
    }
#endif


    private void SetCanvas()
    {
#if UNITY_EDITOR
        if (editorCanvas)
        {
            return;
        }
#endif
        //Debug.Log(DeviceType.GetDeviceType());
        if (canvasType != CanvasType.Ipad && IsIpad)
        {
            canvasType = CanvasType.Ipad;
            var scaler = GetComponent<CanvasScaler>();
            scaler.referenceResolution = referenceResolutionIpad;
            scaler.matchWidthOrHeight = matchWOHIpad;
            OnAspectChange(canvasType);
        }
        else if (canvasType != CanvasType.IphoneX && IsIphoneX)
        {
            canvasType = CanvasType.IphoneX;
            var scaler = GetComponent<CanvasScaler>();
            scaler.referenceResolution = referenceResolutionIphoneX;
            scaler.matchWidthOrHeight = matchWOHIphoneX;
            OnAspectChange(canvasType);
        }
        else if (canvasType != CanvasType.Other && IsOther)
        {
            canvasType = CanvasType.Other;
            var scaler = GetComponent<CanvasScaler>();
            scaler.referenceResolution = referenceResolutionIphone;
            scaler.matchWidthOrHeight = matchWOHIphone;
            OnAspectChange(canvasType);
        }
    }
}

