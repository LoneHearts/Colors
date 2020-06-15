using UnityEngine;

namespace ColorType

{
    public class ColorType
    {
        // 15
        public enum Type{Red        /*Batteuse*/,
                        Orange      /*AK*/,
                        Tangerine   /*MP5*/,
                        Yellow      /*Uzi*/,
                        Green       /*Sniper*/,
                        Lime        /*Scout*/,
                        Navy        /*Judge*/,
                        Blue        /*MAG5*/,
                        Cyan        /*Double Coup*/,
                        Purple      /*LANCE ROQUETTES*/,
                        Magenta     /*LASER*/,
                        Pink        /*LANCE FLAMMES*/,
                        Black       /*Rien*/,
                        Grey        /*Zombie*/,
                        White       /*Me*/};
        public static Color[] m_associatedColor =   {Color.red              /*Red*/,
                                                    new Color(1,0.315f,0)   /*Orange*/,
                                                    new Color(1,0.627f,0)   /*Tangerine*/,
                                                    Color.yellow            /*Yellow*/,
                                                    new Color(0,0.5f,0)     /*Green*/,
                                                    Color.green             /*Lime*/,
                                                    Color.blue              /*Navy*/,
                                                    new Color(0,0.5f,1)     /*Blue*/,
                                                    Color.cyan              /*Cyan*/,
                                                    new Color(0.5f,0,1)     /*Purple*/,
                                                    Color.magenta           /*Magenta*/,
                                                    new Color(1,0.5f,1)     /*Pink*/,
                                                    Color.black             /*Black*/,
                                                    Color.grey              /*Grey*/,
                                                    Color.white             /*White*/};

        public static float[] m_associatedFireRate =    {0.12f      /*Red*/,
                                                        0.20f       /*Orange*/, 
                                                        0.25f       /*Tangerine*/, 
                                                        0.12f       /*Yellow*/, 
                                                        0.6f        /*Green*/, 
                                                        0.7f        /*Lime*/, 
                                                        0.3f        /*Navy*/, 
                                                        0.7f        /*Blue*/, 
                                                        0.5f        /*Cyan*/, 
                                                        0.5f        /*Purple*/, 
                                                        0.5f        /*Magenta*/, 
                                                        0.5f        /*Pink*/, 
                                                        1f          /*Black*/, 
                                                        1f          /*Grey*/, 
                                                        0,8f        /*White*/};

        public static bool[] m_associatedAutomatic =    {true       /*Red*/, 
                                                        true        /*Orange*/, 
                                                        true        /*Tangerine*/, 
                                                        true        /*Yellow*/, 
                                                        false       /*Green*/, 
                                                        false       /*Lime*/, 
                                                        true        /*Navy*/, 
                                                        false       /*Blue*/, 
                                                        false       /*Cyan*/, 
                                                        true        /*Purple*/, 
                                                        true        /*Magenta*/, 
                                                        true        /*Pink*/, 
                                                        false       /*Black*/, 
                                                        false       /*Grey*/, 
                                                        false       /*White*/};
    
        public static int[] m_associatedAmmo = {70      /*Red*/, 
                                                25      /*Orange*/, 
                                                30      /*Tangerine*/, 
                                                20      /*Yellow*/, 
                                                10      /*Green*/, 
                                                5       /*Lime*/, 
                                                8       /*Navy*/, 
                                                5       /*Blue*/, 
                                                2       /*Cyan*/, 
                                                10      /*Purple*/, 
                                                10      /*Magenta*/, 
                                                10      /*Pink*/, 
                                                0       /*Black*/, 
                                                0       /*Grey*/, 
                                                5       /*White*/};

        public static float[] m_associatedSpread = {5f      /*Red*/, 
                                                    2.5f    /*Orange*/, 
                                                    4f      /*Tangerine*/, 
                                                    5f      /*Yellow*/, 
                                                    0f      /*Green*/, 
                                                    1f      /*Lime*/, 
                                                    12f     /*Navy*/, 
                                                    8f      /*Blue*/, 
                                                    20f     /*Cyan*/, 
                                                    0f      /*Purple*/, 
                                                    0f      /*Magenta*/, 
                                                    0f      /*Pink*/, 
                                                    0f      /*Black*/, 
                                                    0f      /*Grey*/, 
                                                    0.5f    /*White*/};

        

    }

}