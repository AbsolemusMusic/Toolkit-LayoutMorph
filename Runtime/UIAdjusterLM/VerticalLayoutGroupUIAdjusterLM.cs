using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CastlesTrip.LayoutMorph
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class VerticalLayoutGroupUIAdjusterLM : UIAdjusterLM
    {
        private VerticalLayoutGroup _verticalLayoutGroup;
        private VerticalLayoutGroup verticalLayoutGroup
        {
            get
            {
                if (!_verticalLayoutGroup)
                    _verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
                return _verticalLayoutGroup;
            }
        }

        [SerializeField]
        private VerticalLayoutGroupDeviceGroupLM iPhoneX;

        [SerializeField]
        private VerticalLayoutGroupDeviceGroupLM iPad;

        [Space]

        [SerializeField]
        private RectOffset basePadding = new RectOffset();
        [SerializeField]
        private float baseSpacing;

        [Space]

        [SerializeField]
        private bool baseValuesSet;
        public override bool BaseValuesSet => baseValuesSet;

        private RectOffset lastPadding = new RectOffset();
        private float lastSpacing;

        public override bool IsEqualsValues
        {
            get
            {
                bool isLeft = lastPadding.left == verticalLayoutGroup.padding.left;
                bool isRight = lastPadding.right == verticalLayoutGroup.padding.right;
                bool isTop = lastPadding.top == verticalLayoutGroup.padding.top;
                bool isBottom = lastPadding.bottom == verticalLayoutGroup.padding.bottom;
                bool isSpacing = lastSpacing == verticalLayoutGroup.spacing;
                return isLeft && isRight && isTop && isBottom && isSpacing;
            }
        }

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

        public override void OnEnable()
        {
            base.OnEnable();

            // TODO: Create properties
#if UNITY_EDITOR

            SetBaseValue();
#endif
        }

        public override void SetBaseValue()
        {
            if (BaseValuesSet)
                return;

            basePadding.left = verticalLayoutGroup.padding.left;
            basePadding.right = verticalLayoutGroup.padding.right;
            basePadding.top = verticalLayoutGroup.padding.top;
            basePadding.bottom = verticalLayoutGroup.padding.bottom;
            baseSpacing = verticalLayoutGroup.spacing;

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
            iPhoneX.Apply(verticalLayoutGroup);
            base.SetToIphoneX();
        }

        public override void SetToIPad()
        {
            iPad.Apply(verticalLayoutGroup);
            base.SetToIPad();
        }

        public override void SetToIPhone()
        {
            verticalLayoutGroup.padding.left = basePadding.left;
            verticalLayoutGroup.padding.right = basePadding.right;
            verticalLayoutGroup.padding.top = basePadding.top;
            verticalLayoutGroup.padding.bottom = basePadding.bottom;

            verticalLayoutGroup.spacing = baseSpacing;
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
                case DeviceType.IphoneX:
                    SetToIphoneX();
                    break;
                case DeviceType.Ipad:
                    SetToIPad();
                    break;
            }

            lastPadding.left = verticalLayoutGroup.padding.left;
            lastPadding.right = verticalLayoutGroup.padding.right;
            lastPadding.top = verticalLayoutGroup.padding.top;
            lastPadding.bottom = verticalLayoutGroup.padding.bottom;

            lastSpacing = verticalLayoutGroup.spacing;
        }
#endif
    }
}