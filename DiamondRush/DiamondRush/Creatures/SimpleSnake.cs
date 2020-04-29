﻿using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public class SimpleSnake : ICreature
    {
        public Point Location { get; set; }
        public string ImageName => $"Snake{Direction}";
        public Direction Direction { get; set; }
        public int BlockedSteps { get; private set; }
        
        public SimpleSnake(Point startLocation, Direction startDirection)
        {
            Location = startLocation;
            Direction = startDirection;
        }

        public void CollapseWithPlayer(GameState gameState, Player player)
        {
            player.Location = Location;
            player.BeatPlayer();
        }

        public void ReactOnWeapon(IWeapon weapon)
        {
            if (weapon is Hammer)
            {
                BlockedSteps += 100;
                return;
            }
        }

        public void Move(GameState gameState)
        {
            var nextPoint = new Point(Location.X + DirectionToPoints[Direction].X,
                Location.Y + DirectionToPoints[Direction].Y);
            if (gameState.InBounds(nextPoint.X,nextPoint.Y))
            {
                if (nextPoint.Equals(gameState.Player.Location))
                {
                    gameState.MoveCreature(Location, nextPoint);
                    Location = nextPoint;
                    gameState.Player.BeatPlayer();
                    return;
                }
                (var environment, var creature) = gameState[nextPoint.Y,nextPoint.X];
                if (creature != null || environment != null)
                {
                    Direction = OppositeDirection[Direction];
                    return;
                }
                gameState.MoveCreature(Location, nextPoint);
                Location = nextPoint;
                return;
            }
            
            Direction = OppositeDirection[Direction];
        }

    }
}