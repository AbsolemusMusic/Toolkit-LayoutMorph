using UnityEngine;

namespace CastlesTrip.LayoutMorph
{
    public class Device : MonoBehaviour
    {
        public DeviceType device;

        private static float lastWidth, lastHeight;
        private static DeviceType lastDevice = DeviceType.Other;

        private static readonly Vector2 iPadSize = new Vector2(4f, 3f);
        private static readonly Vector2 iPhoneSize = new Vector2(16f, 9f);

        private static bool Approximately(float aspect, Vector2 size, float eps = 0.1f)
        {
            return Approximately(aspect, size.x / size.y) || Approximately(aspect, size.y / size.x);
        }

        private static bool Approximately(float aspect, float target, float eps = 0.1f)
        {
            return Mathf.Abs(aspect - target) < eps;
        }

        public static DeviceType GetDeviceType()
        {
            float width = Screen.width;
            float height = Screen.height;

            if (width == lastWidth && height == lastHeight)
                return lastDevice;

            lastWidth = width;
            lastHeight = height;

            DeviceType type = GetDeviceType(width, height);
            if (type == DeviceType.Other)
                return type;

            lastDevice = type;
            return type;
        }

        private static DeviceType GetDeviceType(float width, float height)
        {
            float aspect = width / height;

            if (Approximately(aspect, iPadSize))
            {
                return DeviceType.Ipad;
            }
            else if (Approximately(aspect, iPhoneSize))
            {
                return DeviceType.Iphone;
            }
            else if (width > 2f * height || height > 2f * width)
            {
                return DeviceType.IphoneX;
            }

            return DeviceType.Other;
        }
    }
}
