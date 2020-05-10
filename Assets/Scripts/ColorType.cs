using UnityEngine;

namespace ColorType

{
    public class ColorType : MonoBehaviour
    {
        // 9 
        public enum Type{Blue,Cyan,Green,Magenta,Red,Yellow,Black,Grey,White};
        public static Color[] m_associatedColor = {Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.yellow, Color.black, Color.grey, Color.white};

        public static float[] m_associatedFireRate = {0.15f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.1f};

        public static bool[] m_associatedAutomatic = {true, false, false, false, false, false, false, false, false};
    }

}