using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush.Environments
{
    public class Stone : IEnvironment
    {
        private static readonly Direction Direction = Direction.Down;
        public string ImageName => "Stone";
        public Point Location { get; set; }
        private int fallHeight;


        public void CollapseWithPlayer(GameState gameState, Player player)
        {
            if ((player.Direction != Direction.Left && player.Direction != Direction.Right) ||
                !CanMoveToRightOrLeft(player.Direction, gameState)) return;
            player.Location = Location;
            gameState.MoveEnvironment(Location,
                new Point(Location.X + DirectionToPoints[player.Direction].X,
                    Location.Y + DirectionToPoints[player.Direction].Y));
            Location = new Point(Location.X + DirectionToPoints[player.Direction].X,
                Location.Y + DirectionToPoints[player.Direction].Y);
        }

        public void Move(GameState gameState)
        {
            var nextPoint = new Point(Location.X + DirectionToPoints[Direction].X,
                Location.Y + DirectionToPoints[Direction].Y);
            if (gameState.InBounds(nextPoint))
            {
                if (gameState.Player.Location.Equals(nextPoint))
                {
                    if (fallHeight < 1) return;
                    gameState.Player.BeatPlayer();
                    fallHeight = 0;
                    return;
                }

                (var environment, var creature) = gameState[nextPoint];
                if (creature != null && fallHeight >= 1)
                {
                    gameState.MoveEnvironment(Location, nextPoint);
                    Location = nextPoint;
                    gameState.RemoveCreature(creature);
                    fallHeight++;
                    return;
                }

                if (environment == null && creature == null)
                {
                    gameState.MoveEnvironment(Location, nextPoint);
                    Location = nextPoint;
                    fallHeight++;
                    return;
                }
            }

            fallHeight = 0;
        }

        public void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState)
        {
        }

        private bool CanMoveToRightOrLeft(Direction direction, GameState gameState)
        {
            var nextPoint = new Point(DirectionToPoints[direction].X + Location.X,
                DirectionToPoints[direction].Y + Location.Y);
            if (!gameState.InBounds(nextPoint)) return false;
            (var environment, var creature) = gameState[nextPoint];
            return (environment is null && creature is null);
        }
    }
}