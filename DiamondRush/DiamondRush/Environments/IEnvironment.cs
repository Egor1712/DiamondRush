using System.Drawing;

namespace DiamondRush.Environments
{
    public interface IEnvironment : IDrawable
    { 
        Point Location { get; set; }
        void CollapseWithPlayer(GameState gameState, Player player);
        void Move(GameState gameState);
        void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState);
    }
}