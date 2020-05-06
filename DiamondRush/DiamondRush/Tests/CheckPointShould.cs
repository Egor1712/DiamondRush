using System.Drawing;
using NUnit.Framework;
using static DiamondRush.Resources;

namespace DiamondRush.Tests
{
    [TestFixture]
    public class CheckPointShould
    {
        [Test]
        public void PlayerShouldAppearIntoCheckPointAfterDeath()
        {
            var gameState = new GameState(1,4,new Player(new Point(0,3), Direction.Down, 1));
            ParseAllGameState(gameState, "S\n\n\nC", "");
            var stone = gameState[0, 0];
            Assert.AreEqual(new Point(0,0),stone.Location );
            Assert.AreEqual(new Point(0,3), gameState.Player.Location);
            gameState.UpdateState();
            gameState.Player.Move(gameState, Direction.Up);
            Assert.AreEqual(new Point(0,1),stone.Location );
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,3), gameState.Player.Location);
        }

        [Test]
        public void CheckPointShouldNotInteractWithOtherObjects()
        {
            var gameState = new GameState(1,3, new Player(new Point(0,0), Direction.Down)); 
            ParseAllGameState(gameState, "S\n\nC", "");
            var checkPoint = gameState[2, 0];
            var stone = gameState[0, 0];
            Assert.AreEqual(new Point(0,0),stone.Location);
            Assert.AreEqual(new Point(0,2), checkPoint.Location);
            gameState.UpdateState();
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,2), checkPoint.Location);
            Assert.AreEqual(new Point(0,1),stone.Location);
        }
    }
}