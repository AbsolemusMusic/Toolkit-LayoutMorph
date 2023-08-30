using System;
using UnityEngine;

namespace CastlesTrip.LayoutMorph
{
    [Serializable]
    public class VerticalLayoutGroupBaseValuesLM : BaseValuesLM
    {
        [SerializeField]
        private RectOffset basePadding;
        public RectOffset BasePadding => basePadding;

        [SerializeField]
        private float spacing;
        public float Spacing => spacing;
    }
}