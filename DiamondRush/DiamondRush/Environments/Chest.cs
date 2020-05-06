using System.Drawing;
namespace DiamondRush.Environments
{
    public class Chest : IGameObject, ICanCollapseWithPlayer
    {
        public string ImageName => IsOpened ? "ChestOpen" : "ChestClose";
        public Point Location { get; }
        public bool IsOpened { get; private set; }
        public Weapon.Weapon Weapon { get; }

        public Chest(Point location, Weapon.Weapon weapon)
        {
            Location = location;
            Weapon = weapon;
        }
        
        public void CollapseWithPlayer(GameState gameState)
        {
            if (!IsOpened)
            {
                gameState.Player.AddWeapon(Weapon);
                IsOpened = true;
            }
        }
    }
}