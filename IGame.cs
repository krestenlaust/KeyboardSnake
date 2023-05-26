namespace KeyboardSnake
{
    public interface IGame
    {
        void Start();

        /// <summary>
        /// False to close game.
        /// </summary>
        /// <returns></returns>
        bool Update();
    }
}
