using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestComponents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Component[] components = gameObject.GetComponents(typeof(Component));

        foreach (Component component in components)
        {
            if (component.GetType() == typeof(RectTransform))
            {
                gameObject.AddComponent<RectASL>();
            }

            if (component.GetType() == typeof(Image))
            {
                gameObject.AddComponent<ImageASL>();
            }
        }
    }
}

[Serializable]
public class ComponentASL : MonoBehaviour
{

}

[Serializable]
public class RectASL : ComponentASL
{
    [SerializeField]
    public Vector3 position;
}

[Serializable]
public class ImageASL : ComponentASL
{
    [SerializeField]
    public Color color;
}