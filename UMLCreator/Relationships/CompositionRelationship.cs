using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLCreator.Relationships
{
    public class CompositionRelationship : Relationship
    {
        public CompositionRelationship(Class start) : base(start)
        {
            LinePen = _settings.LINE_PEN;
        }

        protected override void DrawLineEnd(Graphics g)
        {
            Pen pen = new Pen(LinePen.Color, 2);
            Point top = new Point(End.X, End.Y);
            Point bottom;
            Point side1;
            Point side2;
            if (Direction == Direction.Left)
            {
                side1 = new Point(End.X - _settings.LINE_ENDING_WIDTH, End.Y - _settings.LINE_ENDING_LENGTH);
                side2 = new Point(End.X - _settings.LINE_ENDING_WIDTH, End.Y + _settings.LINE_ENDING_LENGTH);
                bottom = new Point(End.X - _settings.LINE_ENDING_WIDTH * 2, End.Y);
                g.FillPolygon(LinePen.Brush, new Point[] { top, side1, bottom, side2 });
            }
            else if (Direction == Direction.Right)
            {
                side1 = new Point(End.X + _settings.LINE_ENDING_WIDTH, End.Y - _settings.LINE_ENDING_LENGTH);
                side2 = new Point(End.X + _settings.LINE_ENDING_WIDTH, End.Y + _settings.LINE_ENDING_LENGTH);
                bottom = new Point(End.X + _settings.LINE_ENDING_WIDTH * 2, End.Y);
                g.FillPolygon(LinePen.Brush, new Point[] { top, side1, bottom, side2 });
            }
            else if (Direction == Direction.Up)
            {
                side1 = new Point(End.X - _settings.LINE_ENDING_LENGTH, End.Y - _settings.LINE_ENDING_WIDTH);
                side2 = new Point(End.X + _settings.LINE_ENDING_LENGTH, End.Y - _settings.LINE_ENDING_WIDTH);
                bottom = new Point(End.X, End.Y - _settings.LINE_ENDING_WIDTH * 2);
                g.FillPolygon(LinePen.Brush, new Point[] { top, side1, bottom, side2 });
            }
            else if (Direction == Direction.Down)
            {
                side1 = new Point(End.X - _settings.LINE_ENDING_LENGTH, End.Y + _settings.LINE_ENDING_WIDTH);
                side2 = new Point(End.X + _settings.LINE_ENDING_LENGTH, End.Y + _settings.LINE_ENDING_WIDTH);
                bottom = new Point(End.X, End.Y + _settings.LINE_ENDING_WIDTH * 2);
                g.FillPolygon(LinePen.Brush, new Point[] { top, side1, bottom, side2 });
            }
        }
    }
}
