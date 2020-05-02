using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush.Creatures
{
    public class SimpleSnake : ICreature
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

        public bool IsCollapseWithPlayer(GameState gameState, Player player)
        {
            return true;
        }

        public void DoLogicWhenCollapseWithPlayer(GameState gameState)
        {
            gameState.Player.BeatPlayer();
        }

        public void ReactOnWeapon(Weapon.Weapon weapon)
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

                (var environment, var creature) = gameState[nextPoint];
                if (creature != null || environment != null)
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