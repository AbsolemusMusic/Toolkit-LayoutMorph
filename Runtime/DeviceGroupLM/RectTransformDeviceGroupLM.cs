using System;
using UnityEngine;

namespace CastlesTrip.LayoutMorph
{
    [Serializable]
    public class RectTransformDeviceGroupLM : DeviceGroupLM
    {
        public RectTransformDeviceGroupLM()
        {
            _newScale = Vector2.one;
            _newRotation = 0f;
        }

        [SerializeField]
        private Vector2 _additivePosition;
        public Vector2 AdditivePosition => _additivePosition;

        [SerializeField]
        private Vector2 _newScale;
        public Vector2 NewScale => _newScale;

        [SerializeField]
        private Vector2 _additiveSize;
        public Vector2 AdditiveSize => _additiveSize;

        [SerializeField]
        private float _newRotation;
        public float NewRotation => _newRotation;

        public void Apply(RectTransform rt, RectTransformBaseValuesLM baseValues)
        {
            rt.anchoredPosition = baseValues.BaseAnchoredPosition + AdditivePosition;
            rt.sizeDelta = baseValues.BaseSizeDelta + AdditiveSize;
            Vector3 scale = rt.localScale;
            rt.localScale = new Vector3(NewScale.x, NewScale.y, scale.z);
            Vector3 rotation = rt.localRotation.eulerAngles;
            rt.localRotation = Quaternion.Euler(rotation.x, rotation.y, NewRotation);
        }
    }
}