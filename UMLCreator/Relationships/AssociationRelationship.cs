using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLCreator.Relationships
{
    public class AssociationRelationship : Relationship
    {
        public AssociationRelationship(Class start) : base(start)
        {
            LinePen = _settings.LINE_PEN;
        }

        protected override void DrawLineEnd(Graphics g)
        {
            Pen pen = new Pen(LinePen.Color, 2);
            if (Direction == Direction.Left)
            {
                g.DrawLine(pen, End.X, End.Y, End.X - _settings.LINE_ENDING_WIDTH, End.Y - _settings.LINE_ENDING_LENGTH);
                g.DrawLine(pen, End.X, End.Y, End.X - _settings.LINE_ENDING_WIDTH, End.Y + _settings.LINE_ENDING_LENGTH);
            }
            else if (Direction == Direction.Right)
            {
                g.DrawLine(pen, End.X, End.Y, End.X + _settings.LINE_ENDING_WIDTH, End.Y - _settings.LINE_ENDING_LENGTH);
                g.DrawLine(pen, End.X, End.Y, End.X + _settings.LINE_ENDING_WIDTH, End.Y + _settings.LINE_ENDING_LENGTH);
            }
            else if (Direction == Direction.Up)
            {
                g.DrawLine(pen, End.X, End.Y, End.X - _settings.LINE_ENDING_LENGTH, End.Y - _settings.LINE_ENDING_WIDTH);
                g.DrawLine(pen, End.X, End.Y, End.X + _settings.LINE_ENDING_LENGTH, End.Y - _settings.LINE_ENDING_WIDTH);
            }
            else if (Direction == Direction.Down)
            {
                g.DrawLine(pen, End.X, End.Y, End.X - _settings.LINE_ENDING_LENGTH, End.Y + _settings.LINE_ENDING_WIDTH);
                g.DrawLine(pen, End.X, End.Y, End.X + _settings.LINE_ENDING_LENGTH, End.Y + _settings.LINE_ENDING_WIDTH);
            }
        }
    }
}
