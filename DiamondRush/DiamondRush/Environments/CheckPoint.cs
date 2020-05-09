using System.Drawing;

namespace DiamondRush.Environments
{
    public class CheckPoint : IGameObject, ICanCollapseWithPlayer
    {
        public string ImageName => "CheckPoint";
        public Point Location { get; }

        public CheckPoint( Point location)
        {
            Location = location;
        }

        public void CollapseWithPlayer(GameState gameState) => gameState.Player.ChangeCheckPoint(Location);
        
    }
}