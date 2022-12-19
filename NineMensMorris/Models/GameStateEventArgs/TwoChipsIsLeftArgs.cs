using System;

namespace NineMensMorris.Models.GameStateEventArgs
{
    public class TwoChipsIsLeftArgs : EventArgs
    {
        public Turn itsTurn;

        public TwoChipsIsLeftArgs(Turn itsTurn)
        {
            this.itsTurn=itsTurn;
        }
    }
}
