﻿using System.Drawing;
using DiamondRush.Environments;
using NUnit.Framework;
using static DiamondRush.Resources;

namespace DiamondRush.Tests
{
    [TestFixture]
    public class ChestShould
    {
        [Test]
        public void ChestShouldNotInteractWithOtherObjects()
        {
            var gameState = new GameState(1,3, new Player(new Point(0,0), Direction.Down));
            ParseAllGameState(gameState, "S\n\nH", "");
            var checkPoint = gameState[2, 0];
            var stone = gameState[0, 0];
            Assert.AreEqual(new Point(0,0),stone.Location);
            Assert.AreEqual(new Point(0,2), checkPoint.Location);
            gameState.UpdateState();
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,2), checkPoint.Location);
            Assert.AreEqual(new Point(0,1),stone.Location);
        }

        [Test]
        public void ChestShouldAddWeaponToPlayer()
        {
            var gameState = new GameState(1,3, new Player(new Point(0,1), Direction.Down));
            ParseAllGameState(gameState, "\n\nH", "");
            var chest = gameState[2, 0] as Chest;
            Assert.IsNotNull(chest);
            Assert.AreEqual(new Point(0,2), chest.Location);
            Assert.IsFalse(chest.IsOpened);
            Assert.AreEqual(new Point(0,1), gameState.Player.Location);
            gameState.Player.Move(gameState, Direction.Down);
            Assert.AreEqual(new Point(0,2),gameState.Player.Location );
            Assert.AreEqual(chest.Weapon,gameState.Player.CurrentWeapon );
        }
    }
}