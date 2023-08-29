using System;
using UnityEngine;

[Serializable]
public class DeviceGroup
{
    public DeviceGroup()
    {
        _newScale = Vector3.one;
    }

    [SerializeField]
    private Vector2 _additivePosition;
    public Vector2 AdditivePosition
    {
        get
        {
            return _additivePosition;
        }
    }

    [SerializeField]
    private Vector2 _newScale;
    public Vector2 NewScale
    {
        get
        {
            return _newScale;
        }
    }

    [SerializeField]
    private Vector2 _additiveSize;
    public Vector2 AdditiveSize
    {
        get
        {
            return _additiveSize;
        }
    }

    public void Apply(RectTransform rectTransform)
    {
        rectTransform.anchoredPosition += AdditivePosition;
        rectTransform.sizeDelta += AdditiveSize;
        rectTransform.localScale = new Vector3(NewScale.x, NewScale.y, 1);
    }

    public void Apply(RectTransform rectTransform, Vector2 basePosition, Vector2 baseSizeDelta)
    {
        rectTransform.anchoredPosition = basePosition + AdditivePosition;
        rectTransform.sizeDelta = baseSizeDelta + AdditiveSize;
        rectTransform.localScale = new Vector3(NewScale.x, NewScale.y, 1);
    }
}