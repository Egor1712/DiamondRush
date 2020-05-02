using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush.Environments
{
    public class Diamond : IEnvironment
    {
        private static readonly Direction Direction = Direction.Down;
        public string ImageName => "Diamond";
        public Point Location { get; private set; }

        public Diamond(Point location) => Location = location;

        public bool IsCollapseWithPlayer(Player player,  GameState gameState) => true;

        public void Move(GameState gameState)
        {
            var nextPoint = new Point(Location.X + DirectionToPoints[Direction].X,
                Location.Y + DirectionToPoints[Direction].Y);
            if (gameState.Player.Location.Equals(nextPoint))
                return;
            if (!gameState.InBounds(nextPoint)) return;
            (var environment, var creature) = gameState[nextPoint];
            if (creature != null || environment != null) return;
            Location = nextPoint;
        }

        public void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState)
        {
        }

        public void DoLogicWhenCollapseWithPlayer(GameState gameState)
        {
            gameState.Player.AddScore(20);
            gameState.RemoveEnvironment(this);
        }
    }
}