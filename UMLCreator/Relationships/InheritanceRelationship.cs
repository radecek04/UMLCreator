using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLCreator.Relationships
{
    public class InheritanceRelationship : Relationship
    {
        public InheritanceRelationship(ClassAnchor start) : base(start)
        {
            LinePen = _settings.LINE_PEN;
        }

        protected override void DrawLineEnd(Graphics g)
        {
            Pen pen = new Pen(LinePen.Color, 2);
            Point top = new Point(EndAnchor.X, EndAnchor.Y);
            Point side1;
            Point side2;
            if (EndAnchor.XCoeficient == -1)
            {
                side1 = new Point(EndAnchor.X - _settings.LINE_ENDING_WIDTH, EndAnchor.Y - _settings.LINE_ENDING_LENGTH);
                side2 = new Point(EndAnchor.X - _settings.LINE_ENDING_WIDTH, EndAnchor.Y + _settings.LINE_ENDING_LENGTH);
                g.FillPolygon(Brushes.White, new Point[] { top, side1, side2 });
                g.DrawPolygon(pen, new Point[] { top, side1, side2 });
            }
            else if (EndAnchor.XCoeficient == 1)
            {
                side1 = new Point(EndAnchor.X + _settings.LINE_ENDING_WIDTH, EndAnchor.Y - _settings.LINE_ENDING_LENGTH);
                side2 = new Point(EndAnchor.X + _settings.LINE_ENDING_WIDTH, EndAnchor.Y + _settings.LINE_ENDING_LENGTH);
                g.FillPolygon(Brushes.White, new Point[] { top, side1, side2 });
                g.DrawPolygon(pen, new Point[] { top, side1, side2 });
            }
            else if (EndAnchor.YCoeficient == -1)
            {
                side1 = new Point(EndAnchor.X - _settings.LINE_ENDING_LENGTH, EndAnchor.Y - _settings.LINE_ENDING_WIDTH);
                side2 = new Point(EndAnchor.X + _settings.LINE_ENDING_LENGTH, EndAnchor.Y - _settings.LINE_ENDING_WIDTH);
                g.FillPolygon(Brushes.White, new Point[] { top, side1, side2 });
                g.DrawPolygon(pen, new Point[] { top, side1, side2 });
            }
            else if (EndAnchor.YCoeficient == 1)
            {
                side1 = new Point(EndAnchor.X - _settings.LINE_ENDING_LENGTH, EndAnchor.Y + _settings.LINE_ENDING_WIDTH);
                side2 = new Point(EndAnchor.X + _settings.LINE_ENDING_LENGTH, EndAnchor.Y + _settings.LINE_ENDING_WIDTH);
                g.FillPolygon(Brushes.White, new Point[] { top, side1, side2 });
                g.DrawPolygon(pen, new Point[] { top, side1, side2 });
            }
        }
    }
}
