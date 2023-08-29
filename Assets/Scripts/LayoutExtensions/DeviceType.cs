using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceType : MonoBehaviour
{

    public enum Device
    {
        Iphone,
        IphoneX,
        Ipad,
        Amazon,
        Other
    }
    public Device device;

    private static bool Approximately(float a, float b, float eps = 0.1f)
    {
        return Mathf.Abs(a - b) < eps;
    }

    private static int lastWidth, lastHeight;
    private static Device lastDevice = Device.Other;

    public static Device GetDeviceType()
    {
        int width = Screen.width;
        int height = Screen.height;

        if (width == lastWidth && height == lastHeight)
        {
            return lastDevice;
        }

        lastWidth = width;
        lastHeight = height;

        float value = (float)width / (float)height;

        if (Approximately(value, 4f / 3f) || Approximately(value, 3f / 4f))
        {
            lastDevice = Device.Ipad;
            return Device.Ipad;
        }
        else if (Approximately(value, 16f / 9f) || Approximately(value, 9f / 16f))
        {
            lastDevice = Device.Iphone;
            return Device.Iphone;
        }
        else if (width > 2f * height || height > 2f * width)
        {
            lastDevice = Device.IphoneX;
            return Device.IphoneX;
        }

        //return Device.Iphone;
        return Device.Other;
    }
}
