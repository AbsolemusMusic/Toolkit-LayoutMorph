using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class AdvancedFixUIElement : MonoBehaviour
{
    [SerializeField]
    private DeviceGroup iPhoneX;

    [SerializeField]
    private DeviceGroup iPad;

    [SerializeField]
    private bool baseValuesSet;

    [SerializeField]
    private Vector2 baseAnchoredPosition;

    [SerializeField]
    private Vector2 baseSizeDelta;


    private CanvasAdjuster rootAdjuster;

    private RectTransform rectTransform;

    private DeviceType.Device currentDeviceType = DeviceType.Device.Other;

#if UNITY_EDITOR
    private SerializedObject serializedObject;

    private SerializedProperty iPadSerializedProperty, iPhoneXSerializedProperty;

    private Vector2 lastAnchoredPosition, lastSizeDelta;

    private Vector3 lastScale;
#endif

    private void OnEnable()
    {
        //return;
        rectTransform = GetComponent<RectTransform>();
#if UNITY_EDITOR
        rootAdjuster = transform.root.GetComponent<CanvasAdjuster>();

        serializedObject = new SerializedObject(this);
        iPadSerializedProperty = serializedObject.FindProperty("iPad");
        iPhoneXSerializedProperty = serializedObject.FindProperty("iPhoneX");

        if (rootAdjuster != null)
        {
            rootAdjuster.OnAspectChange += OnAspectChange;
        }
        if (!baseValuesSet)
        {
            serializedObject.FindProperty("baseAnchoredPosition").vector2Value = rectTransform.anchoredPosition;
            serializedObject.FindProperty("baseSizeDelta").vector2Value = rectTransform.sizeDelta;
            serializedObject.FindProperty("baseValuesSet").boolValue = true;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
#endif

        SelectAndSet();
    }

#if UNITY_EDITOR
    private void OnDisable()
    {
        if (rootAdjuster != null)
            rootAdjuster.OnAspectChange -= OnAspectChange;
    }
#endif

    private void SelectAndSet()
    {

#if UNITY_EDITOR
        if (rootAdjuster == null)
        {
            rootAdjuster = transform.root.GetComponent<CanvasAdjuster>();
        }

        if (rootAdjuster == null)
        {
            return;
        }

        switch (rootAdjuster.canvasType)
        {
            case CanvasAdjuster.CanvasType.Ipad:
                SetToIPad();
                break;
            case CanvasAdjuster.CanvasType.IphoneX:
                SetToIphoneX();
                break;
            default:
                SetToIPhone();
                break;
        }
        return;
#endif

        switch (DeviceType.GetDeviceType())
        {
            case DeviceType.Device.IphoneX:
                SetToIphoneX();
                break;
            case DeviceType.Device.Ipad:
                SetToIPad();
                break;
            default:
                SetToIPhone();
                break;
        }
    }

    private void SetToIphoneX()
    {
        iPhoneX.Apply(rectTransform, baseAnchoredPosition, baseSizeDelta);
        currentDeviceType = DeviceType.Device.IphoneX;
    }

    private void SetToIPad()
    {
        iPad.Apply(rectTransform, baseAnchoredPosition, baseSizeDelta);
        currentDeviceType = DeviceType.Device.Ipad;
    }

    private void SetToIPhone()
    {
        rectTransform.anchoredPosition = baseAnchoredPosition;
        rectTransform.sizeDelta = baseSizeDelta;
        rectTransform.localScale = Vector3.one;
        currentDeviceType = DeviceType.Device.Iphone;
    }

#if UNITY_EDITOR

    private void OnGUI()
    {
#if UNITY_EDITOR
        if (currentDeviceType == DeviceType.Device.Other)

        {
            SelectAndSet();
        }
#endif

        if (Application.isPlaying || !baseValuesSet)
            return;
        if (lastAnchoredPosition == rectTransform.anchoredPosition && lastSizeDelta == rectTransform.sizeDelta && lastScale == rectTransform.localScale)
            return;

        switch (currentDeviceType)
        {
            case DeviceType.Device.Iphone:
                serializedObject.FindProperty("baseAnchoredPosition").vector2Value = rectTransform.anchoredPosition;
                serializedObject.FindProperty("baseSizeDelta").vector2Value = rectTransform.sizeDelta;
                serializedObject.ApplyModifiedProperties();
                break;
            case DeviceType.Device.IphoneX:
                //    iPhoneXSerializedProperty.FindPropertyRelative("_additivePosition").vector2Value = rectTransform.anchoredPosition - baseAnchoredPosition;
                //    iPhoneXSerializedProperty.FindPropertyRelative("_additiveSize").vector2Value = rectTransform.sizeDelta - baseSizeDelta;
                //    iPhoneXSerializedProperty.FindPropertyRelative("_newScale").vector2Value = rectTransform.localScale;
                //    serializedObject.ApplyModifiedProperties();
                SetToIphoneX();
                break;
            case DeviceType.Device.Ipad:
                //iPadSerializedProperty.FindPropertyRelative("_additivePosition").vector2Value = rectTransform.anchoredPosition - baseAnchoredPosition;
                //iPadSerializedProperty.FindPropertyRelative("_additiveSize").vector2Value = rectTransform.sizeDelta - baseSizeDelta;
                //iPadSerializedProperty.FindPropertyRelative("_newScale").vector2Value = rectTransform.localScale;
                //serializedObject.ApplyModifiedProperties();
                SetToIPad();
                break;

        }

        lastAnchoredPosition = rectTransform.anchoredPosition;
        lastSizeDelta = rectTransform.sizeDelta;
    }

    private void OnAspectChange(CanvasAdjuster.CanvasType canvasType)
    {
        SelectAndSet();
    }

#endif
}