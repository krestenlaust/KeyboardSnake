namespace KeyboardSnake
{
    public struct Color
    {
        /// <summary>
        /// The red component expressed as a percentage.
        /// </summary>
        public int Red;

        /// <summary>
        /// The green component expressed as a percentage.
        /// </summary>
        public int Green;

        /// <summary>
        /// The blue component expressed as a percentage.
        /// </summary>
        public int Blue;

        public Color(int redPercentage, int greenPercentage, int bluePercentage)
        {
            Red = redPercentage;
            Green = greenPercentage;
            Blue = bluePercentage;
        }
    }
}
