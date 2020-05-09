using System.Drawing;

namespace DiamondRush.Environments
{
    public class FragileWall : IGameObject, ICanReactOnWeapon
    {
        public string ImageName => "FragileWall";
        public Point Location { get; }

        public FragileWall(Point location) => Location = location;

        public void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState)
        {
            gameState.RemoveGameObject(this);   
        }
    }
}