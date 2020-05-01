using System.Drawing;

namespace DiamondRush.Environments
{
    public class Wall : IEnvironment
    {
        public string ImageName => "Wall";
        public Point Location { get; set; }

        public void CollapseWithPlayer(GameState gameState, Player player)
        {
        }

        public void Move(GameState gameState)
        {

        }

        public void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState)
        {
        }
    }
}