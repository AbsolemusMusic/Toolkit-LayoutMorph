using System;
using UnityEngine;
using UnityEngine.UI;

namespace CastlesTrip.LayoutMorph
{
    [RequireComponent(typeof(CanvasScaler))]
    [ExecuteInEditMode]
    public class CanvasAdjusterLM : MonoBehaviour
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
        private float matchWOHIphoneX = 0;

        private CanvasScaler _scaler;
        private CanvasScaler scaler
        {
            get
            {
                if (!_scaler)
                    _scaler = GetComponent<CanvasScaler>();
                return _scaler;
            }
        }

        private bool IsIpad => DeviceType.GetDeviceType() == DeviceType.Device.Ipad;
        private bool IsIphoneX => DeviceType.GetDeviceType() == DeviceType.Device.IphoneX;
        private bool IsOther => DeviceType.GetDeviceType() != DeviceType.Device.Ipad &&
            DeviceType.GetDeviceType() != DeviceType.Device.IphoneX;

        private CanvasType canvasType { get; set; } = CanvasType.None;

        public Action<CanvasType> OnAspectChange = delegate { };


        private void Awake()
        {
            UpdateReferenceResolutions();
            SetCanvas();

#if UNITY_EDITOR
            if (!editorCanvas)
                return;

            SetCanvas(CanvasType.Other, referenceResolutionIphone, matchWOHIphone);
#endif
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
                return;
#endif

            if (canvasType != CanvasType.Ipad && IsIpad)
            {
                SetCanvas(CanvasType.Ipad, referenceResolutionIpad, matchWOHIpad);
            }
            else if (canvasType != CanvasType.IphoneX && IsIphoneX)
            {
                SetCanvas(CanvasType.IphoneX, referenceResolutionIphoneX, matchWOHIphoneX);
            }
            else if (canvasType != CanvasType.Other && IsOther)
            {
                SetCanvas(CanvasType.Other, referenceResolutionIphone, matchWOHIphone);
            }
        }

        private void SetCanvas(CanvasType type, Vector2 resolution, float matchWH)
        {
            canvasType = type;
            scaler.referenceResolution = resolution;
            scaler.matchWidthOrHeight = matchWH;
            OnAspectChange(canvasType);
        }

        private void UpdateReferenceResolutions()
        {
            if (referenceResolutionIphoneX == Vector2.zero)
                referenceResolutionIphoneX = scaler.referenceResolution;

            if (referenceResolutionIpad == Vector2.zero)
                referenceResolutionIpad = scaler.referenceResolution;

            if (referenceResolutionIphone == Vector2.zero)
                referenceResolutionIphone = scaler.referenceResolution;
        }
    }
}