using System.Drawing;
using NUnit.Framework;

namespace DiamondRush.Tests
{
    [TestFixture]
    public class ArcherShould
    {
        [Test]
        public void ArcherShouldDieByStone()
        {
            var gameState = new GameState(1, 3, new Player(new Point(-5, -5), Direction.Down, 2));
            gameState.ParseAllGameState("S","\n\nA");
            var monkey = gameState[2, 0].Creature;
            var stone = gameState[0, 0].Enviroment;
            Assert.IsNotNull(monkey);
            gameState.UpdateState();
            gameState.UpdateState();
            Assert.AreEqual(new Point(0, 2), stone.Location);
            Assert.IsNull(gameState[2, 0].Creature);
        }

        [Test]
        public void ArcherShouldShootArrowWhenPlayerNear()
        {
            var gameState  = new GameState(4,1, new Player(new Point(3,0),Direction.Right ));
            gameState.ParseAllGameState("","A");
            var archer = gameState[0, 0].Creature;
            Assert.IsNotNull(archer);
            gameState.UpdateState();
            gameState.UpdateState();
            gameState.UpdateState();
            Assert.AreEqual(2, gameState.Player.Health);
        }
    }
}