using System.Drawing;
using System.Reflection;

namespace DiamondRush
{
    public interface IEnvironment : IDrawable
    { 
        Point Location { get; set; }
        void CollapseWithPlayer(GameState gameState, Player player);
        void Move(GameState gameState);
        void ReactOnWeapon(IWeapon weapon, GameState gameState);
    }
}