﻿namespace KeyboardSnake
{
    /// <summary>
    /// A point in space.
    /// </summary>
    public readonly struct Point
    {
        public readonly int x;
        public readonly int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Point a, Point b) => a.x == b.x && a.y == b.y;

        public static bool operator !=(Point a, Point b) => a.x != b.x || a.y != b.y;
    }
}
