using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush.Creatures
{
    public static class MoveLogic
    {
        public static bool CanMove(GameState gameState, ICreature creature, int vision, out Point nextPoint)
        {
            nextPoint = new Point(-1, -1);
            var directionOrNull = GetNextStep(gameState, creature, vision);
            if (directionOrNull == null)
                return false;
            var direction = (Direction) directionOrNull;
            nextPoint = new Point(creature.Location.X + DirectionToPoints[direction].X,
                creature.Location.Y + DirectionToPoints[direction].Y);
            if (!gameState.InBounds(nextPoint)) return false;
            (var environments, var nextCreature) = gameState[nextPoint];
            return environments == null && nextCreature == null;
        }

        public static bool IsPlayerNear(Player player, ICreature creature, int vision)
        {
            var point = player.Location;
            return creature.Location.X - vision <= point.X
                   && creature.Location.X + vision >= point.X
                   && creature.Location.Y - vision < point.Y
                   && creature.Location.Y + vision > point.Y;
        }

        private static Direction? GetNextStep(GameState gameState, ICreature creature, int vision)
        {
            if (!IsPlayerNear(gameState.Player, creature, vision)) return null;
            if (gameState.Player.Location.X > creature.Location.X)
                return Direction.Right;
            if (gameState.Player.Location.X < creature.Location.X)
                return Direction.Left;
            if (gameState.Player.Location.Y > creature.Location.Y)
                return Direction.Down;
            if (gameState.Player.Location.Y < creature.Location.Y)
                return Direction.Up;

            return null;
        }
    }
}