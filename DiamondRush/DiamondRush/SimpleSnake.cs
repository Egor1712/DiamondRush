using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public class SimpleSnake : creature
    {
        public Point Location { get; set; }
        public Direction Direction { get; set; }
        public string ImageName { get; }


        public SimpleSnake(Point startLocation, Direction startDirection)
        {
            Location = startLocation;
            Direction = startDirection;
            ImageName = "Snake";
        }

        public void Move(GameState gameState)
        {
            var nextPoint = new Point(Location.X + DirectionToPoints[Direction].X,
                Location.Y + DirectionToPoints[Direction].Y);
            if (gameState.InBounds(nextPoint))
            {
                var tuple = gameState[nextPoint];
                if (tuple.Creature != null)
                {
                    Direction = OppositeDirection[Direction];
                    return;
                }
                if (tuple.Enviroment !=  null)
                {
                    Direction = OppositeDirection[Direction];
                    return;
                }
                gameState.MoveCreature(Location, nextPoint);
                Location = nextPoint;
                return;
            }
            
            Direction = OppositeDirection[Direction];
        }

    }
}