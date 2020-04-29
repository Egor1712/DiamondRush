using System.Drawing;

namespace DiamondRush
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

        public void ReactOnWeapon(IWeapon weapon, GameState gameState)
        {
        }
    }
}