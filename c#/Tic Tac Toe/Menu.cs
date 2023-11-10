using static System.Console;

namespace Tic_Tac_Toe
{
    static class Menu
    {
        public const string LEFT_CURSOR = "->";
        public const string RIGHT_CURSOR = "<-";
        public const int CURSOR_GAP = 10;

        private static int pvpCursorPos;
        private static int pveCursorPos;
        private static int exitCursorPos;

        private static int cursorIndent;
        private static Option currentOption = Option.PVP;
        private static Option previousOption;

        public static Option GetOption()
        {
            Display();

            while (true)
            {
                UpdateCursor();

                previousOption = currentOption;

                switch (Input.Get())
                {
                    case Key.Left:
                    case Key.Up:
                        if (Enum.IsDefined(typeof(Option), currentOption - 1))
                            currentOption--;
                        break;

                    case Key.Right:
                    case Key.Down:
                        if (Enum.IsDefined(typeof(Option), currentOption + 1))
                            currentOption++;
                        break;

                    case Key.Confirm:
                        return currentOption;
                        break;

                    case Key.Cancel:
                        return Option.Exit;
                        break;
                }
            }
        }

        private static void Display()
        {
            Write("    TIC-TAC-TOE\n\n\t");
            cursorIndent = CursorLeft * 3 / 5;
            pvpCursorPos = CursorTop;
            Write("PVP\n\t");
            pveCursorPos = CursorTop;
            Write("PVE\n\t");
            exitCursorPos = CursorTop;
            Write("EXIT");
        }

        private static void UpdateCursor()
        {
            ClearPreviousCursor();

            PrintCurrentCursor();
        }

        private static void ClearPreviousCursor()
        {
            CursorLeft = cursorIndent;
            CursorTop = GetCursorTop(previousOption);

            Write(new string(' ', LEFT_CURSOR.Length));

            CursorLeft = cursorIndent + CURSOR_GAP;

            Write(new string(' ', RIGHT_CURSOR.Length));
        }

        private static void PrintCurrentCursor()
        {
            CursorLeft = cursorIndent;
            CursorTop = GetCursorTop(currentOption);

            Write(LEFT_CURSOR);

            CursorLeft = cursorIndent + CURSOR_GAP;

            Write(RIGHT_CURSOR);
        }

        private static int GetCursorTop(Option option)
        {
            switch (option)
            {
                case Option.PVP: return pvpCursorPos; break;
                case Option.PVE: return pveCursorPos; break;
                case Option.Exit: return exitCursorPos; break;
            }

            return -1;
        }
    }
}
