using UnityEditor;
using UnityEngine;

namespace CastlesTrip.LayoutMorph
{
    public class VerticalLayoutGroupPropertiesChangerLM : MonoBehaviour
    {
        private SerializedObject serializedObject;

        public VerticalLayoutGroupPropertiesChangerLM(SerializedObject serializedObject)
        {
            this.serializedObject = serializedObject;
        }

        public VerticalLayoutGroupPropertiesChangerLM SetPadding()
        {
            return this;
        }

        public VerticalLayoutGroupPropertiesChangerLM SetSpacing(float spacing)
        {
            return this;
        }
    }
}