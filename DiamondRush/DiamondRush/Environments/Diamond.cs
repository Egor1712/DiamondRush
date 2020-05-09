using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush.Environments
{
    public class Diamond : IGameObject, ICanMove, ICanCollapseWithPlayer
    {
        private static readonly Direction Direction = Direction.Down;
        public string ImageName => "Diamond";
        public Point Location { get; private set; }

        public Diamond(Point location) => Location = location;
        public void Move(GameState gameState)
        {
            var nextPoint = new Point(Location.X + DirectionToPoints[Direction].X,
                Location.Y + DirectionToPoints[Direction].Y);
            if (gameState.Player.Location.Equals(nextPoint))
                return;
            if (!gameState.InBounds(nextPoint)) return;
            var gameObject = gameState[nextPoint];
            if (gameObject != null) return;
            Location = nextPoint;
        }
        
        public void CollapseWithPlayer(GameState gameState)
        {
            gameState.Player.AddScore(20);
            gameState.RemoveGameObject(this);
        }
    }
}