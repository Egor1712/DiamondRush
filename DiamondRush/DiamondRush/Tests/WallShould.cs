using System.Drawing;
using NUnit.Framework;

namespace DiamondRush.Tests
{
    [TestFixture]
    public class WallShould
    {
        [Test]
        public void WallShouldNotInteractWithOtherObject()
        {
            var gameState = new GameState(1,3, new Player(new Point(0,0), Direction.Down));
            gameState.ParseAllGameState("S\n\nW", "");
            var checkPoint = gameState[2, 0].Enviroment;
            var stone = gameState[0, 0].Enviroment;
            Assert.AreEqual(new Point(0,0),stone.Location);
            Assert.AreEqual(new Point(0,2), checkPoint.Location);
            gameState.UpdateState();
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,2), checkPoint.Location);
            Assert.AreEqual(new Point(0,1),stone.Location);
        }
    }
}