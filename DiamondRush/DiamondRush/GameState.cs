using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.ValueTuple;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public class GameState
    {
        public static int Coefficient = 15;
        public int Width { get; }
        public int Height { get;  }
        private readonly IEnvironment[,] enviroments;
        private readonly creature[,] creatures;
        public Player Player;

        public GameState(int width, int height, Player player)
        {
            Width = width;
            Height = height;
            Player = player;
            enviroments = new IEnvironment[Width, Height];
            creatures = new creature[Width,Height];
        }


        public (IEnvironment Enviroment, creature Creature) this[Point point] =>
            (enviroments[point.X, point.Y], creatures[point.X, point.Y]);

        public void UpdateState()
        {
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    if (creatures[i,j] != null)
                        creatures[i,j].Move(this);
                }
            }
        }

        public bool InBounds(Point point)
            => point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;

        public bool IsValid(Point point, creature creature) =>
            enviroments[point.X, point.Y].IsDisappearInConflict(creature);

        public void MoveCreature(Point oldLocation, Point newLocation)
        {
            creatures[newLocation.X, newLocation.Y] = creatures[oldLocation.X, oldLocation.Y];
            creatures[oldLocation.X, oldLocation.Y] = null;
        }

        public void MoveEnviroment(Point oldLocation, Point newLocation)
        {
            enviroments[newLocation.X, newLocation.Y] = enviroments[oldLocation.X, oldLocation.Y];
            enviroments[oldLocation.X, oldLocation.Y] = null;
        }

        public void Draw(Graphics graphics)
        {
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    var environment = enviroments[i, j];
                    var creature = creatures[i, j];
                    if (creature != null)
                        graphics.DrawImage(Images[creature.ImageName], new Point(30*i, 30*j));
                    if (environment != null)
                        graphics.DrawImage(Images[environment.ImageName], new Point(30*i, 30*j));
                }
            }
            graphics.DrawImage(Images[Player.ImageName], new Point(30*Player.Location.X, 30*Player.Location.Y));
        }
    }
}