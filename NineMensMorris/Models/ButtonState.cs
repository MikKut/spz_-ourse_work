namespace NineMensMorris.Models
{
    public struct ButtonState
    {
        public ButtonState(ButtonPosition position)
        {
            buttonPosition = position;
        }
        public readonly ButtonPosition buttonPosition;
        public ChipState state = ChipState.None;
    }
}
