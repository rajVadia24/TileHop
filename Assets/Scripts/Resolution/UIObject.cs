using UnityEngine;
using System.Collections;

namespace Yudiz.Gaming.Engine
{
    [System.Serializable]
    public class UIObject
    {
        public GameObject
            ObjectToSet;
        public SettingOption Option;
        public SettingMode Mode;
        public AxisToSet Axis;
        public float SetMultiplier = 1;
        public bool IsNotVisible;

        public float Aspect;
        public Visiblemode visiblemode;

        public UIObject()
        {
        }
    }


    public enum SettingOption
    {
        None = 0,
        Position = 1,
        Rotation = 2,
        Scale = 3,
        CameraFoV = 4

    }

    public enum SettingMode
    {
        Addition = 0,
        Subtraction = 1
    }

    public enum AxisToSet
    {
        X = 0,
        Y = 1,
        Z = 2,
        XY = 3,
        XZ = 4,
        YZ = 5,
        XYZ = 6
    }

    public enum Visiblemode
    {
        Greter = 0,
        less = 1
    }
}