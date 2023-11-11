using System.Runtime.CompilerServices;
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

            Option option = Menu.GetOption();

            Clear();

            Board.Init();

            switch (option)
            {
                case Option.PVP: PVP(); break;
                case Option.PVE: break;
                case Option.Exit: return;
            }

        }

        static void PVP()
        {
            Board board = new Board();

            board.Print();

            while (board.CheckWin() == Status.Null)
            {
                board.Print();

                board.UpdateTurn();


            }
        }
    }
}