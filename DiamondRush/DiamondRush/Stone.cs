using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public class Stone : IEnvironment
    {
        private static readonly Direction Direction = Direction.Down;
        public string ImageName { get; }
        public int Priority { get; set; }
        public Point Location { get; private set; }
        public int FallHight { get; private set; }
        public bool IsDisappearInConflict(creature creature) => false;

        public void Move(GameState gameState)
        {
            var nextPoint = new Point(Location.X + DirectionToPoints[Direction].X, 
                Location.Y+DirectionToPoints[Direction].Y);
            if (gameState.InBounds(nextPoint))
            {
                var tuple = gameState[nextPoint];
                if (tuple.Enviroment != null)
                {
                    FallHight = 0;
                    return;
                }

                gameState.MoveEnviroment(Location, nextPoint);
                FallHight++;
                Location = nextPoint;
            }
        }

        public bool CanMoveToRightOrLeft(Direction direction, GameState gameState)
        {
            var nextPoint = new Point(DirectionToPoints[direction].X + Location.X,
                DirectionToPoints[direction].Y + Location.Y);
            if (!gameState.InBounds(nextPoint)) return false;
            (var environment, var creature) = gameState[nextPoint];
            return environment is null && creature is null;

        }

    }
}