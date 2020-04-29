﻿using System.Drawing;

namespace DiamondRush
{
    public class CheckPoint : IEnvironment
    {
        public string ImageName => "CheckPoint";
        public Point Location { get; set; }

        public void CollapseWithPlayer(GameState gameState, Player player)
        {
            player.ChangeCheckPoint(Location);
            player.Location = Location;
        }

        public void Move(GameState gameState)
        {
        }

        public void ReactOnWeapon(IWeapon weapon,  GameState gameState)
        {
        }
    }
}