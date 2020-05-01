using System.Drawing;

namespace DiamondRush.Environments
{
    public class Chest : IEnvironment
    {
        public string ImageName => IsOpened ? "ChestOpen" : "ChestClose";
        public Point Location { get; set; }
        public bool IsOpened { get; private set; }
        public  Weapon.Weapon Weapon { get; }
        
        public Chest(Weapon.Weapon weapon, Point location)
        {
            Weapon = weapon;
            Location = location;
        }

        public void CollapseWithPlayer(GameState gameState, Player player)
        {
            if (!IsOpened)
            {
                player.AddWeapon(Weapon);
                IsOpened = true;
            }
            player.Location = Location;
        }

        public void Move(GameState gameState)
        {
        }

        public void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState)
        {
        }
    }
}