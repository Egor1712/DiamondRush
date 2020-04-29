using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public interface IWeapon
    {
        void DoWork(GameState gameState, Direction direction);
    }
}