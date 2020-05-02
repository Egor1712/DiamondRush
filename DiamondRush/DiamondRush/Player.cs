using System.Collections.Generic;
using System.Drawing;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public class Player
    {
        public Point Location { get; private set; }
        public Direction Direction { get; private set; }
        public int Health { get; private set; }
        public int Score { get; private set; }
        private Point LastCheckPoint { get;  set; }
        public Weapon.Weapon CurrentWeapon { get; private set; }
        private readonly List<Weapon.Weapon> weapons = new List<Weapon.Weapon>();
        public string ImageName => $"Player{Direction}";

        public Player(Point location, Direction direction, int health = 3)
        {
            Location = location;
            Direction = direction;
            Health = health;
        }

        public void AddWeapon(Weapon.Weapon weapon)
        {
            weapons.Add(weapon);
            CurrentWeapon = weapon;
        }

        private void ResetPlayer()
        {
            Location = LastCheckPoint;
            Health = 3;
        }
        
        public void BeatPlayer()
        {
            if (Health > 1)
                Health--;
            else
               ResetPlayer();
        }

        public void ChangeCheckPoint(Point point) => LastCheckPoint = point;

        public void Move(GameState gameState, Direction direction)
        {
            Direction = direction;
            var nextPoint = new Point(Location.X + DirectionToPoints[direction].X,
                Location.Y + DirectionToPoints[direction].Y);
            if (!gameState.InBounds(nextPoint)) return;
            (var environment, var creature) = gameState[nextPoint];
            if (environment != null && environment.IsCollapseWithPlayer(this, gameState))
            {
                Location = nextPoint;
                environment.DoLogicWhenCollapseWithPlayer(gameState);
            }
            if (creature != null && creature.IsCollapseWithPlayer(gameState, this))
            {
                Location = nextPoint;
                creature.DoLogicWhenCollapseWithPlayer(gameState);
            }
                
            if (environment == null && creature == null)
                Location = nextPoint;
        }
        
        public void UseWeapon(GameState gameState)
        {
            CurrentWeapon?.DoWork(gameState, Direction);
        }

        public void AddScore(int score)
        {
            Score += score;
        }
        
        public void ChangeDirection(Direction direction)
        {
            Direction = direction;
        }
    }
}