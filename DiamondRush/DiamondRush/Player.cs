using System.Drawing;
using System.Windows.Forms;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public class Player
    {
        public Point Location { get; set; }
        public Direction Direction { get; set; }
        public int Health { get; set; }
        public string ImageName { get; }

        public Player(Point location, Direction direction)
        {
            Location = location;
            Direction = direction;
            Health = 3;
            ImageName = "PlayerRight";
        }

        public void Move(GameState gameState, Direction direction)
        {
            var nextPoint = new Point(Location.X + DirectionToPoints[direction].X,
                Location.Y + DirectionToPoints[direction].Y);
            if (gameState.InBounds(nextPoint))
            {
                (var environment, var creature) = gameState[nextPoint];
                if (CanCollapse(environment))
                    Location = nextPoint;
                if (environment is Stone stone
                    && (Direction == Direction.Left || Direction == Direction.Right)
                    && stone.CanMoveToRightOrLeft(Direction,gameState))
                {
                    Location = nextPoint;
                    gameState.MoveEnviroment(nextPoint, 
                        new Point(nextPoint.X + DirectionToPoints[Direction].X, nextPoint.Y + DirectionToPoints[Direction].Y));
                }

                if (environment == null && creature == null)
                    Location = nextPoint;

            }
        }

        private bool CanCollapse(IEnvironment environment)
        {
            return environment is Foliage;
        }
        
        public void ChangeDirection(Keys key)
        {
            Direction = KeysToDirection[key];
        }
    }
}