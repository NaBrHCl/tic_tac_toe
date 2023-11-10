using static System.Console;

namespace Tic_Tac_Toe
{
    static class Input
    {
        private static ConsoleKey[] _upKeys = { ConsoleKey.W, ConsoleKey.UpArrow };
        private static ConsoleKey[] _downKeys = { ConsoleKey.S, ConsoleKey.DownArrow };
        private static ConsoleKey[] _leftKeys = { ConsoleKey.A, ConsoleKey.LeftArrow };
        private static ConsoleKey[] _rightKeys = { ConsoleKey.D, ConsoleKey.RightArrow };
        private static ConsoleKey[] _confirmKeys = { ConsoleKey.Z, ConsoleKey.Enter };
        private static ConsoleKey[] _cancelKeys = { ConsoleKey.X, ConsoleKey.Escape };

        private static ConsoleKey[][] _keys;

        static Input() => _keys = new ConsoleKey[][] { _upKeys, _downKeys, _leftKeys, _rightKeys, _confirmKeys, _cancelKeys };

        public static Key Get() => Parse(ReadKey(true).Key);

        private static Key Parse(ConsoleKey input)
        {
            for (int i = 0; i < _keys.Length; i++)
            {
                if (_keys[i].Contains(input))
                    return (Key)i;
            }

            return Key.Null;
        }
    }
}
