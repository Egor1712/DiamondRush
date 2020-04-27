using System.Drawing;

namespace DiamondRush
{
    public interface creature : IDrawable
    {
        void Move(GameState gameState);
        Point Location { get; set; }
        Direction Direction { get; set; }
    }
}