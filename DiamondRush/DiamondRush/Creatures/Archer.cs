using System.Drawing;
using static DiamondRush.Creatures.MoveLogic;
using static DiamondRush.Resources;

namespace DiamondRush.Creatures
{
    public class Archer : ICreature, ICanReactOnWeapon, ICanMove, ICanCollapseWithPlayer
    {
        public string ImageName => $"Archer";
        public Point Location { get; }
        public Direction Direction { get; }
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
            if ((gameState.Player.Location.X != Location.X && gameState.Player.Location.Y != Location.Y) ||
                !IsPlayerNear(gameState.Player, this, 8)) return;
            var directionToShoot = GetDirectionToShoot(gameState.Player);
            gameState.AddGameObject(new Arrow(
                new Point(Location.X + DirectionToPoints[directionToShoot].X,
                    Location.Y + DirectionToPoints[directionToShoot].Y),
                directionToShoot));
        }
        
        public void CollapseWithPlayer(GameState gameState)
        {
            gameState.Player.BeatPlayer();
        }

        public void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState)
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