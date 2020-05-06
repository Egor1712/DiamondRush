using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush.Creatures
{
    public class Arrow : ICreature, ICanMove, ICanCollapseWithPlayer
    {
        public string ImageName => $"Arrow";
        public Point Location { get; private set; }
        public Direction Direction { get; }
        
        public int BlockedSteps { get; }
        public bool IsFrozen { get; }

        public Arrow(Point location, Direction direction)
        {
            Location = location;
            Direction = direction;
        }

        public void Move(GameState gameState)
        {
            var nextPoint = new Point(Location.X + DirectionToPoints[Direction].X,
                Location.Y + DirectionToPoints[Direction].Y);
            if (!gameState.InBounds(nextPoint))
            {
                gameState.RemoveGameObject(this);
                return;
            }

            if (gameState.Player.Location == nextPoint)
            {
                gameState.Player.BeatPlayer();
                gameState.RemoveGameObject(this);
                return;
            }

            var gameObject = gameState[nextPoint];
            if (gameObject != null && gameObject is ICreature creature)
                gameState.RemoveGameObject(creature);
            if (gameObject == null)
            {
                Location = nextPoint;
                return;
            }
            gameState.RemoveGameObject(this);
        }


        public void CollapseWithPlayer(GameState gameState)
        {
            gameState.RemoveGameObject(this);
            gameState.Player.BeatPlayer();
        }
    }
}