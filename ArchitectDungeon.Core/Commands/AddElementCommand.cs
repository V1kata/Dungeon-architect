namespace architectSteps.Commands
{
    public class AddElementCommand : ICommand
    {
        private DungeonScene _scene;
        private DungeonElement _element;

        public AddElementCommand(DungeonScene scene, DungeonElement element)
        {
            _scene = scene;
            _element = element;
        }

        public void Execute()
        {
            _scene.AddElement(_element);
        }

        public void Undo()
        {
            _scene.RemoveElement(_element);
        }
    }
}
