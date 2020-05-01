using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush.Creatures
{
    public class Arrow : ICreature
    {
        public string ImageName => $"Arrow";
        public Point Location { get; private set; }
        public Direction Direction { get; }
        public int BlockedSteps => 0;
        public bool IsFrozen => false;

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
                gameState.RemoveCreature(this);
                return;
            }

            if (gameState.Player.Location == nextPoint)
            {
                gameState.Player.BeatPlayer();
                gameState.RemoveCreature(this);
                return;
            }

            (var environment, var creature) = gameState[nextPoint];
            if (environment != null)
                gameState.RemoveCreature(this);
            if (creature != null)
                gameState.RemoveCreature(creature);
            if (creature == null && environment == null)
                Location = nextPoint;
        }

        public void CollapseWithPlayer(GameState gameState, Player player)
        {
            gameState.RemoveCreature(this);
            player.BeatPlayer();
        }

        public void ReactOnWeapon(Weapon.Weapon weapon)
        {
        }
    }
}