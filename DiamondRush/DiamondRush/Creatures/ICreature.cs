using System.Drawing;

namespace DiamondRush
{
    public interface ICreature : IDrawable
    {
        void Move(GameState gameState);
        Point Location { get; set; }
        Direction Direction { get; set; }
        void CollapseWithPlayer(GameState gameState, Player player);
        void ReactOnWeapon(Weapon weapon);
    }
}