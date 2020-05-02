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


        public bool IsCollapseWithPlayer(Player player,  GameState gameState) =>
             (player.Direction == Direction.Left || player.Direction == Direction.Right)
             && CanMoveToRightOrLeft(player.Direction, gameState);
        

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
                    Location = nextPoint;
                    gameState.RemoveCreature(creature);
                    fallHeight++;
                    return;
                }

                if (environment == null && creature == null)
                {
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

        public void DoLogicWhenCollapseWithPlayer(GameState gameState)
        {
            var point = gameState.Player.Location;
            Location = new Point(point.X + DirectionToPoints[gameState.Player.Direction].X,
                point.Y + DirectionToPoints[gameState.Player.Direction].Y);
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