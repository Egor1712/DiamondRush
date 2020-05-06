using System.Drawing;

namespace DiamondRush.Creatures
{
    public interface ICreature : IGameObject
    {
        int BlockedSteps { get; }
        bool IsFrozen { get; }
    }
}