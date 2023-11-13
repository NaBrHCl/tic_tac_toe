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
                case Option.PVE: PVE(); break;
                case Option.Exit: return;
            }

        }

        static void PVP()
        {
            const string NAME_1 = "Player 1";
            const string NAME_2 = "Player 2";

            Board board = new Board();
            Status status;

            board.Print();
            board.UpdateTurn(NAME_1, NAME_2);

            do
            {
                if (board.GetOption())
                    Environment.Exit(0);

                board.UpdateTurn(NAME_1, NAME_2);

                status = board.CheckWin();
            }
            while (status == Status.Null);

            WriteLine();

            if (status == Status.Draw)
                WriteLine("Draw!");
            else
                WriteLine(status + " wins!");

            Thread.Sleep(2000);
        }

        static void PVE()
        {
            const string NAME_1 = "Computer";
            const string NAME_2 = "Player";

            Board board = new Board();
            Status status;

            board.Print();

            board.GetAIOption();

            board.UpdateTurn(NAME_1, NAME_2);

            do
            {
                if (!board.IsPlayer1)
                {
                    if (board.GetOption())
                        Environment.Exit(0);
                }
                else
                    board.GetAIOption();

                board.UpdateTurn(NAME_1, NAME_2);

                status = board.CheckWin();
            }
            while (status == Status.Null);

            WriteLine();

            if (status == Status.Draw)
                WriteLine("Draw!");
            else
                WriteLine(status + " wins!");

            Thread.Sleep(2000);
        }
    }
}