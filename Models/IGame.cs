namespace KeyboardSnake
{
    public interface IGame
    {
        /// <summary>
        /// Called when started.
        /// </summary>
        void Start();

        /// <summary>
        /// False to close game.
        /// </summary>
        /// <returns></returns>
        bool Update();
    }
}
