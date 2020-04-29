using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public class Diamond : IEnvironment
    {
        private static Direction direction = Direction.Down;
        public string ImageName => "Diamond";
        public int FallHeight { get; private set; }

        public Point Location { get; set; }

        public void CollapseWithPlayer(GameState gameState, Player player)
        {
            player.Location = Location;
            player.AddScore(20);
            gameState.RemoveEnvironment(this);
        }

        public void Move(GameState gameState)
        {
            var nextPoint = new Point(Location.X + DirectionToPoints[direction].X,
                Location.Y + DirectionToPoints[direction].Y);
            if (gameState.Player.Location.Equals(nextPoint))
                return;

            if (gameState.InBounds(nextPoint.X,nextPoint.Y))
            {
                (var environment, var creature) = gameState[nextPoint.Y, nextPoint.X];
                if (creature == null && environment == null)
                {
                    gameState.MoveEnvironment(Location, nextPoint);
                    Location = nextPoint;
                    FallHeight++;
                }
            }
        }

        public void ReactOnWeapon(IWeapon weapon,  GameState gameState)
        {
        }
    }
}