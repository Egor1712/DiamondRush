using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        private readonly HashSet<IEnvironment> environments = new HashSet<IEnvironment>();
        private readonly HashSet<ICreature> creatures = new HashSet<ICreature>();
        private readonly List<ICreature> creaturesToRemove = new List<ICreature>();
        private readonly List<ICreature> creaturesToAdd = new List<ICreature>();
        public Player Player { get; }

        public GameState(int width, int height, Player player)
        {
            Width = width;
            Height = height;
            Player = player;
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
                        var creature = CharToCreature(rowsCr[i][j], new Point(j, i));
                        if (creature != null)
                            creatures.Add(creature);
                    }
                    if (i < rowsEnv.Length && j < rowsEnv[i].Length)
                    {
                        var environment = CharToEnvironment(rowsEnv[i][j], new Point(j, i));
                        if (environment != null)
                            environments.Add(environment);
                    }
                    
                }
            }
        }
        
        public (IEnvironment Enviroment, ICreature Creature) this[Point point] =>
            (environments.FirstOrDefault(x => x.Location == point),
                creatures.FirstOrDefault(x => x.Location == point));

        public (IEnvironment Enviroment, ICreature Creature) this[int x, int y] => this[new Point(y, x)];

        public void UpdateState()
        {
            foreach (var environment in environments)
                environment.Move(this);   

            foreach (var creature in creatures)
                creature.Move(this);

            foreach (var creature in creaturesToRemove)
                creatures.Remove(creature);
            
            creaturesToRemove.Clear();
            foreach (var creature in creaturesToAdd)
                creatures.Add(creature);
            creaturesToAdd.Clear();
        }

        public bool InBounds(Point point)
            => point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;
        
        public void AddCreature(ICreature creature) => creaturesToAdd.Add(creature);
        public void RemoveEnvironment(IEnvironment environment)
        {
            environments.Remove(environment);
        }

        public void RemoveCreature(ICreature creature) => creaturesToRemove.Add(creature);

        public void Draw(Graphics graphics)
        {
            foreach (var creature in creatures)
                graphics.DrawImage(Images[creature.ImageName],
                    new Point(creature.Location.X*Coefficient, creature.Location.Y*Coefficient));
            
            foreach (var environment in environments)
                graphics.DrawImage(Images[environment.ImageName],
                    new Point(environment.Location.X*Coefficient, environment.Location.Y*Coefficient));
            
            graphics.DrawImage(Images[Player.ImageName],
                new Point(Coefficient * Player.Location.X, Coefficient * Player.Location.Y));
        }
    }
}