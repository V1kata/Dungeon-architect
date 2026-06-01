using System.Drawing;

namespace DungeonArchitect.Commands
{
    public class MoveElementCommand : ICommand
    {
        private DungeonElement _element;
        private Point _startPos;
        private Point _endPos;

        public MoveElementCommand(DungeonElement element, Point startPos, Point endPos)
        {
            _element = element;
            _startPos = startPos;
            _endPos = endPos;
        }

        public void Execute()
        {
            _element.X = _endPos.X;
            _element.Y = _endPos.Y;
        }

        public void Undo()
        {
            _element.X = _startPos.X;
            _element.Y = _startPos.Y;
        }
    }
}
