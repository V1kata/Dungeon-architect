namespace DungeonArchitect.Commands
{
    public class RemoveElementCommand : ICommand
    {
        private DungeonScene _scene;
        private DungeonElement _element;

        public RemoveElementCommand(DungeonScene scene, DungeonElement element)
        {
            _scene = scene;
            _element = element;
        }

        public void Execute()
        {
            _scene.RemoveElement(_element);
        }

        public void Undo()
        {
            _scene.AddElement(_element);
        }
    }
}
