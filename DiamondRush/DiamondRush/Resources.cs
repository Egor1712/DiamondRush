﻿using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DiamondRush.Creatures;
using DiamondRush.Environments;
using DiamondRush.Weapon;

namespace DiamondRush
{
    public static class Resources
    {
        public static readonly Dictionary<Direction, Point> DirectionToPoints = new Dictionary<Direction, Point>
        {
            [Direction.Down] = new Point(0, 1),
            [Direction.Up] = new Point(0, -1),
            [Direction.Left] = new Point(-1, 0),
            [Direction.Right] = new Point(1, 0)
        };

        public static readonly Dictionary<Direction, Direction> OppositeDirection = new Dictionary<Direction, Direction>
        {
            [Direction.Down] = Direction.Up,
            [Direction.Left] = Direction.Right,
            [Direction.Right] = Direction.Left,
            [Direction.Up] = Direction.Down
        };

        public static readonly Dictionary<Keys, Direction> KeysToDirection = new Dictionary<Keys, Direction>
        {
            [Keys.W] = Direction.Up,
            [Keys.D] = Direction.Right,
            [Keys.A] = Direction.Left,
            [Keys.S] = Direction.Down
        };

        public static readonly Dictionary<string, Image> Images = new Dictionary<string, Image>();
        public static string MapOfEnvironment;
        public static string MapOfCreatures;

        public static void CreateAllImages()
        {
            var source = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            var files = new DirectoryInfo(source).GetFiles();
            foreach (var file in files)
            {
                Images.Add(file.Name.Substring(0, file.Name.Length - 4),
                    Image.FromFile(Path.Combine(source, file.Name)));
            }
        }

        private static IGameObject CharToEnvironment(char symbol, Point location)
        {
            switch (symbol)
            {
                case 'S':
                    return new Stone {Location = location};
                case 'F':
                    return new Foliage(location);
                case 'D':
                    return new Diamond(location);
                case 'C':
                    return new CheckPoint(location);
                case 'W':
                    return new Wall(location);
                case 'H':
                    return new Chest(location, new Hammer());
                case 'R':
                    return new Chest(location, new FrozenHammer());
                case 'E':
                    return new FragileWall(location);
            }

            return null;
        }

        public static IGameObject CharToCreature(char symbol, Point location, Direction direction = Direction.Down)
        {
            switch (symbol)
            {
                case 'S':
                    return new SimpleSnake(location, direction);
                case 'M':
                    return new Monkey(location, direction);
                case 'R':
                    return new RedSnake(location, direction);
                case 'A':
                    return new Archer(location, direction);
            }

            return null;
        }

        public static void CreateAllMaps()
        {
            var source = Path.Combine(Directory.GetCurrentDirectory(), "Maps");
            var files = new DirectoryInfo(source).GetFiles();
            foreach (var file in files)
            {
                var stream = file.OpenText();
                if (file.Name == "Environment.txt")
                    MapOfEnvironment = stream.ReadToEnd();
                else
                    MapOfCreatures = stream.ReadToEnd();
            }
        }

        public static void ParseAllGameState(GameState gameState, string mapOfEnvironments, string mapOfCreatures)
        {
            var rowsEnv = mapOfEnvironments.Split(new[] {'\n'});
            var rowsCr = mapOfCreatures.Split(new[] {'\n'});
            for (var i = 0; i < gameState.Height; i++)
            {
                for (var j = 0; j < gameState.Width; j++)
                {
                    if (i < rowsCr.Length && j < rowsCr[i].Length)
                    {
                        var creature = CharToCreature(rowsCr[i][j], new Point(j, i));
                        if (creature != null)
                            gameState.AddGameObject(creature);
                    }

                    if (i < rowsEnv.Length && j < rowsEnv[i].Length)
                    {
                        var environment = CharToEnvironment(rowsEnv[i][j], new Point(j, i));
                        if (environment != null)
                            gameState.AddGameObject(environment);
                    }
                }
            }

            gameState.UpdateCollections();
        }
    }
}