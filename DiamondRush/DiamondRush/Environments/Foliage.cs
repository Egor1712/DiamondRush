using System.Drawing;
using DiamondRush.Creatures;

namespace DiamondRush.Environments
{
    public class Foliage : IGameObject, ICanReactOnWeapon, ICanCollapseWithPlayer
    {
        public Point Location { get; }
        public string ImageName => "Foliage";

        public Foliage(Point location) => Location = location;

        
        public void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState)
        {
            gameState.RemoveGameObject(this);
        }

        public void CollapseWithPlayer(GameState gameState) => gameState.RemoveGameObject(this);
    }
}