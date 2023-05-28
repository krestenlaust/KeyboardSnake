using System;

namespace KeyboardSnake
{
    public class ConsoleController : IDisplayController
    {
        public void SetColor(Point point, Color color)
        {
            Console.SetCursorPosition(point.x, point.y);
            Console.CursorVisible = false;

            int maxPercentage = Math.Max(Math.Max(color.Red, color.Green), color.Blue);

            if (maxPercentage == 0)
            {
                Console.Write(' ');
                return;
            }

            if (maxPercentage == color.Red)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (maxPercentage == color.Green)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (maxPercentage == color.Blue)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }

            Console.Write('#');
        }
    }
}
