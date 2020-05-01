using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public class Player
    {
        public Point Location { get; set; }
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

        public void ResetPlayer()
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
            if (gameState.InBounds(nextPoint))
            {
                (var environment, var creature) = gameState[nextPoint.Y,nextPoint.X];
                if (environment == null)
                {
                    if (creature != null)
                    {
                        creature.CollapseWithPlayer(gameState, gameState.Player);
                        return;
                    }

                    Location = nextPoint;
                    return;
                }
                if (creature == null)
                    environment.CollapseWithPlayer(gameState,this);
            }
        }

        public IEnumerable<Weapon.Weapon> EnumerateAllWeapons() => weapons;

        public void ChangeWeapon(Weapon.Weapon weapon) => CurrentWeapon = weapon;
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