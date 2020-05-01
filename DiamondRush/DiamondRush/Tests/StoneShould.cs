using System.Drawing;
using NUnit.Framework;

namespace DiamondRush.Tests
{
    [TestFixture]
    public class StoneShould
    {
        [Test]
        public void StoneShouldFall()
        {
            var gameState = new GameState(1,2,new Player(new Point(-1,-1), Direction.Down ));
            gameState.ParseAllGameState(@"S","");
            var stone = gameState[0, 0].Enviroment;
            Assert.AreEqual(new Point(0,0),  stone.Location);
            gameState.UpdateState();
            Assert.AreEqual(null, gameState[0,0].Enviroment);
            Assert.AreEqual(new Point(0,1),stone.Location );
        }

        [Test]
        public void StoneShouldNotFallIntoPlayer()
        {
            var gameState = new GameState(1,2,new Player(new Point(0,1), Direction.Left ));
            gameState.ParseAllGameState(@"S", "");
            var stone = gameState[0, 0].Enviroment;
            Assert.AreEqual(new Point(0,0), stone.Location);
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,0), stone.Location );
            Assert.AreEqual(3, gameState.Player.Health);
        }

        [Test]
        public void StoneShouldBeatPlayer()
        {
            var gameState = new GameState(1,3,new Player(new Point(0,2), Direction.Down ));
            gameState.ParseAllGameState(@"S", "");
            var stone = gameState[0, 0].Enviroment;
            Assert.AreEqual(new Point(0,0), stone.Location );
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,1), stone.Location );
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,1), stone.Location);
            Assert.AreEqual(2, gameState.Player.Health);
        }

        [Test]
        public void StoneShouldKillSnake()
        {
            var gameState = new GameState(1,3,new Player(new Point(-1,-1), Direction.Down ));
            gameState.ParseAllGameState(@"S", "\n\nS");
            var stone = gameState[0, 0].Enviroment;
            var snake = gameState[2, 0].Creature;
            Assert.AreEqual(new Point(0,0) ,stone.Location );
            Assert.AreEqual(new Point(0,2), snake.Location );
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,1), stone.Location);
            Assert.AreEqual(new Point(0,2),snake.Location );
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,2),stone.Location );
            Assert.IsNull(gameState[2,0].Creature);
        }

        [Test]
        public void StoneShouldNotCollapseWithAnotherStone()
        {
            var gameState = new GameState(1,3, new Player(new Point(-1,-1), Direction.Down ));
            gameState.ParseAllGameState("S\n\nS", "");
            var firstStone = gameState[0, 0].Enviroment;
            var secondStone = gameState[2, 0].Enviroment;
            Assert.AreEqual(new Point(0,0),firstStone.Location );
            Assert.AreEqual(new Point(0,2), secondStone.Location);
            gameState.UpdateState();
            Assert.AreEqual(new Point(0,1), firstStone.Location );
            Assert.AreEqual(new Point(0,2), secondStone.Location);
        }
    }
}