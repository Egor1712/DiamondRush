using System.Drawing;
using static DiamondRush.Creatures.MoveLogic;

namespace DiamondRush.Creatures
{
    public class Archer : ICreature
    {
        public string ImageName => $"Archer";
        public Point Location { get; set; }
        public Direction Direction { get; set; }
        public int BlockedSteps { get; private set; }
        public bool IsFrozen { get; private set; }

        public Archer(Point location, Direction direction)
        {
            Location = location;
            Direction = direction;
        }

        public void Move(GameState gameState)
        {
            if (BlockedSteps > 0)
            {
                BlockedSteps--;
                return;
            }

            IsFrozen = false;
            if ((gameState.Player.Location.X == Location.X
                 || gameState.Player.Location.Y == Location.Y)
                && IsPlayerNear(gameState.Player, this, 8))
            {
                var directionToShoot = GetDirectionToShoot(gameState.Player);
                gameState.AddCreature(new Arrow(Location, directionToShoot));
            }
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

        private Direction GetDirectionToShoot(Player player)
        {
            if (player.Location.X == Location.X)
                return Location.Y < player.Location.Y ? Direction.Down : Direction.Up;
            if (player.Location.Y == Location.Y)
                return Location.X < player.Location.X ? Direction.Right : Direction.Left;
            return Direction.Down;
        }
    }
}