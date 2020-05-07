using UnityEngine;

namespace ColorType

{
    public class ColorType : MonoBehaviour
    {
        public enum Type{Blue,Cyan,Green,Magenta,Red,Yellow,Black,Grey,White};
        public static Color[] m_associatedColor = {Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.yellow, Color.black, Color.grey, Color.white};
    }

}