using UnityEditor;
using UnityEngine;

namespace CastlesTrip.LayoutMorph
{
    public class RectTransformPropertiesChangerLM
    {
        private SerializedObject serializedObject;

        public readonly string Base = "baseValues";
        public readonly string AnchoredPosition = "baseAnchoredPosition";
        public readonly string SizeDelta = "baseSizeDelta";
        public readonly string Rotation = "baseRotation";

        public RectTransformPropertiesChangerLM(SerializedObject serializedObject)
        {
            this.serializedObject = serializedObject;
        }

        public RectTransformPropertiesChangerLM SetAnchoredPosition(Vector2 anchoredPosition)
        {
            serializedObject.FindProperty(Base)
                .FindPropertyRelative(AnchoredPosition)
                .vector2Value = anchoredPosition;
            return this;
        }

        public RectTransformPropertiesChangerLM SetSizeDelta(Vector2 sizeDelta)
        {
            serializedObject.FindProperty(Base)
                .FindPropertyRelative(SizeDelta)
                .vector2Value = sizeDelta;
            return this;
        }

        public RectTransformPropertiesChangerLM SetRotation(Vector3 rotation)
        {
            serializedObject.FindProperty(Base)
                .FindPropertyRelative(Rotation)
                .floatValue = rotation.z;
            return this;
        }
    }
}
