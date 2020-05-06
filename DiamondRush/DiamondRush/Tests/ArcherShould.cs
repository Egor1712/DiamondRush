using System.Drawing;
using NUnit.Framework;
using static DiamondRush.Resources;

namespace DiamondRush.Tests
{
    [TestFixture]
    public class ArcherShould
    {
        [Test]
        public void ArcherShouldDieByStone()
        {
            var gameState = new GameState(1, 3, new Player(new Point(-5, -5), Direction.Down, 2));
            ParseAllGameState(gameState, "S","\n\nA");
            var monkey = gameState[2, 0];
            var stone = gameState[0, 0];
            Assert.IsNotNull(monkey);
            gameState.UpdateState();
            gameState.UpdateState();
            Assert.AreEqual(new Point(0, 2), stone.Location);
            Assert.IsNull(gameState[2, 0]);
        }

        [Test]
        public void ArcherShouldShootArrowWhenPlayerNear()
        {
            var gameState  = new GameState(4,1, new Player(new Point(3,0),Direction.Right ));
            ParseAllGameState(gameState, "","A");
            var archer = gameState[0, 0];
            Assert.IsNotNull(archer);
            gameState.UpdateState();
            gameState.UpdateState();
            gameState.UpdateState();
            Assert.AreEqual(2, gameState.Player.Health);
        }
    }
}