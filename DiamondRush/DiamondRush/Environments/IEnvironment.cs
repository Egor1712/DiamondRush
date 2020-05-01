using System.Drawing;
using DiamondRush.Weapon;

namespace DiamondRush.Environments
{
    public interface IEnvironment : IDrawable
    { 
        Point Location { get;}
        void CollapseWithPlayer(GameState gameState, Player player);
        void Move(GameState gameState);
        void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState);
    }
}