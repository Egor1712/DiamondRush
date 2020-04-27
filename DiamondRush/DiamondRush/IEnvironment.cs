namespace DiamondRush
{
    public interface IEnvironment : IDrawable
    { 
        int Priority { get; set; }
        bool IsDisappearInConflict(creature creature);
        void Move(GameState gameState);
    }
}