using System.Drawing;
using DiamondRush.Creatures;
using DiamondRush.Weapon;
using NUnit.Framework;
using static DiamondRush.Resources;

namespace DiamondRush.Tests
{
    [TestFixture]
    public class HammerShould
    {
        [Test]
        public void HammerShouldStopCreature()
        {
            var gameState = new GameState(2, 3, new Player(new Point(0, 0), Direction.Right));
            ParseAllGameState(gameState, ""," S");
            var snake = gameState[0, 1] as SimpleSnake;
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
            ParseAllGameState(gameState, ""," S");
            var snake = gameState[0, 1] as SimpleSnake;
            Assert.IsNotNull(snake);
            gameState.Player.AddWeapon(new FrozenHammer());
            gameState.Player.UseWeapon(gameState);
            gameState.UpdateState();
            Assert.AreEqual(new Point(1,0), snake.Location);
            Assert.IsTrue(snake.IsFrozen);
        }
    }
}