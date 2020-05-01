using System.Drawing;

namespace DiamondRush.Environments
{
    public class CheckPoint : IEnvironment
    {
        public string ImageName => "CheckPoint";
        public Point Location { get; }

        public CheckPoint(Point location) => Location = location;

        public void CollapseWithPlayer(GameState gameState, Player player)
        {
            player.ChangeCheckPoint(Location);
            player.Location = Location;
        }

        public void Move(GameState gameState)
        {
            if (Location.Equals(gameState.Player.Location))
            {
                gameState.Player.ChangeCheckPoint(Location);
            }
        }

        public void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState)
        {
        }
    }
}