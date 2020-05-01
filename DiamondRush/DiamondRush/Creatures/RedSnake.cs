using System.Drawing;
using static DiamondRush.Creatures.MoveLogic;

namespace DiamondRush.Creatures
{
    public class RedSnake : ICreature
    {
        public string ImageName => $"RedSnakeRight";
        public Point Location { get; private set; }
        public Direction Direction { get; private set; }
        public int BlockedSteps { get; private set; }
        public bool IsFrozen { get; private set; }

        public RedSnake(Point location, Direction direction)
        {
            Location = location;
            Direction = direction;
        }

        public void CollapseWithPlayer(GameState gameState, Player player)
        {
            player.Location = Location;
            player.BeatPlayer();
        }

        public void ReactOnWeapon(Weapon.Weapon weapon)
        {
            IsFrozen = weapon.IsFrozen;
            BlockedSteps = weapon.Force;
        }
        public void Move(GameState gameState)
        {
            if (BlockedSteps > 0)
            {
                BlockedSteps--;
                return;
            }
            IsFrozen = false;
            
            if (!CanMove(gameState,this,10, out var nextPoint))
                return;
            if (gameState.Player.Location == nextPoint)
                gameState.Player.BeatPlayer();
            gameState.MoveCreature(Location, nextPoint);
            Location = nextPoint;
        }
    }
}