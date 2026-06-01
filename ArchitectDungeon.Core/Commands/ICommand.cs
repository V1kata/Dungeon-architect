namespace architectSteps
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
