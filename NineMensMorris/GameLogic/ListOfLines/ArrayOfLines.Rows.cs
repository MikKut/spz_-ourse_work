using NineMensMorris.Models;

namespace NineMensMorris.GameLogic
{
    internal static partial class CollectionOfLines
    {
        //private static void 
        //Event r
        public static class Rows
        {
            public readonly static ButtonPosition[] A, B, C, D_Upper, D_Lower, E, F, G;
            static Rows()
            {
                A = new ButtonPosition[] { ButtonPosition.a1, ButtonPosition.a4, ButtonPosition.a7 };
                B = new ButtonPosition[] { ButtonPosition.b2, ButtonPosition.b4, ButtonPosition.b6 };
                C = new ButtonPosition[] { ButtonPosition.c3, ButtonPosition.c4, ButtonPosition.c5 };
                D_Upper =  new ButtonPosition[] { ButtonPosition.d1, ButtonPosition.d2, ButtonPosition.d3 };
                D_Lower = new ButtonPosition[] { ButtonPosition.d5, ButtonPosition.d6, ButtonPosition.d7};
                E =  new ButtonPosition[] { ButtonPosition.e3, ButtonPosition.e4, ButtonPosition.e5 };
                F =  new ButtonPosition[] { ButtonPosition.f2, ButtonPosition.f4, ButtonPosition.f6 };
                G =  new ButtonPosition[] { ButtonPosition.g1, ButtonPosition.g4, ButtonPosition.g7 };
            }

        }
            
    
    }
}
