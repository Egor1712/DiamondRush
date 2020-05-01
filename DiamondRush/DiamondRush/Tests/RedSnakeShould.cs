using System.Drawing;
using DiamondRush.Weapon;
using NUnit.Framework;

namespace DiamondRush.Tests
{
    [TestFixture]
    public class RedSnakeShould
    {
        [Test]
        public void RedSnakeShouldBeatPlayer()
        {
            var gameState = new GameState(1,2, new Player(new Point(0,1), Direction.Down, 2));
            gameState.ParseAllGameState("","R");
            var monkey = gameState[0, 0].Creature;
            Assert.IsNotNull(monkey);
            Assert.AreEqual(new Point(0,0),monkey.Location );
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,1),monkey.Location );
            Assert.AreEqual(1, gameState.Player.Health);
        }

        [Test]
        public void RedSnakeShouldDieByStone()
        {
            var gameState = new GameState(1,3, new Player(new Point(-5,-5), Direction.Down, 2));
            gameState.ParseAllGameState("S","\n\nR");
            var monkey = gameState[2, 0].Creature;
            var stone = gameState[0, 0].Enviroment;
            Assert.IsNotNull(monkey);
            gameState.UpdateState();
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,2),stone.Location);
            Assert.IsNull(gameState[2,0].Creature);
        }

        [Test]
        public void RedSnakeShouldBeStoppedByWeapon()
        {
            var player = new Player(new Point(0, 1), Direction.Up);
            var gameState = new GameState(1,2, player);
            gameState.ParseAllGameState("","M");
            var monkey = gameState[0, 0].Creature;
            Assert.IsNotNull(monkey);
            player.AddWeapon(new Hammer());
            player.UseWeapon(gameState);
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,0), monkey.Location);
        }

        [Test]
        public void RedSnakeShouldSeePlayerBy10Cells()
        {
            var gameState = new GameState(1,10,new Player(new Point(0,9),Direction.Down));
            gameState.ParseAllGameState("","R");
            var redSnake = gameState[0, 0].Creature;
            Assert.IsNotNull(redSnake);
            for (var i = 1; i < 10; i++)
            {
                gameState.UpdateState();
                Assert.AreEqual(new Point(0,i),redSnake.Location);
            }
            Assert.AreEqual(2, gameState.Player.Health);
        }
    }
}