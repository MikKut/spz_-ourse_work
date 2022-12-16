using NineMensMorris.Models;

namespace NineMensMorris.GameLogic
{
    internal static partial class CollectionOfLines
    {
        public static class Columns
        {
            public static readonly ButtonPosition[] First, Second, Third, Fourth_Left, Fourth_Right, Fifth, Sixth, Seventh;
            static Columns()
            {
                First = new ButtonPosition[] { ButtonPosition.a1, ButtonPosition.d1, ButtonPosition.g1 };
                Second = new ButtonPosition[] { ButtonPosition.b2, ButtonPosition.d2, ButtonPosition.f2 };
                Third = new ButtonPosition[] { ButtonPosition.c3, ButtonPosition.d3, ButtonPosition.e3 };
                Fourth_Left =  new ButtonPosition[] { ButtonPosition.a4, ButtonPosition.b4, ButtonPosition.c4 };
                Fourth_Right = new ButtonPosition[] { ButtonPosition.e4, ButtonPosition.f4, ButtonPosition.g4 };
                Fifth =  new ButtonPosition[] { ButtonPosition.c5, ButtonPosition.d5, ButtonPosition.e5 };
                Sixth =  new ButtonPosition[] { ButtonPosition.b6, ButtonPosition.d6, ButtonPosition.f6 };
                Seventh =  new ButtonPosition[] { ButtonPosition.a7, ButtonPosition.d7, ButtonPosition.g7 };
            }
        }
    
    }
}
