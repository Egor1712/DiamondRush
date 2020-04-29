using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;
using static System.ValueTuple;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public class GameState
    {
        public static int Coefficient = 15;
        public int Width { get; }
        public int Height { get; }
        private readonly IEnvironment[,] enviroments;
        private readonly ICreature[,] creatures;
        private readonly HashSet<IEnvironment> environmentsHS;
        private readonly HashSet<ICreature> creaturesHS;
        public Player Player;

        public GameState(int width, int height, Player player)
        {
            Width = width;
            Height = height;
            Player = player;
            enviroments = new IEnvironment[Height, Width];
            creatures = new ICreature[Height, Width];
            environmentsHS = new HashSet<IEnvironment>();
            creaturesHS = new HashSet<ICreature>();
        }

        public void ParseEnvironment(string mapOfEnvironments)
        {
            var rows = mapOfEnvironments.Split(new[] {'\n'});
            for (var i = 0; i < Height; i++)
            {
                if (i >= rows.Length)
                    return;
                var row = rows[i];
                for (var j = 0; j < Width; j++)
                {
                    if (j >= row.Length) continue;
                    enviroments[i,j] = CharToEnvironment(row[j], new Point(j,i));
                    if ( enviroments[i,j] != null)
                        environmentsHS.Add(enviroments[i, j]);
                }
            }
        }

        public void ParseCreatures(string mapOfCreatures)
        {
            var rows = mapOfCreatures.Split(new[] {'\n'});
            for (var i = 0; i < Height; i++)
            {
                if (i >= rows.Length)
                    return;
                var row = rows[i];
                for (var j = 0; j < Width; j++)
                {
                    if (j >= row.Length) continue;
                    creatures[i,j] = CharToCreature(row[j], new Point(j,i));
                    if ( creatures[i,j] != null)
                        creaturesHS.Add(creatures[i, j]);
                }
            }
        }


        public (IEnvironment Enviroment, ICreature Creature) this[Point point] =>
            (enviroments[point.X, point.Y], creatures[point.X, point.Y]);
        public (IEnvironment Enviroment, ICreature Creature) this[int x, int y] =>
            (enviroments[x, y], creatures[x,y]);

        public void UpdateState()
        {
            foreach (var environment in environmentsHS)
            {
                environment.Move(this);   
            }

            foreach (var creature in creaturesHS)
            {
                creature.Move(this);
            }
        }

        public bool InBounds(Point point)
            => point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;

        public bool InBounds(int x, int y)
            => x >= 0 && x < Width && y >= 0 && y < Height;

        public void MoveCreature(Point oldLocation, Point newLocation)
        {
            creatures[newLocation.Y, newLocation.X] = creatures[oldLocation.Y, oldLocation.X];
            creatures[oldLocation.Y, oldLocation.X] = null;
        }

        public void MoveEnvironment(Point oldLocation, Point newLocation)
        {
            enviroments[newLocation.Y, newLocation.X] = enviroments[oldLocation.Y, oldLocation.X];
            enviroments[oldLocation.Y, oldLocation.X] = null;
        }

        public void RemoveEnvironment(IEnvironment environment)
        {
            environmentsHS.Remove(environment);
            enviroments[environment.Location.Y, environment.Location.X] = null;
        }

        public void RemoveCreature(ICreature creature)
        {
            creaturesHS.Remove(creature);
            creatures[creature.Location.Y, creature.Location.X] = null;
        }

        public void Draw(Graphics graphics)
        {
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    var environment = enviroments[j, i];
                    var creature = creatures[j, i];
                    if (creature != null)
                        graphics.DrawImage(Images[creature.ImageName], new Point(45 * i, 45 * j));
                    if (environment != null)
                        graphics.DrawImage(Images[environment.ImageName], new Point(45 * i, 45 * j));
                }
            }

            graphics.DrawImage(Images[Player.ImageName], new Point(45 * Player.Location.X, 45 * Player.Location.Y));
        }
    }
}