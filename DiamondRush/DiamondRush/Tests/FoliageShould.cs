using System.Drawing;
using NUnit.Framework;

namespace DiamondRush.Tests
{
    [TestFixture]
    public class FoliageShould
    {
        [Test]
        public void FoliageShouldNotFall()
        {
            var gameState = new GameState(1,2,null);
            gameState.ParseEnvironment(@"F");
            var foliage = gameState[0, 0].Enviroment;
            Assert.AreEqual(new Point(0,0), foliage.Location);
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,0), foliage.Location);
        }

        [Test]
        public void FoliageShouldDisappearAfterPlayerStep()
        {
            var gameState = new GameState(1,2,new Player(new Point(0,0),Direction.Down));
            gameState.ParseEnvironment(
                "\nF");
            var foliage = gameState[1, 0].Enviroment;
            Assert.AreEqual(new Point(0,1),foliage.Location);
            gameState.Player.Move(gameState, Direction.Down);
            Assert.AreEqual(new Point(0,1), gameState.Player.Location);
            Assert.IsNull(gameState[1,0].Enviroment);
        }
    }
}