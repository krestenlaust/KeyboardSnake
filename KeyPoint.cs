namespace KeyboardSnake
{
    /// <summary>
    /// A dot on the keyboard (from a macroscopic point of view).
    /// </summary>
    public readonly struct KeyPoint
    {
        public readonly int x;
        public readonly int y;
        public readonly int keyCode;

        public KeyPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
            keyCode = KeyCode(x, y);
        }

        public static bool operator ==(KeyPoint a, KeyPoint b) => a.x == b.x && a.y == b.y;

        public static bool operator !=(KeyPoint a, KeyPoint b) => a.x != b.x || a.y != b.y;

        public static int KeyCode(int x, int y)
        {
            int code = 0;

            // number row
            if (y == 0)
            {
                // new int[] { 41, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, }
                if (x == 0)
                {
                    code = 41;
                }
                else if (x > 0)
                {
                    code = 1 + x;
                }
            }
            else if (y == 1)
            {
                // new int[] { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, }
                code = 15;
                code += x;
            }
            else if (y == 2)
            {
                // new int[] { 58, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 93, }
                if (x == 0)
                {
                    code = 58;
                }
                else if (x == 12)
                {
                    code = 93;
                }
                else if (x > 0)
                {
                    code = 29 + x;
                }
            }
            else if (y == 3)
            {
                // new int[] { 42, 86, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, }
                if (x == 1)
                {
                    code = 86;
                }
                else
                {
                    code = 42 + x;
                }
            }

            return code;
        }
    }
}
