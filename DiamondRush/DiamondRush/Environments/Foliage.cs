using System.Drawing;
using DiamondRush.Creatures;

namespace DiamondRush.Environments
{
    public class Foliage : IEnvironment
    {
        public Point Location { get; }
        public string ImageName => "Foliage";

        public Foliage(Point location) => Location = location;

        public void CollapseWithPlayer(GameState gameState, Player player)
        {
            player.Location = Location;
            gameState.RemoveEnvironment(this);
        }

        public void Move(GameState gameState)
        {
        }

        public void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState)
        {
            gameState.RemoveEnvironment(this);
        }

        public override int GetHashCode()
        {
            return Location.GetHashCode();
        }
    }
}