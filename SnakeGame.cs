using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Media;
using LedCSharp;

namespace KeyboardSnake
{
    public class SnakeGame : IGame
    {
        const int MOVE_INTERVAL_MS = 700;
        const int MAP_WIDTH = 13;
        const int MAP_HEIGHT = 4;

        static readonly Color AppleColor = new Color(100, 0, 0);
        static readonly Color NothingColor = new Color(0, 0, 0);
        static Random random = new Random();
        static SoundPlayer appleCollectSound = new SoundPlayer("apple_collect.wav");
        static SoundPlayer gameOverSound = new SoundPlayer("game_over.wav");

        readonly IDisplayController displayController;

        int playerX = 0;
        int playerY = 0;

        int oldPlayerX = 0;
        int oldPlayerY = 0;

        Queue<Point> playerBody = new Queue<Point>();

        int directionX = 1;
        int directionY = 0;

        int oldDirectionX = 1;
        int oldDirectionY = 0;

        Point applePos = new Point(3, 3);
        Stopwatch timer = new Stopwatch();

        public SnakeGame(IDisplayController displayController)
        {
            this.displayController = displayController;
        }

        static int Mod(int x, int m) => ((x % m) + m) % m;

        public void Start()
        {
            timer.Start();
            SpawnApple();
        }

        public bool Update()
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
                {
                    return false;
                }

                if (oldDirectionX == directionX && oldDirectionY == directionY)
                {
                    return true;
                }

                RedrawTargetPoint();
            }

            if (timer.ElapsedMilliseconds < MOVE_INTERVAL_MS)
            {
                return true;
            }

            timer.Restart();

            Point oldPlayer = new Point(oldPlayerX, oldPlayerY);

            playerX = Mod(playerX + directionX, MAP_WIDTH);
            playerY = Mod(playerY + directionY, MAP_HEIGHT);

            oldPlayerX = playerX;
            oldPlayerY = playerY;

            Point player = new Point(playerX, playerY);
            Point apple = new Point(applePos.x, applePos.y);

            if (player != apple && playerBody.Count > 0)
            {
                // remove part furthest back
                Point lastPart = playerBody.Dequeue();
                displayController.SetColor(lastPart, NothingColor);
            }

            // put new part where player was before.
            playerBody.Enqueue(oldPlayer);

            // draw body part
            displayController.SetColor(oldPlayer, new Color(0, 60, 0));

            // draw head
            displayController.SetColor(player, new Color(0, 100, 0));

            RedrawTargetPoint();

            // check if player is colliding with itself.
            if (playerBody.Contains(player))
            {
                foreach (var item in playerBody)
                {
                    displayController.SetColor(item, NothingColor);
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

            return true;
        }

        void RedrawApple()
        {
            displayController.SetColor(applePos, AppleColor);
        }

        void SpawnApple()
        {
            applePos = new Point(random.Next(MAP_WIDTH), random.Next(MAP_HEIGHT));

            RedrawApple();

            appleCollectSound.Play();
        }

        void RedrawTargetPoint()
        {
            Point oldTarget = new Point(
                Mod(playerX + oldDirectionX, MAP_WIDTH),
                Mod(playerY + oldDirectionY, MAP_HEIGHT));
            Point target = new Point(
                Mod(playerX + directionX, MAP_WIDTH),
                Mod(playerY + directionY, MAP_HEIGHT));

            if (oldTarget.x == applePos.x && oldTarget.y == applePos.y)
            {
                RedrawApple();
            }
            else
            {
                displayController.SetColor(oldTarget, NothingColor);
            }

            if (target.x == applePos.x && target.y == applePos.y)
            {
                displayController.SetColor(target, new Color(100, 0, 50));
            }
            else
            {
                displayController.SetColor(target, new Color(0, 0, 20));
            }
        }
    }
}
