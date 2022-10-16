using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLCreator.Relationships
{
    public class DependencyRelationship : Relationship
    {
        public DependencyRelationship(ClassAnchor start) : base(start)
        {
            LinePen = _settings.LINE_PEN_DASHED;
        }

        protected override void DrawLineEnd(Graphics g)
        {
            Pen pen = new Pen(LinePen.Color, 2);
            if (EndAnchor.XCoeficient == -1)
            {
                g.DrawLine(pen, EndAnchor.X, EndAnchor.Y, EndAnchor.X - _settings.LINE_ENDING_WIDTH, EndAnchor.Y - _settings.LINE_ENDING_LENGTH);
                g.DrawLine(pen, EndAnchor.X, EndAnchor.Y, EndAnchor.X - _settings.LINE_ENDING_WIDTH, EndAnchor.Y + _settings.LINE_ENDING_LENGTH);
            }
            else if (EndAnchor.XCoeficient == 1)
            {
                g.DrawLine(pen, EndAnchor.X, EndAnchor.Y, EndAnchor.X + _settings.LINE_ENDING_WIDTH, EndAnchor.Y - _settings.LINE_ENDING_LENGTH);
                g.DrawLine(pen, EndAnchor.X, EndAnchor.Y, EndAnchor.X + _settings.LINE_ENDING_WIDTH, EndAnchor.Y + _settings.LINE_ENDING_LENGTH);
            }
            else if (EndAnchor.YCoeficient == -1)
            {
                g.DrawLine(pen, EndAnchor.X, EndAnchor.Y, EndAnchor.X - _settings.LINE_ENDING_LENGTH, EndAnchor.Y - _settings.LINE_ENDING_WIDTH);
                g.DrawLine(pen, EndAnchor.X, EndAnchor.Y, EndAnchor.X + _settings.LINE_ENDING_LENGTH, EndAnchor.Y - _settings.LINE_ENDING_WIDTH);
            }
            else if (EndAnchor.YCoeficient == 1)
            {
                g.DrawLine(pen, EndAnchor.X, EndAnchor.Y, EndAnchor.X - _settings.LINE_ENDING_LENGTH, EndAnchor.Y + _settings.LINE_ENDING_WIDTH);
                g.DrawLine(pen, EndAnchor.X, EndAnchor.Y, EndAnchor.X + _settings.LINE_ENDING_LENGTH, EndAnchor.Y + _settings.LINE_ENDING_WIDTH);
            }
        }
    }
}
