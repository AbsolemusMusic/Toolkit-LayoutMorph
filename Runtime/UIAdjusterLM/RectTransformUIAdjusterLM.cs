using UnityEditor;
using UnityEngine;

namespace CastlesTrip.LayoutMorph
{
    [RequireComponent(typeof(RectTransform))]
    public class RectTransformUIAdjusterLM : UIAdjusterLM
    {
        [SerializeField]
        private RectTransformDeviceGroupLM iPhoneX;

        [SerializeField]
        private RectTransformDeviceGroupLM iPad;

        [SerializeField]
        private RectTransformBaseValuesLM baseValues;

        private RectTransformPropertiesChangerLM properties;


        private RectTransform _rectTransform;
        private RectTransform rectTransform
        {
            get
            {
                if (!_rectTransform)
                    _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }

#if UNITY_EDITOR

        private Vector2 lastAnchoredPosition, lastSizeDelta;

        private Vector3 lastScale;

        private SerializedObject serializedObject;
        public override SerializedObject SerializedObject
        {
            get
            {
                if (serializedObject == null)
                    serializedObject = new SerializedObject(this);
                return serializedObject;
            }
        }

        public override bool IsEqualsValues
        {
            get
            {
                bool isAnchored = lastAnchoredPosition == rectTransform.anchoredPosition;
                bool isSizeDelta = lastSizeDelta == rectTransform.sizeDelta;
                bool isScale = lastScale == rectTransform.localScale;
                return isAnchored && isSizeDelta && isScale;
            }
        }

        [SerializeField]
        private bool baseValuesSet;
        public override bool BaseValuesSet => baseValuesSet;
#endif

        public override void OnEnable()
        {
            base.OnEnable();
            properties = new RectTransformPropertiesChangerLM(SerializedObject);
#if UNITY_EDITOR

            SetBaseValue();
#endif

            SelectAndSet();
        }

        public override void SetBaseValue()
        {
            if (BaseValuesSet)
                return;

            properties.SetAnchoredPosition(rectTransform.anchoredPosition)
                .SetSizeDelta(rectTransform.sizeDelta)
                .SetRotation(rectTransform.localRotation.eulerAngles);
            base.SetBaseValue();
        }

        public override void SelectAndSet()
        {
            base.SelectAndSet();
#if UNITY_EDITOR
            if (rootAdjuster == null)
            {
                rootAdjuster = transform.root.GetComponent<CanvasAdjusterLM>();
            }

            if (rootAdjuster == null)
            {
                return;
            }

            switch (rootAdjuster.canvasType)
            {
                case CanvasType.Ipad:
                    SetToIPad();
                    break;
                case CanvasType.IphoneX:
                    SetToIphoneX();
                    break;
                default:
                    SetToIPhone();
                    break;
            }
            return;
#endif

            switch (Device.GetDeviceType())
            {
                case DeviceType.IphoneX:
                    SetToIphoneX();
                    break;
                case DeviceType.Ipad:
                    SetToIPad();
                    break;
                default:
                    SetToIPhone();
                    break;
            }
        }

        public override void SetToIphoneX()
        {
            iPhoneX.Apply(rectTransform, baseValues);
            base.SetToIphoneX();
        }

        public override void SetToIPad()
        {
            iPad.Apply(rectTransform, baseValues);
            base.SetToIPad();
        }

        public override void SetToIPhone()
        {
            rectTransform.anchoredPosition = baseValues.BaseAnchoredPosition;
            rectTransform.sizeDelta = baseValues.BaseSizeDelta;
            Vector3 euler = rectTransform.localRotation.eulerAngles;
            rectTransform.localRotation = Quaternion.Euler(euler.x, euler.y, baseValues.BaseRotation);
            rectTransform.localScale = Vector3.one;
            base.SetToIPhone();
        }

#if UNITY_EDITOR

        public override void OnGUI()
        {
            base.OnGUI();
            if (CurrentDeviceType == DeviceType.Other)
                SelectAndSet();

            if (IsBreakSet && IsEqualsValues)
                return;

            switch (CurrentDeviceType)
            {
                case DeviceType.Iphone:
                    properties.SetAnchoredPosition(rectTransform.anchoredPosition)
                        .SetSizeDelta(rectTransform.sizeDelta)
                        .SetRotation(rectTransform.localRotation.eulerAngles);
                    SerializedObject.ApplyModifiedProperties();
                    break;
                case DeviceType.IphoneX:
                    SetToIphoneX();
                    break;
                case DeviceType.Ipad:
                    SetToIPad();
                    break;

            }

            lastAnchoredPosition = rectTransform.anchoredPosition;
            lastSizeDelta = rectTransform.sizeDelta;
        }
#endif
    }
}
