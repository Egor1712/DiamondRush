using System;
using System.Drawing;
using System.Windows.Forms;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public partial class MyForm : Form
    {
        private readonly GameState gameState;
        private readonly Timer timer;
        private readonly FlowLayoutPanel layout = new FlowLayoutPanel();

        public MyForm()
        {
            CreateAllMaps();
            CreateAllImages();
            WindowState = FormWindowState.Maximized;
            var size = Screen.GetBounds(Location);
            gameState = new GameState(size.Width / 45, size.Height / 45 - 1,
                new Player(new Point(5, 5), Direction.Right));
            timer = new Timer
            {
                Interval = 80
            };

            gameState.ParseEnvironment(MapOfEnvironment);
            gameState.ParseCreatures(MapOfCreatures);
            Paint += (sender, args) => gameState.Draw(args.Graphics);
            timer.Tick += (sender, args) => gameState.UpdateState();
            timer.Tick += (sender, args) => Invalidate();
            timer.Start();
            layout.Location = new Point(Bounds.Bottom, Bounds.Left);
            SetLayout();
            InitializeComponent();
        }

        private void SetLayout()
        {
            var label = new LinkLabel();
            label.Text = "CurrentWeapon";
            if (gameState.Player.CurrentWeapon != null)
                label.Image = Images[gameState.Player.CurrentWeapon.ImageName];
            layout.Controls.Add(label);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.E)
            {
                gameState.Player.UseWeapon(gameState);
                return;
            }

            var direction = KeysToDirection[e.KeyCode];
            gameState.Player.ChangeDirection(direction);
            gameState.Player.Move(gameState, direction);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BackgroundImage = Images["Grass"];
            DoubleBuffered = true;
        }
    }
}