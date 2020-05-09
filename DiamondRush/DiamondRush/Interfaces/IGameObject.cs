using System.Drawing;

namespace DiamondRush
{
    public interface IGameObject : IDrawable
    {
         Point Location { get; }
    }
}