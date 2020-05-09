using System.Drawing;

namespace DiamondRush.Environments
{
    public class Wall : IGameObject
    {
        public string ImageName => "Wall";
        public Point Location { get; }

        public Wall(Point location)
        {
            Location = location;
        }
    }
}