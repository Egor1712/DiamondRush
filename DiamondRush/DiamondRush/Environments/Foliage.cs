using System.Drawing;
using DiamondRush.Creatures;

namespace DiamondRush.Environments
{
    public class Foliage : IEnvironment
    {
        public int Priority { get; set; }
        public bool IsDisappearInConflict(ICreature creature) => true;
        public Point Location { get; set; }
        public string ImageName { get; }

        public Foliage()
        {
            ImageName = "Foliage";
        }

        public void CollapseWithPlayer(GameState gameState, Player player)
        {
            player.Location = Location;
            gameState.RemoveEnvironment(this);
        }

        public void Move(GameState gameState)
        {
        }

        public void ReactOnWeapon(Weapon.Weapon weapon,  GameState gameState)
        {
            gameState.RemoveEnvironment(this);
        }

        public override int GetHashCode()
        {
            return Location.GetHashCode();
        }
    }
}