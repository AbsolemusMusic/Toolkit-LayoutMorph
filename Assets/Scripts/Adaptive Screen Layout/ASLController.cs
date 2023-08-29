using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASLController : MonoBehaviour
{
    public Component[] GetASLComponents()
    {
        Component[] components = this.GetComponents(typeof(Component));
        // TODO: Filterred valid components
        return components;
    }
}
