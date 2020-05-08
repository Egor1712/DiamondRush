using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush.Weapon
{
    public abstract class Weapon
    {
        public int Force { get; protected set; }
        public bool IsFrozen { get; protected set; }
        
        public string ImageName { get; protected set; }

        protected void DoWork(GameState gameState, Direction direction)
        {
            var actionPoint = new Point(gameState.Player.Location.X + DirectionToPoints[direction].X,
                gameState.Player.Location.Y + DirectionToPoints[direction].Y);
            if (!gameState.InBounds(actionPoint)) return;
            var gameObject = gameState[actionPoint];
            if (gameObject != null && gameObject is ICanReactOnWeapon reactOnWeapon)
                reactOnWeapon.ReactOnWeapon(this, gameState);
        }
    }
}