using System;
using System.Drawing;
using System.Windows.Forms;
using DiamondRush.Weapon;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public partial class MyForm : Form
    {
        private GameState gameState;
        private readonly FlowLayoutPanel layout = new FlowLayoutPanel();
        private readonly  Label weaponLabel= new Label();
        private readonly Label scoreLabel = new Label();
        private Rectangle size;

        public MyForm()
        {
            CreateAllMaps();
            CreateAllImages();
            SetLayout();
            SetGameState();
            SetWeaponLabel();
            SetScoreLabel();
            WindowState = FormWindowState.Maximized;
            var timer = new Timer
            {
                Interval = 100
            };
            Paint += (sender, args) => gameState.Draw(args.Graphics);
            timer.Tick += (sender, args) =>
            {
                gameState.UpdateState();
                Invalidate();
            };
            timer.Start();
            Controls.Add(layout);
            InitializeComponent();
        }

        private void SetGameState()
        { 
            size = Screen.GetBounds(Location);
            gameState = new GameState(size.Width / GameState.Coefficient, 
                (size.Height - layout.Height)/ GameState.Coefficient - 2,
                new Player(new Point(2, 0), Direction.Right,100));
            gameState.Player.NotifyWeaponChanged += () => layout.Invalidate();
            gameState.Player.NotifyScoreChanged += UpdateScore;
            gameState.Player.NotifyHealthChanged += () => layout.Invalidate();
            gameState.Player.AddWeapon(new FrozenHammer());
            gameState.Player.AddWeapon(new Democracy());
            ParseAllGameState(gameState,MapOfEnvironment,MapOfCreatures);
        }

        private void SetWeaponLabel()
        {
            weaponLabel.ForeColor = Color.DarkRed;
            weaponLabel.Text = "Current Weapon:";
            weaponLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            weaponLabel.AutoSize = true;
            weaponLabel.Margin = new Padding(0,0,70,0);  
        }

        private void SetScoreLabel()
        {
            scoreLabel.ForeColor = Color.DarkRed;
            scoreLabel.AutoSize = true;
            scoreLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            scoreLabel.Location = new Point(weaponLabel.Width+100, 0);
            scoreLabel.TextAlign = ContentAlignment.MiddleCenter;
            scoreLabel.Text = "Score:\n0";
        }
        
        private void SetLayout()
        {
            layout.Controls.Add(weaponLabel);
            layout.Controls.Add(scoreLabel);
            layout.Dock = DockStyle.Bottom;
            layout.BackColor = Color.DarkGreen;
            layout.Paint += (sender, args) => UpdateImage(args.Graphics);
            layout.Paint += (sender, args) => DrawHealth(args.Graphics);
        }

        private void UpdateImage(Graphics graphics)
        {
            if (gameState.Player.CurrentWeapon != null)
                graphics.DrawImage(Images[gameState.Player.CurrentWeapon.ImageName], new Point(weaponLabel.Width, 0));
        }

        private void UpdateScore() => scoreLabel.Text = $"Score:\n{gameState.Player.Score}";

        private void DrawHealth(Graphics graphics)
        {
            var location = new Point(scoreLabel.Right+20, 0);
            for (var i = 0; i < gameState.Player.Health && location.X+ 60*i < size.Width; i++)
            {
                graphics.DrawImage(Images["Health"], new Point(location.X+i*60, 0));
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.E)
            {
                gameState.Player.UseWeapon(gameState);
                return;
            }

            if (e.KeyCode == Keys.Tab)
            {
                gameState.Player.ChangeWeapon();
                return;
            }
            if (!KeysToDirection.ContainsKey(e.KeyCode)) return;
            var direction = KeysToDirection[e.KeyCode];
            gameState.Player.ChangeDirection(direction);
            gameState.Player.Move(gameState, direction);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BackgroundImage = Images["Grass"];
            DoubleBuffered = true;
            Text = "Use WASD to control, E to use weapon, Tab to change weapon";
        }
    }
}