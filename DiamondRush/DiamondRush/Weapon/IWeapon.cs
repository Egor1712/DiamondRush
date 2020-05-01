using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush.Weapon
{
    public abstract class Weapon : IDrawable
    {
        public int Force { get; protected set; }
        public bool IsFrozen { get; protected set; }
        
        public string ImageName { get; protected set; }

        public void DoWork(GameState gameState, Direction direction)
        {
            var actionPoint = new Point(gameState.Player.Location.X + DirectionToPoints[direction].X,
                gameState.Player.Location.Y + DirectionToPoints[direction].Y);
            if (!gameState.InBounds(actionPoint)) return;
            (var environment, var creature) = gameState[actionPoint];
            if (environment != null)
            {
                environment.ReactOnWeapon(this, gameState);
                return;
            }
            creature?.ReactOnWeapon(this);
        }
    }
}