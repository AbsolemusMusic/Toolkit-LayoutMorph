using UnityEditor;
using UnityEngine;

namespace CastlesTrip.LayoutMorph
{
    [ExecuteInEditMode]
    public abstract class UIAdjusterLM : MonoBehaviour
    {
        public abstract bool BaseValuesSet { get; }

        public CanvasAdjusterLM rootAdjuster;

        public DeviceType CurrentDeviceType { get; private set; } = DeviceType.Other;

        public virtual bool IsBreakSet
        {
            get
            {
                return Application.isPlaying || !BaseValuesSet;
            }
        }

        public abstract bool IsEqualsValues { get; }

#if UNITY_EDITOR
        public abstract SerializedObject SerializedObject { get; }
#endif

        public virtual void OnEnable()
        {
#if UNITY_EDITOR
            rootAdjuster = transform.root.GetComponent<CanvasAdjusterLM>();
            if (rootAdjuster != null)
            {
                rootAdjuster.OnAspectChange += OnAspectChange;
            }
#endif
        }

        public virtual void OnDisable()
        {
#if UNITY_EDITOR
            if (rootAdjuster != null)
                rootAdjuster.OnAspectChange -= OnAspectChange;
#endif
        }


        public virtual void OnAspectChange(CanvasType canvasType)
        {
            SelectAndSet();
        }

        public virtual void SelectAndSet()
        {

        }

        public virtual void OnGUI()
        {
            //switch (CurrentDeviceType)
            //{
            //    case DeviceType.Iphone:
            //        SerializedObject.FindProperty("baseAnchoredPosition").vector2Value = rectTransform.anchoredPosition;
            //        SerializedObject.FindProperty("baseSizeDelta").vector2Value = rectTransform.sizeDelta;
            //        SerializedObject.ApplyModifiedProperties();
            //        break;
            //    case DeviceType.IphoneX:
            //        SetToIphoneX();
            //        break;
            //    case DeviceType.Ipad:
            //        SetToIPad();
            //        break;

            //}
        }


        public virtual void SetToIphoneX()
        {
            CurrentDeviceType = DeviceType.IphoneX;
        }

        public virtual void SetToIPad()
        {
            CurrentDeviceType = DeviceType.Ipad;
        }

        public virtual void SetToIPhone()
        {
            CurrentDeviceType = DeviceType.Iphone;
        }

        public virtual void SetBaseValue()
        {
            var properties = new Properties();
            SerializedObject.FindProperty(properties.ValueSet).boolValue = true;
            SerializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
