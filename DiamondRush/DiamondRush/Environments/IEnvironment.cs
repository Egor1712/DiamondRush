using System.Drawing;
using DiamondRush.Weapon;

namespace DiamondRush.Environments
{
    public interface IEnvironment : IDrawable
    { 
        Point Location { get;}
        bool IsCollapseWithPlayer(Player player, GameState gameState);
        void Move(GameState gameState);
        void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState);
        void DoLogicWhenCollapseWithPlayer(GameState gameState);
    }
}