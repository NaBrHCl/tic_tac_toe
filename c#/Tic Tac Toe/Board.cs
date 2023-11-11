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
        Player1,
        Player2,
        Null,
        Draw
    }

    class Board
    {
        private static int[,] _winningLines = new int[8, 3]
        {
            { 0,1,2 },
            { 3,4,5 },
            { 6,7,8 },
            { 0,3,6 },
            { 1,4,7 },
            { 2,5,8 },
            { 0,4,8 },
            { 2,4,6 }
        };

        private const string HORIZONTAL_SEPARATOR = "---+---+---";
        private const string VERTICAL_SEPARATOR = "|";
        private const string PADDING = " ";

        private const string PLAYER_1 = "O";
        private const string PLAYER_2 = "X";
        private const string BLANK = " ";

        private const int COUNT_ROWS = 3;
        private const int COUNT_COLUMNS = 3;

        private const int CURSOR_VERTICAL_DISPLACEMENT = 3;
        private const int CURSOR_HORIZONTAL_DISPLACEMENT = 1;

        private const ConsoleColor HIGHLIGHT_COLOR = ConsoleColor.DarkGray;

        public bool IsPlayer1
        {
            get;
            private set;
        }

        private Status[] _board = new Status[9];
        private int[] _horizontalPos = new int[COUNT_COLUMNS];
        private int[] _verticalPos = new int[COUNT_ROWS];
        private int _turnPos;

        private int _currentCursorPos;
        private int _previousCursorPos;

        public int CurrentCursorX
        {
            get => _currentCursorPos % 3;
        }

        public int CurrentCursorY
        {
            get => _currentCursorPos / 3;
        }

        public int PreviousCursorX
        {
            get => _previousCursorPos % 3;
        }

        public int PreviousCursorY
        {
            get => _previousCursorPos / 3;
        }

        public Board()
        {
            for (int i = 0; i < _board.Length; i++)
                _board[i] = Status.Null;

            _currentCursorPos = (int)Math.Floor((double)(COUNT_COLUMNS * COUNT_ROWS) / 2);
            _previousCursorPos = _currentCursorPos;

            IsPlayer1 = true;
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

        public bool GetOption()
        {
            _currentCursorPos = (int)Math.Floor((double)(COUNT_COLUMNS * COUNT_ROWS) / 2);
            _previousCursorPos = _currentCursorPos;

            PrintCurrentCursor();

            while (true)
            {
                Key input = Input.Get();

                switch (input)
                {
                    case Key.Left: TryMoveCursor(-CURSOR_HORIZONTAL_DISPLACEMENT); break;
                    case Key.Right: TryMoveCursor(CURSOR_HORIZONTAL_DISPLACEMENT); break;
                    case Key.Up: TryMoveCursor(-CURSOR_VERTICAL_DISPLACEMENT); break;
                    case Key.Down: TryMoveCursor(CURSOR_VERTICAL_DISPLACEMENT); break;
                    case Key.Confirm:
                        if (TryPlacement((Move)_currentCursorPos))
                        {
                            return false;
                        }
                        break;
                    case Key.Cancel: return true;
                }
            }
        }

        private string GetStatusText(int x, int y)
        {
            return (_board[y * COUNT_COLUMNS + x] == Status.Null) ? BLANK : (_board[y * COUNT_COLUMNS + x] == Status.Player1) ? PLAYER_1 : PLAYER_2;
        }

        private void PrintCurrentCursor()
        {
            CursorLeft = _horizontalPos[CurrentCursorX];
            CursorTop = _verticalPos[CurrentCursorY];

            BackgroundColor = HIGHLIGHT_COLOR;

            Write(GetStatusText(CurrentCursorX, CurrentCursorY));

            ResetColor();
        }

        private void ClearPreviousCursor()
        {
            CursorLeft = _horizontalPos[PreviousCursorX];
            CursorTop = _verticalPos[PreviousCursorY];

            Write(GetStatusText(PreviousCursorX, PreviousCursorY));
        }

        private void TryMoveCursor(int displacement)
        {
            if (!Enum.IsDefined(typeof(Move), _currentCursorPos + displacement))
                return;

            _currentCursorPos += displacement;

            ClearPreviousCursor();
            PrintCurrentCursor();

            _previousCursorPos = _currentCursorPos;
        }

        private bool TryPlacement(Move move)
        {
            if (_board[(int)move] != Status.Null)
                return false;

            _board[(int)move] = (IsPlayer1) ? Status.Player1 : Status.Player2;

            CursorLeft = _horizontalPos[CurrentCursorX];
            CursorTop = _verticalPos[CurrentCursorY];

            Write((IsPlayer1) ? PLAYER_1 : PLAYER_2);

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

                    if (_board[_winningLines[i, j]] != Status.Player1)
                        isPlayer1Win = false;

                    if (_board[_winningLines[i, j]] != Status.Player2)
                        isPlayer2Win = false;

                }

                if (isPlayer1Win)
                    return Status.Player1;

                if (isPlayer2Win)
                    return Status.Player2;
            }

            foreach (Status spot in _board)
            {
                if (spot == Status.Null)
                    return Status.Null;
            }

            return Status.Draw;
        }
    }
}