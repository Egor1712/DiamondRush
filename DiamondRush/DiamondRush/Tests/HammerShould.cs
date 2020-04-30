using System.Drawing;
using NUnit.Framework;

namespace DiamondRush.Tests
{
    [TestFixture]
    public class HammerShould
    {
        [Test]
        public void HammerShouldStopCreature()
        {
            var gameState = new GameState(2, 3, new Player(new Point(0, 0), Direction.Right));
            gameState.ParseCreatures(" S");
            var snake = gameState[0, 1].Creature as SimpleSnake;
            Assert.IsNotNull(snake);
            gameState.Player.AddWeapon(new Hammer());
            gameState.Player.UseWeapon(gameState);
            gameState.UpdateState();
            Assert.AreEqual(new Point(1,0), snake.Location);
        }
        
        [Test]
        public void FrozenHammerShouldFrozeCreature()
        {
            var gameState = new GameState(2, 3, new Player(new Point(0, 0), Direction.Right));
            gameState.ParseCreatures(" S");
            var snake = gameState[0, 1].Creature as SimpleSnake;
            Assert.IsNotNull(snake);
            gameState.Player.AddWeapon(new FrozenHammer());
            gameState.Player.UseWeapon(gameState);
            gameState.UpdateState();
            Assert.AreEqual(new Point(1,0), snake.Location);
            Assert.IsTrue(snake.IsFrozen);
        }
    }
}