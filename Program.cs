using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Media;
using LedCSharp;

namespace KeyboardSnake
{
    class Program
    {
        static void Main(string[] args)
        {
            KeyboardController controller = new KeyboardController();

            SnakeGame game = new SnakeGame(controller);
            game.GameLoop();

            controller.Dispose();
        }
    }
}

/*
    // !! map editor !!
    int keycode = 0;
    List<int> keycodeList = new List<int>();

    while (true)
    {
        // input polling

        int oldKeycode = keycode;
        ConsoleKey key = Console.ReadKey(true).Key;
        if (key == ConsoleKey.LeftArrow)
            keycode--;
        else if (key == ConsoleKey.RightArrow)
            keycode++;
        else if (key == ConsoleKey.Enter)
        {
            keycodeList.Add(keycode);
            OutputList(keycodeList);
        }
        else if (key == ConsoleKey.Backspace)
        {
            keycodeList.Clear();
            OutputList(keycodeList);
        }

        if (oldKeycode == keycode)
            continue;

        LogitechGSDK.LogiLedSetLightingForKeyWithScanCode(oldKeycode, NothingColor);
        LogitechGSDK.LogiLedSetLightingForKeyWithScanCode(keycode, 100, 0, 0);

    }


    static void OutputList(List<int> list)
    {
        StringBuilder sb = new StringBuilder("new int[] { ");

        foreach (int number in list)
        {
            sb.Append($"{number}, ");
        }

        sb.Append("}");

        Console.WriteLine(sb);
    }
*/