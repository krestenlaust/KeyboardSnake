using System;
using LedCSharp;

namespace KeyboardSnake
{
    public class KeyboardController : IDisposable, IDisplayController
    {
        public KeyboardController()
        {
            LogitechGSDK.LogiLedInit();
            LogitechGSDK.LogiLedSaveCurrentLighting();
        }

        public void Dispose()
        {
            LogitechGSDK.LogiLedShutdown();
        }

        static int ToKeyCode(Point point)
        {
            int x = point.x;
            int y = point.y;
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

        public void SetColor(Point point, Color color)
        {
            LogitechGSDK.LogiLedSetLightingForKeyWithScanCode(ToKeyCode(point), color.Red, color.Green, color.Blue);
        }
    }
}
