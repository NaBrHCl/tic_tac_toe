using System.Reflection.Metadata.Ecma335;
using static System.Console;

namespace Tic_Tac_Toe
{
    public enum Move
    {
        A1 = 0,
        B1,
        C1,
        A2,
        B2,
        C2,
        A3,
        B3,
        C3
    }

    public enum Status
    {
        O,
        X,
        Null
    }

    class Board
    {
        private static int[,] _winningLines = new int[8, 3]
        {
            { 0,1,2 },
            { 3,4,5 },
            { 5,6,7 },
            { 0,3,5 },
            { 1,4,6 },
            { 2,5,7 },
            { 0,4,7 },
            { 2,4,5 }
        };

        private const string HORIZONTAL_SEPARATOR = "---+---+---";
        private const string VERTICAL_SEPARATOR = "|";
        private const string PADDING = " ";

        private const string PLAYER_1 = "O";
        private const string PLAYER_2 = "X";
        private const string BLANK = " ";

        private const int COUNT_ROWS = 3;
        private const int COUNT_COLUMNS = 3;

        public bool IsPlayer1
        {
            get => IsPlayer1;
            private set => IsPlayer1 = value;
        }

        private Status[] _board = new Status[9];
        private int[] _horizontalPos = new int[COUNT_COLUMNS];
        private int[] _verticalPos = new int[COUNT_ROWS];
        private int _turnPos;

        public Board()
        {
            for (int i = 0; i < _board.Length; i++)
                _board[i] = Status.Null;
        }

        public static void Init()
        {

        }

        public void Print()
        {
            for (int i = 0; i < COUNT_ROWS * 2 - 1; i++)
            {
                if (i % 2 == 0)
                {
                    _verticalPos[(i + 1) / 2] = CursorTop;

                    Write(PADDING);

                    for (int j = 0; j < COUNT_COLUMNS - 1; j++)
                    {
                        if (i == 0)
                            _horizontalPos[j] = CursorLeft;

                        Write(BLANK);

                        Write(PADDING + VERTICAL_SEPARATOR + PADDING);
                    }

                    if (i == 0)
                        _horizontalPos[COUNT_COLUMNS - 1] = CursorLeft;

                    WriteLine(BLANK);
                }
                else
                {
                    WriteLine(HORIZONTAL_SEPARATOR);
                }
            }

            _turnPos = CursorTop + 1;

            UpdateTurn();
        }

        public void UpdateTurn()
        {
            CursorLeft = 0;
            CursorTop = _turnPos;

            string turnText = "Player " + ((IsPlayer1) ? "1" : "2") + "'s Turn";

            WriteLine(turnText + new string(' ', WindowWidth - turnText.Length));
        }

        public void GetOption()
        {
            Key input = Input.Get();

            if (input == Key.Cancel)
                return;

            while ()
            {

            }
        }

        public bool TryMove(Move move)
        {
            if (_board[(int)move] != Status.Null)
                return false;

            _board[(int)move] = (IsPlayer1) ? Status.O : Status.X;

            IsPlayer1 = !IsPlayer1;

            return true;
        }

        public Status CheckWin()
        {
            for (int i = 0; i < _winningLines.GetLength(0); i++)
            {
                bool isPlayer1Win = true;
                bool isPlayer2Win = true;

                for (int j = 0; j < _winningLines.GetLength(1); j++)
                {
                    if (_board[_winningLines[i, j]] == Status.Null)
                    {
                        isPlayer1Win = false;
                        isPlayer2Win = false;
                        break;
                    }

                    if (_board[_winningLines[i, j]] != Status.O)
                        isPlayer1Win = false;

                    if (_board[_winningLines[i, j]] != Status.X)
                        isPlayer2Win = false;

                }

                if (isPlayer1Win)
                    return Status.O;

                if (isPlayer2Win)
                    return Status.X;
            }

            return Status.Null;
        }
    }
}