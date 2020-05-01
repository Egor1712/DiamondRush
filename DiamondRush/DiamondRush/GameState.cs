using System.Collections.Generic;
using System.Drawing;
using DiamondRush.Creatures;
using DiamondRush.Environments;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public class GameState
    {
        private static int Coefficient = 45;
        private int Width { get; }
        private int Height { get; }
        private readonly IEnvironment[,] environments;
        private readonly ICreature[,] creatures;
        private readonly HashSet<IEnvironment> environmentsHs = new HashSet<IEnvironment>();
        private readonly HashSet<ICreature> creaturesHs = new HashSet<ICreature>();
        private readonly List<ICreature> creaturesToRemove = new List<ICreature>();
        private readonly List<ICreature> creaturesToAdd = new List<ICreature>();
        public Player Player { get; }

        public GameState(int width, int height, Player player)
        {
            Width = width;
            Height = height;
            Player = player;
            environments = new IEnvironment[Height, Width];
            creatures = new ICreature[Height, Width];
        }

        public void ParseAllGameState(string mapOfEnvironments, string mapOfCreatures)
        {
            var rowsEnv = mapOfEnvironments.Split(new[] {'\n'});
            var rowsCr = mapOfCreatures.Split(new[] {'\n'});
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    if (i < rowsCr.Length && j < rowsCr[i].Length)
                    {
                        creatures[i, j] = CharToCreature(rowsCr[i][j], new Point(j, i));
                        if (creatures[i, j] != null)
                            creaturesHs.Add(creatures[i, j]);
                    }
                    if (i < rowsEnv.Length && j < rowsEnv[i].Length)
                    {
                        environments[i, j] = CharToEnvironment(rowsEnv[i][j], new Point(j, i));
                        if (environments[i, j] != null)
                            environmentsHs.Add(environments[i, j]);
                    }
                    
                }
            }
        }
        
        public (IEnvironment Enviroment, ICreature Creature) this[Point point] =>
            (environments[point.Y, point.X], creatures[point.Y, point.X]);
        
        public (IEnvironment Enviroment, ICreature Creature) this[int x, int y] =>
            (environments[x, y], creatures[x,y]);

        public void UpdateState()
        {
            foreach (var environment in environmentsHs)
            {
                environment.Move(this);   
            }

            foreach (var creature in creaturesHs)
            {
                creature.Move(this);
            }

            foreach (var creature in creaturesToRemove)
            {
                creaturesHs.Remove(creature);
                creatures[creature.Location.Y, creature.Location.X] = null;
            }
            creaturesToRemove.Clear();
            foreach (var creature in creaturesToAdd)
            {
                creaturesHs.Add(creature);
            }
            creaturesToAdd.Clear();
        }

        public bool InBounds(Point point)
            => point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;

        

        public void MoveCreature(Point oldLocation, Point newLocation)
        {
            creatures[newLocation.Y, newLocation.X] = creatures[oldLocation.Y, oldLocation.X];
            creatures[oldLocation.Y, oldLocation.X] = null;
        }

        public void MoveEnvironment(Point oldLocation, Point newLocation)
        {
            environments[newLocation.Y, newLocation.X] = environments[oldLocation.Y, oldLocation.X];
            environments[oldLocation.Y, oldLocation.X] = null;
        }


        public void AddCreature(ICreature creature) => creaturesToAdd.Add(creature);
        public void RemoveEnvironment(IEnvironment environment)
        {
            environmentsHs.Remove(environment);
            environments[environment.Location.Y, environment.Location.X] = null;
        }

        public void RemoveCreature(ICreature creature) => creaturesToRemove.Add(creature);

        public void Draw(Graphics graphics)
        {
            foreach (var creature in creaturesHs)
                graphics.DrawImage(Images[creature.ImageName],
                    new Point(creature.Location.X*Coefficient, creature.Location.Y*Coefficient));
            
            foreach (var environment in environmentsHs)
                graphics.DrawImage(Images[environment.ImageName],
                    new Point(environment.Location.X*Coefficient, environment.Location.Y*Coefficient));
            
            graphics.DrawImage(Images[Player.ImageName],
                new Point(Coefficient * Player.Location.X, Coefficient * Player.Location.Y));
        }
    }
}