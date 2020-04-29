﻿using System.Drawing;
using NUnit.Framework;

namespace DiamondRush.Tests
{
    [TestFixture]
    public class SimpleSnakeShould
    {
        [Test]
        public void SnakeShouldMoveBetweenSomething()
        {
            var gameState = new GameState(1,3, new Player(new Point(0,0), Direction.Down ));
            gameState.ParseCreatures(@"S");
            var snake = gameState[0, 0].Creature;
            snake.Direction = Direction.Down;
            Assert.AreEqual(new Point(0,0), snake.Location);
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,1), snake.Location );
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,2), snake.Location);
            gameState.UpdateState();
            Assert.AreEqual(Direction.Up, snake.Direction);
        }
        
        
    }
}