using UnityEngine;

namespace ColorType

{
    public class ColorType
    {
        // 15
        public enum Type{Red,Orange,Tangerine,Yellow,Green,Lime,Navy,Blue,Cyan,Purple,Magenta,Pink,Black,Grey,White};
        public static Color[] m_associatedColor = {Color.red,new Color(1,0.315f,0),new Color(1,0.627f,0),Color.yellow,new Color(0,0.5f,0),Color.green,Color.blue,new Color(0,0.5f,1),Color.cyan,new Color(0.5f,0,1),Color.magenta,new Color(1,0.5f,1),Color.black,Color.grey,Color.white};

        public static float[] m_associatedFireRate = {0.12f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0,5f};

        public static bool[] m_associatedAutomatic = {true, false, false, false, false, false, false, false, false, false, false, false, false, false, false};
    
        public static int[] m_associatedAmmo = {20, 1, 1, 1, 5, 1, 1, 1, 6, 6, 6, 6, 6, 6, 6};

    }

}