using UnityEngine;

namespace CastlesTrip.LayoutMorph
{
    public class RectTransformDeviceGroupLM
    {
        public RectTransformDeviceGroupLM(RectTransform rt)
        {
            _newScale = rt.localScale;
            _newRotation = rt.localRotation.eulerAngles.z;
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

        public void Apply(RectTransform rt, Vector2 basePosition, Vector2 baseSizeDelta)
        {
            rt.anchoredPosition = basePosition + AdditivePosition;
            rt.sizeDelta = baseSizeDelta + AdditiveSize;
            Vector3 scale = rt.localScale;
            rt.localScale = new Vector3(NewScale.x, NewScale.y, scale.z);
            Vector3 rotation = rt.localRotation.eulerAngles;
            rt.localRotation = Quaternion.Euler(rotation.x, rotation.y, NewRotation);
        }
    }
}