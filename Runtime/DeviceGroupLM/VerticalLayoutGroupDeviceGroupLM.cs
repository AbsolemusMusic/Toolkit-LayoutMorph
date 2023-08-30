using System;
using UnityEngine;
using UnityEngine.UI;

namespace CastlesTrip.LayoutMorph
{
    [Serializable]
    public class VerticalLayoutGroupDeviceGroupLM : DeviceGroupLM
    {
        [SerializeField]
        private RectOffset _padding;
        public RectOffset Padding => _padding;

        [SerializeField]
        private float _spacing;
        public float Spacing => _spacing;

        public void Apply(VerticalLayoutGroup group)
        {
            group.padding = new RectOffset(Padding.left, Padding.right, Padding.top, Padding.bottom);
            group.spacing = Spacing;
        }
    }
}