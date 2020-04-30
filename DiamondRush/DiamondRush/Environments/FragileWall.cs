using System.Drawing;

namespace DiamondRush
{
    public class FragileWall : IEnvironment
    {
        public string ImageName => "FragileWall";
        public Point Location { get; set; }

        public FragileWall(Point location) => Location = location;

        public void CollapseWithPlayer(GameState gameState, Player player)
        {
        }

        public void Move(GameState gameState)
        {
        }

        public void ReactOnWeapon(Weapon weapon, GameState gameState)
        {
            gameState.RemoveEnvironment(this);   
        }
    }
}