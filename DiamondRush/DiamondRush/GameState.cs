using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static DiamondRush.Resources;

namespace DiamondRush
{
    public class GameState
    {
        public static int Coefficient = 45;
        public int Width { get; }
        public int Height { get; }
        private readonly List<IGameObject> gameObjectsToRemove = new List<IGameObject>();
        private readonly List<IGameObject> gameObjectsToAdd = new List<IGameObject>();
        private readonly HashSet<IGameObject> gameObjects = new HashSet<IGameObject>();
        public Player Player { get; }

        public GameState(int width, int height, Player player)
        {
            Width = width;
            Height = height;
            Player = player;
        }
        
        public IGameObject this[Point point] => gameObjects.FirstOrDefault(x => x.Location == point);
        public IGameObject this[int x, int y] => this[new Point(y, x)];

        public void UpdateState()
        {
            UpdateCollections();
            foreach (var gameObject in gameObjects)
            {
                if (gameObject is ICanMove environmentMove)
                    environmentMove.Move(this);
            }
        }

        public bool InBounds(Point point)
            => point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;
        
        public void AddGameObject(IGameObject gameObject) => gameObjectsToAdd.Add(gameObject);
        public void RemoveGameObject(IGameObject gameObject) => gameObjectsToRemove.Add(gameObject);

        public void UpdateCollections()
        {
            foreach (var gameObject in gameObjectsToRemove)
                gameObjects.Remove(gameObject);
            
            gameObjectsToRemove.Clear();
            foreach (var gameObject in gameObjectsToAdd)
                gameObjects.Add(gameObject);
            gameObjectsToAdd.Clear();
        }
        public void Draw(Graphics graphics)
        {
            foreach (var gameObject in gameObjects)
            {
                graphics.DrawImage(Images[gameObject.ImageName], new Point(gameObject.Location.X*Coefficient,
                    gameObject.Location.Y*Coefficient));
            }
            
            graphics.DrawImage(Images[Player.ImageName],
                new Point(Coefficient * Player.Location.X, Coefficient * Player.Location.Y));
        }
    }
}