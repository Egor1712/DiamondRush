using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush.Creatures
{
    public class SimpleSnake : IGameObject, ICanMove, ICanReactOnWeapon, ICanCollapseWithPlayer
    {
        public Point Location { get; private set; }
        public string ImageName => $"Snake{Direction}";
        public Direction Direction { get; private set; }
        public int BlockedSteps { get; private set; }
        public bool IsFrozen { get; private set; }


        public SimpleSnake(Point startLocation, Direction startDirection)
        {
            Location = startLocation;
            Direction = startDirection;
        }
        
        public void CollapseWithPlayer(GameState gameState)
        {
            gameState.Player.BeatPlayer();
        }

        public void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState)
        {
            BlockedSteps = weapon.Force;
            IsFrozen = weapon.IsFrozen;
        }

        public void Move(GameState gameState)
        {
            if (BlockedSteps > 0)
            {
                BlockedSteps--;
                return;
            }

            IsFrozen = false;

            var nextPoint = new Point(Location.X + DirectionToPoints[Direction].X,
                Location.Y + DirectionToPoints[Direction].Y);
            if (gameState.InBounds(nextPoint))
            {
                if (nextPoint.Equals(gameState.Player.Location))
                {
                    Location = nextPoint;
                    gameState.Player.BeatPlayer();
                    return;
                }

                var gameObject = gameState[nextPoint];
                if (gameObject != null)
                {
                    Direction = OppositeDirection[Direction];
                    return;
                }

                Location = nextPoint;
                return;
            }

            Direction = OppositeDirection[Direction];
        }
    }
}