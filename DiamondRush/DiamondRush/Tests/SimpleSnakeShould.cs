using System.Drawing;
using DiamondRush.Creatures;
using NUnit.Framework;
using static DiamondRush.Resources;

namespace DiamondRush.Tests
{
    [TestFixture]
    public class SimpleSnakeShould
    {
        [Test]
        public void SnakeShouldMoveBetweenSomething()
        {
            var gameState = new GameState(1,3, new Player(new Point(0,0), Direction.Down ));
            ParseAllGameState(gameState,"",@"S");
            var snake = gameState[0, 0];
            Assert.AreEqual(new Point(0,0), snake.Location);
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,1), snake.Location );
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,2), snake.Location);
            gameState.UpdateState();
            Assert.AreEqual(Direction.Up, ((SimpleSnake) snake).Direction);
        }

        [Test]
        public void SnakeShouldBeatPlayer()
        {
            var gameState = new GameState(1,2, new Player(new Point(0,1),Direction.Down , 2));
            ParseAllGameState(gameState, "","S");
            var snake = gameState[0, 0];
            Assert.IsNotNull(snake);
            Assert.AreEqual(new Point(0,0), snake.Location);
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,1),snake.Location );
            Assert.AreEqual(1,gameState.Player.Health);
        }
    }
}