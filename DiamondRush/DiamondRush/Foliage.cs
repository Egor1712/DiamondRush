namespace DiamondRush
{
    public class Foliage : IEnvironment
    {
        public int Priority { get; set; }
        public bool IsDisappearInConflict(creature creature) => creature is Player;
        public string ImageName { get; }

        public Foliage()
        {
            ImageName = "Grass";
        }

        public void Move(GameState gameState)
        {
        }

    }
}