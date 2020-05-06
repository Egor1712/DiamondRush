using System.Drawing;
using DiamondRush.Weapon;
using NUnit.Framework;
using static DiamondRush.Resources;

namespace DiamondRush.Tests
{
    [TestFixture]
    public class MonkeyShould
    {
        [Test]
        public void MonkeyShouldBeatPlayer()
        {
            var gameState = new GameState(1,2, new Player(new Point(0,1), Direction.Down, 2));
            ParseAllGameState(gameState, "","M");
            var monkey = gameState[0, 0];
            Assert.IsNotNull(monkey);
            Assert.AreEqual(new Point(0,0),monkey.Location );
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,1),monkey.Location );
            Assert.AreEqual(1, gameState.Player.Health);
        }

        [Test]
        public void MonkeyShouldDieByStone()
        {
            var gameState = new GameState(1,3, new Player(new Point(-5,-5), Direction.Down, 2));
            ParseAllGameState(gameState, "S","\n\nM");
            var monkey = gameState[2, 0];
            var stone = gameState[0, 0];
            Assert.IsNotNull(monkey);
            gameState.UpdateState();
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,2),stone.Location);
            Assert.IsNull(gameState[2,0]);
        }

        [Test]
        public void MonkeyShouldBeStoppedByWeapon()
        {
            var player = new Player(new Point(0, 1), Direction.Up);
            var gameState = new GameState(1,2, player);
            ParseAllGameState(gameState, "","M");
            var monkey = gameState[0, 0];
            Assert.IsNotNull(monkey);
            player.AddWeapon(new Hammer());
            player.UseWeapon(gameState);
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,0), monkey.Location);
        }
        
        [Test]
        public void MonkeyShouldSeePlayerBy4Cells()
        {
            var gameState = new GameState(1,4,new Player(new Point(0,3),Direction.Down));
            ParseAllGameState(gameState, "","R");
            var redSnake = gameState[0, 0];
            Assert.IsNotNull(redSnake);
            for (var i = 1; i < 4; i++)
            {
                gameState.UpdateState();
                Assert.AreEqual(new Point(0,i),redSnake.Location);
            }
            Assert.AreEqual(2, gameState.Player.Health);
        }
    }
}