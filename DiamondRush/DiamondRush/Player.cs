using System.Collections.Generic;
using System.Drawing;
using DiamondRush.Environments;
using DiamondRush.Weapon;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public class Player
    {
        public event WeaponChanged NotifyWeaponChanged;
        public event ScoreChanged NotifyScoreChanged;
        public event HealthChanged NotifyHealthChanged;
        public Point Location { get; private set; }
        public Direction Direction { get; private set; }
        public int Health { get; private set; }
        public int Score { get; private set; }
        private Point LastCheckPoint { get; set; }
        public Weapon.Weapon CurrentWeapon { get; private set; }
        private readonly List<Weapon.Weapon> weapons = new List<Weapon.Weapon>();

        public string ImageName => $"Player{Direction}";

        public Player(Point location, Direction direction, int health = 10)
        {
            Location = location;
            Direction = direction;
            Health = health;
        }

        public void AddWeapon(Weapon.Weapon weapon)
        {
            weapons.Add(weapon);
            CurrentWeapon = weapon;
            NotifyWeaponChanged?.Invoke();
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
            NotifyHealthChanged?.Invoke();
        }

        public void ChangeCheckPoint(Point point) => LastCheckPoint = point;

        public void Move(GameState gameState, Direction direction)
        {
            Direction = direction;
            var nextPoint = new Point(Location.X + DirectionToPoints[direction].X,
                Location.Y + DirectionToPoints[direction].Y);
            if (!gameState.InBounds(nextPoint)) return;
            var gameObject = gameState[nextPoint];
            if (gameObject != null && gameObject is ICanCollapseWithPlayer collapseWithPlayer)
            {
                if (collapseWithPlayer is Stone stone && !stone.CanCollapseWithPlayer(this, gameState))
                    return;
                Location = nextPoint;
                collapseWithPlayer.CollapseWithPlayer(gameState);
            }

            if (gameObject == null)
                Location = nextPoint;
        }

        public void UseWeapon(GameState gameState)
        {
            if (CurrentWeapon is Democracy)
            {
                gameState.UseWeaponForAllCreatures(CurrentWeapon);
                return;
            }

            CurrentWeapon?.DoWork(gameState, Direction);
        }

        public void AddScore(int score)
        {
            Score += score;
            NotifyScoreChanged?.Invoke();
        }

        public void ChangeWeapon()
        {
            if (weapons.Count == 0)
                return;
            var index = weapons.IndexOf(CurrentWeapon);
            if (index + 1 < weapons.Count)
            {
                CurrentWeapon = weapons[index + 1];
                NotifyWeaponChanged?.Invoke();
                return;
            }

            CurrentWeapon = weapons[0];
            NotifyWeaponChanged?.Invoke();
        }

        public void ChangeDirection(Direction direction)
        {
            Direction = direction;
        }
    }
}