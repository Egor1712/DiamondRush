using System;
using System.Drawing;
using System.Windows.Forms;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public partial class MyForm : Form
    {
        public GameState GameState;
        public Timer Timer;
        private FlowLayoutPanel layout = new FlowLayoutPanel();

        public MyForm()
        {
            CreateAllMaps();
            CreateAllImages();
            WindowState = FormWindowState.Maximized;
            var size = Screen.GetBounds(Location);
            GameState = new GameState(size.Width / 45, size.Height / 45 - 1,
                new Player(new Point(5, 5), Direction.Right));
            Timer = new Timer
            {
                Interval = 80
            };

            GameState.ParseEnvironment(MapOfEnvironment);
            GameState.ParseCreatures(MapOfCreatures);
            Paint += (sender, args) => GameState.Draw(args.Graphics);
            Timer.Tick += (sender, args) => GameState.UpdateState();
            Timer.Tick += (sender, args) => Invalidate();
            Timer.Start();
            layout.Location = new Point(Bounds.Bottom, Bounds.Left);
            SetLayout();
            InitializeComponent();
        }

        private void SetLayout()
        {
            var label = new LinkLabel();
            label.Text = "CurrentWeapon";
            if (GameState.Player.CurrentWeapon != null)
                label.Image = Images[GameState.Player.CurrentWeapon.ImageName];
            layout.Controls.Add(label);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            if (e.KeyCode == Keys.E)
            {
                GameState.Player.UseWeapon(GameState);
                return;
            }
            var direction = KeysToDirection[e.KeyCode];
            GameState.Player.ChangeDirection(direction);
            GameState.Player.Move(GameState, direction);
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BackgroundImage = Images["Grass"];
            DoubleBuffered = true;
        }
    }
}