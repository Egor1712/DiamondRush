using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public class Stone : IEnvironment
    {
        private static readonly Direction Direction = Direction.Down;
        public string ImageName => "Stone";
        public Point Location { get; set; }
        public int FallHeight { get; private set; }
        

        public void CollapseWithPlayer(GameState gameState, Player player)
        {
            if ((player.Direction == Direction.Left || player.Direction == Direction.Right)
                && CanMoveToRightOrLeft(player.Direction, gameState))
            {
                player.Location = Location;
                gameState.MoveEnvironment(Location, 
                    new Point(Location.X + DirectionToPoints[player.Direction].X, 
                        Location.Y + DirectionToPoints[player.Direction].Y));
                Location = new Point(Location.X+DirectionToPoints[player.Direction].X,
                    Location.Y+ DirectionToPoints[player.Direction].Y);
            }
        }

        public void Move(GameState gameState)
        {
            var nextPoint = new Point(Location.X + DirectionToPoints[Direction].X,
                Location.Y + DirectionToPoints[Direction].Y);
            if (gameState.InBounds(nextPoint.X, nextPoint.Y))
            {
                if (gameState.Player.Location.Equals(nextPoint))
                {
                    if (FallHeight >= 1)
                    {
                        gameState.Player.BeatPlayer();
                        FallHeight = 0;
                    }
                    return;
                }
                (var environment, var creature) = gameState[nextPoint.Y, nextPoint.X];
                if (creature != null && FallHeight >= 1)
                {
                    gameState.MoveEnvironment(Location,nextPoint);
                    Location = nextPoint;
                    gameState.RemoveCreature(creature);
                    FallHeight++;
                    return;
                }

                if (environment == null && creature == null)
                {
                    gameState.MoveEnvironment(Location,nextPoint);
                    Location = nextPoint;
                    FallHeight++;
                    return;
                }
            }

            FallHeight = 0;
        }

        public void ReactOnWeapon(Weapon weapon,  GameState gameState)
        {
        }

        public bool CanMoveToRightOrLeft(Direction direction, GameState gameState)
        {
            var nextPoint = new Point(DirectionToPoints[direction].X + Location.X,
                DirectionToPoints[direction].Y + Location.Y);
            if (!gameState.InBounds(nextPoint.X,nextPoint.Y)) return false;
            (var environment, var creature) = gameState[nextPoint.Y,nextPoint.X];
            return (environment is null && creature is null);
        }

        public override int GetHashCode()
        {
            return Location.GetHashCode();
        }
    }
}