using System;
using UnityEngine;

namespace CastlesTrip.LayoutMorph
{
    [Serializable]
    public class RectTransformBaseValuesLM : BaseValuesLM
    {
        [SerializeField]
        private Vector2 baseAnchoredPosition;
        public Vector2 BaseAnchoredPosition => baseAnchoredPosition;

        [SerializeField]
        private Vector2 baseSizeDelta;
        public Vector2 BaseSizeDelta => baseSizeDelta;

        [SerializeField]
        private float baseRotation;
        public float BaseRotation => baseRotation;
    }
}