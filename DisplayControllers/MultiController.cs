using System.Collections.Generic;

namespace KeyboardSnake
{
    public class MultiController : IDisplayController
    {
        readonly ICollection<IDisplayController> controllers;

        public MultiController(ICollection<IDisplayController> controllers)
        {
            this.controllers = controllers;
        }

        public void SetColor(Point point, Color color)
        {
            foreach (var item in controllers)
            {
                item.SetColor(point, color);
            }
        }
    }
}
