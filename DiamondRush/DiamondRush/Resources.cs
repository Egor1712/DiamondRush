using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DiamondRush
{
    public static class Resources
    {
        public static readonly Dictionary<Direction, Point> DirectionToPoints = new Dictionary<Direction, Point>
        {
            [Direction.Down] = new Point(0,1),
            [Direction.Up] = new Point(0,-1),
            [Direction.Left] = new Point(-1,0),
            [Direction.Right] = new Point(1,0)
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
        
        public static readonly Dictionary<char, Direction> CharToDirection =  new Dictionary<char, Direction>
        {
            ['w'] = Direction.Up,
            ['d'] = Direction.Right,
            ['a'] = Direction.Left,
            ['s'] = Direction.Down
        };

        public static readonly Dictionary<string, Image> Images = new Dictionary<string, Image>();

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
    }
}