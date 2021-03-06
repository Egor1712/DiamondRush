﻿using System.Drawing;
using static DiamondRush.Creatures.MoveLogic;

namespace DiamondRush.Creatures
{
    public class Monkey : ICreature, ICanMove, ICanReactOnWeapon, ICanCollapseWithPlayer
    {
        public string ImageName => "Monkey";
        public Point Location { get; private set; }
        public Direction Direction { get; private set; }
        public int BlockedSteps { get; private set; }
        public bool IsFrozen { get; private set; }

        public Monkey(Point location, Direction direction)
        {
            Location = location;
            Direction = direction;
        }

        public void Move(GameState gameState)
        {
            if (BlockedSteps > 0)
            {
                BlockedSteps--;
                return;
            }
            IsFrozen = false;
            
            if (!CanMove(gameState,this,4, out var nextPoint))
                return;
            if (gameState.Player.Location == nextPoint)
                gameState.Player.BeatPlayer();
            Location = nextPoint;
        }

        public void CollapseWithPlayer(GameState gameState)
        {
            gameState.Player.BeatPlayer();
        }

        public void ReactOnWeapon(Weapon.Weapon weapon, GameState gameState)
        {
            IsFrozen = weapon.IsFrozen;
            BlockedSteps = weapon.Force;
        }

     

    }
}