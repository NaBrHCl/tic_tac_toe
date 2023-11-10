using static System.Console;

namespace Tic_Tac_Toe
{
    public enum Option
    {
        PVP,
        PVE,
        Exit
    }

    public enum Key
    {
        Up = 0,
        Down,
        Left,
        Right,
        Confirm,
        Cancel,
        Null
    }

    class Game
    {
        static void Main(string[] args)
        {
            CursorVisible = false;

            switch (Menu.GetOption())
            {
                case Option.PVP: break;
                case Option.PVE: break;
                case Option.Exit: return;
            }
        }
    }
}