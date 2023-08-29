using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AdaptiveScreenLayout : MonoBehaviour
{
    public virtual void OnEnable()
    {

    }

    public virtual void OnDisable()
    {
        
    }
}

[RequireComponent(typeof(LayoutGroup))]
public class GridLayoutGroupSAL : AdaptiveScreenLayout
{
    private GridLayoutGroup gridLG;

    [SerializeField]
    private LayoutGroupASLDG baseValues = new LayoutGroupASLDG();

    [SerializeField]
    private LayoutGroupASLDG iPhoneX = new LayoutGroupASLDG();


    public override void OnEnable()
    {
        base.OnEnable();

        gridLG = GetComponent<GridLayoutGroup>();

        Init();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    private void Init()
    {
        baseValues.padding.left = gridLG.padding.left;
        baseValues.padding.right = gridLG.padding.right;
        baseValues.padding.top = gridLG.padding.top;
        baseValues.padding.bottom = gridLG.padding.bottom;

        baseValues.cellSize = gridLG.cellSize;

        baseValues.spacing = gridLG.spacing;
    }
}



[Serializable]
public class ASLDeviceGroup
{

}

[Serializable]
public class LayoutGroupASLDG : ASLDeviceGroup
{
    public RectOffset padding = new RectOffset();

    public Vector2 cellSize;

    public Vector2 spacing;
}
