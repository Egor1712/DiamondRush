﻿using System.Drawing;

namespace DiamondRush.Creatures
{
    public interface ICreature : IDrawable
    {
        Point Location { get; }
        Direction Direction { get; }
        int BlockedSteps { get; }
        bool IsFrozen { get; }
        void Move(GameState gameState);

        void CollapseWithPlayer(GameState gameState, Player player);
        void ReactOnWeapon(Weapon.Weapon weapon);
    }
}