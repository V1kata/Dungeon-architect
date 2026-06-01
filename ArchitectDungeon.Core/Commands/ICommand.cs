namespace DungeonArchitect
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
