using System.Drawing;

namespace DiamondRush.Environments
{
    public class Chest : IEnvironment
    {
        public string ImageName => IsOpened ? "ChestOpen" : "ChestClose";
        public Point Location { get; }
        public bool IsOpened { get; private set; }
        public Weapon.Weapon Weapon { get; }

        public Chest(Weapon.Weapon weapon, Point location)
        {
            Weapon = weapon;
            Location = location;
        }

        public bool IsCollapseWithPlayer(Player player, GameState gameState) => true;

        public void Move(GameState gameState)
        {
        }

        public void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState)
        {
        }

        public void DoLogicWhenCollapseWithPlayer(GameState gameState)
        {
            if (!IsOpened)
            {
                gameState.Player.AddWeapon(Weapon);
                IsOpened = true;
            }
        }
    }
}