using System.Drawing;

namespace DiamondRush.Environments
{
    public class FragileWall : IEnvironment
    {
        public string ImageName => "FragileWall";
        public Point Location { get; }

        public FragileWall(Point location) => Location = location;

        public bool IsCollapseWithPlayer(Player player, GameState gameState) => false;

        public void Move(GameState gameState)
        {
        }

        public void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState)
        {
            gameState.RemoveEnvironment(this);   
        }

        public void DoLogicWhenCollapseWithPlayer(GameState gameState)
        {
        }
    }
}