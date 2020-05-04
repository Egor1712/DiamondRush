using System.Drawing;

namespace DiamondRush.Environments
{
    public class Wall : IEnvironment
    {
        public string ImageName => "Wall";
        public Point Location { get; }

        public Wall(Point location)
        {
            Location = location;
        }

        public bool IsCollapseWithPlayer(Player player, GameState gameState) => false;
        

        public void Move(GameState gameState)
        {
        }

        public void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState)
        {
        }

        public void DoLogicWhenCollapseWithPlayer(GameState gameState)
        {
        }
    }
}