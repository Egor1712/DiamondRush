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
        public MyForm()
        {
            CreateAllImages();
            WindowState = FormWindowState.Maximized;
            var size = Screen.GetWorkingArea(Bounds);
            GameState = new GameState(size.Width/30, size.Height/30,
                new Player(new Point(5,5),Direction.Right ));
            Timer = new Timer
            {
                Interval = 10
            };
            
            Paint += (sender, args) => GameState.Draw(args.Graphics); 
            Timer.Tick += (sender, args) => GameState.UpdateState();
            Timer.Tick += (sender, args) => Invalidate();
            Timer.Start();
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            GameState.Player.ChangeDirection(e.KeyCode);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            GameState.Player.Move(GameState, CharToDirection[e.KeyChar]);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BackgroundImage = Images["Grass"];
            DoubleBuffered = true;
        }
    }
}