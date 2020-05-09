using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush.Creatures
{
    public static class MoveLogic
    {
        public static bool CanMove(GameState gameState, IGameObject creature, int vision, out Point nextPoint)
        {
            nextPoint = new Point(-1, -1);
            var directionOrNull = GetNextStep(gameState, creature, vision);
            if (directionOrNull == null)
                return false;
            var direction = (Direction) directionOrNull;
            nextPoint = new Point(creature.Location.X + DirectionToPoints[direction].X,
                creature.Location.Y + DirectionToPoints[direction].Y);
            if (!gameState.InBounds(nextPoint)) return false;
            var gameObject = gameState[nextPoint];
            return gameObject == null;
        }

        public static bool IsPlayerNear(Player player, IGameObject gameObject, int vision)
        {
            var point = player.Location;
            return gameObject.Location.X - vision <= point.X
                   && gameObject.Location.X + vision >= point.X
                   && gameObject.Location.Y - vision < point.Y
                   && gameObject.Location.Y + vision > point.Y;
        }

        private static Direction? GetNextStep(GameState gameState, IGameObject gameObject, int vision)
        {
            if (!IsPlayerNear(gameState.Player, gameObject, vision)) return null;
            if (gameState.Player.Location.X > gameObject.Location.X)
                return Direction.Right;
            if (gameState.Player.Location.X < gameObject.Location.X)
                return Direction.Left;
            if (gameState.Player.Location.Y > gameObject.Location.Y)
                return Direction.Down;
            if (gameState.Player.Location.Y < gameObject.Location.Y)
                return Direction.Up;

            return null;
        }
    }
}