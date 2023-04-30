using LedCSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Media;

namespace KeyboardSnake
{
    class Program
    {
        const int MOVE_INTERVAL_MS = 700;
        const int MAP_WIDTH = 13;
        const int MAP_HEIGHT = 4;

        static int playerX = 0;
        static int playerY = 0;

        static int oldPlayerX = 0;
        static int oldPlayerY = 0;

        static Queue<KeyPoint> playerBody = new Queue<KeyPoint>();
        
        static int directionX = 1;
        static int directionY = 0;
        
        static int oldDirectionX = 1;
        static int oldDirectionY = 0;

        static int appleX = 3;
        static int appleY = 3;

        static Random random = new Random();

        static SoundPlayer appleCollectSound = new SoundPlayer("apple_collect.wav");
        static SoundPlayer gameOverSound = new SoundPlayer("game_over.wav");

        static void Main(string[] args)
        {
            LogitechGSDK.LogiLedInit();
            LogitechGSDK.LogiLedSaveCurrentLighting();

            Stopwatch timer = new Stopwatch();
            timer.Start();

            SpawnApple();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    // input polling
                    ConsoleKey key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.LeftArrow)
                    {
                        oldDirectionX = directionX;
                        oldDirectionY = directionY;

                        directionX = -1;
                        directionY = 0;
                    }
                    else if (key == ConsoleKey.RightArrow)
                    {
                        oldDirectionX = directionX;
                        oldDirectionY = directionY;

                        directionX = 1;
                        directionY = 0;
                    }
                    else if (key == ConsoleKey.UpArrow)
                    {
                        oldDirectionX = directionX;
                        oldDirectionY = directionY;

                        directionX = 0;
                        directionY = -1;
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        oldDirectionX = directionX;
                        oldDirectionY = directionY;

                        directionX = 0;
                        directionY = 1;
                    }
                    else if (key == ConsoleKey.Escape)
                        break;

                    if (oldDirectionX == directionX && oldDirectionY == directionY)
                        continue;

                    RedrawTargetPoint();
                }

                if (timer.ElapsedMilliseconds < MOVE_INTERVAL_MS)
                    continue;

                timer.Restart();

                KeyPoint oldPlayer = new KeyPoint(oldPlayerX, oldPlayerY);

                playerX = mod(playerX + directionX, MAP_WIDTH);
                playerY = mod(playerY + directionY, MAP_HEIGHT);

                oldPlayerX = playerX;
                oldPlayerY = playerY;

                KeyPoint player = new KeyPoint(playerX, playerY);
                KeyPoint apple = new KeyPoint(appleX, appleY);

                if (player != apple && playerBody.Count > 0)
                {
                    // fjern bageste del
                    KeyPoint lastPart = playerBody.Dequeue();
                    LogitechGSDK.LogiLedSetLightingForKeyWithScanCode(lastPart.keyCode, 0, 0, 0);
                }
                
                // put new part where player was before.
                playerBody.Enqueue(oldPlayer);
                
                // draw body part
                LogitechGSDK.LogiLedSetLightingForKeyWithScanCode(oldPlayer.keyCode, 0, 60, 0);

                // draw head
                LogitechGSDK.LogiLedSetLightingForKeyWithScanCode(player.keyCode, 0, 100, 0);

                RedrawTargetPoint();

                // check if player is colliding with itself.
                if (playerBody.Contains(player))
                {
                    foreach (var item in playerBody)
                    {
                        LogitechGSDK.LogiLedSetLightingForKeyWithScanCode(item.keyCode, 0, 0, 0);
                    }

                    playerBody.Clear();

                    RedrawApple();
                    gameOverSound.Play();
                }
                
                if (apple == player)
                {
                    // new apple
                    SpawnApple();
                }
            }

            LogitechGSDK.LogiLedShutdown();
        }

        static void RedrawTargetPoint()
        {
            KeyPoint oldTarget = new KeyPoint(
                mod(playerX + oldDirectionX, MAP_WIDTH), 
                mod(playerY + oldDirectionY, MAP_HEIGHT)
                );
            KeyPoint target = new KeyPoint(
                mod(playerX + directionX, MAP_WIDTH), 
                mod(playerY + directionY, MAP_HEIGHT)
                );

            if (oldTarget.x == appleX && oldTarget.y == appleY)
            {
                RedrawApple();
            }else
            {
                LogitechGSDK.LogiLedSetLightingForKeyWithScanCode(oldTarget.keyCode, 0, 0, 0);
            }

            if (target.x == appleX && target.y == appleY)
            {
                LogitechGSDK.LogiLedSetLightingForKeyWithScanCode(target.keyCode, 100, 0, 50);
            }
            else
            {
                LogitechGSDK.LogiLedSetLightingForKeyWithScanCode(target.keyCode, 0, 0, 20);
            }
        }

        static void RedrawApple()
        {
            KeyPoint apple = new KeyPoint(appleX, appleY);

            LogitechGSDK.LogiLedSetLightingForKeyWithScanCode(apple.keyCode, 100, 0, 0);
        }

        static void SpawnApple()
        {
            appleX = random.Next(MAP_WIDTH);
            appleY = random.Next(MAP_HEIGHT);

            RedrawApple();

            appleCollectSound.Play();
        }

        static int mod(int x, int m) => (x % m + m) % m;
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

        LogitechGSDK.LogiLedSetLightingForKeyWithScanCode(oldKeycode, 0, 0, 0);
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