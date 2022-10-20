﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLCreator.Relationships
{
    internal class ImplementationRelationship : Relationship
    {
        public ImplementationRelationship(Class start) : base(start)
        {
            LinePen = _settings.LINE_PEN_DASHED;
        }

        protected override void DrawLineEnd(Graphics g)
        {
            Pen pen = new Pen(LinePen.Color, 2);
            Point top = new Point(End.X, End.Y);
            Point side1;
            Point side2;
            if (Direction == Direction.Left)
            {
                side1 = new Point(End.X - _settings.LINE_ENDING_WIDTH, End.Y - _settings.LINE_ENDING_LENGTH);
                side2 = new Point(End.X - _settings.LINE_ENDING_WIDTH, End.Y + _settings.LINE_ENDING_LENGTH);
                g.FillPolygon(Brushes.White, new Point[] { top, side1, side2 });
                g.DrawPolygon(pen, new Point[] { top, side1, side2 });
            }
            else if (Direction == Direction.Right)
            {
                side1 = new Point(End.X + _settings.LINE_ENDING_WIDTH, End.Y - _settings.LINE_ENDING_LENGTH);
                side2 = new Point(End.X + _settings.LINE_ENDING_WIDTH, End.Y + _settings.LINE_ENDING_LENGTH);
                g.FillPolygon(Brushes.White, new Point[] { top, side1, side2 });
                g.DrawPolygon(pen, new Point[] { top, side1, side2 });
            }
            else if (Direction == Direction.Up)
            {
                side1 = new Point(End.X - _settings.LINE_ENDING_LENGTH, End.Y - _settings.LINE_ENDING_WIDTH);
                side2 = new Point(End.X + _settings.LINE_ENDING_LENGTH, End.Y - _settings.LINE_ENDING_WIDTH);
                g.FillPolygon(Brushes.White, new Point[] { top, side1, side2 });
                g.DrawPolygon(pen, new Point[] { top, side1, side2 });
            }
            else if (Direction == Direction.Down)
            {
                side1 = new Point(End.X - _settings.LINE_ENDING_LENGTH, End.Y + _settings.LINE_ENDING_WIDTH);
                side2 = new Point(End.X + _settings.LINE_ENDING_LENGTH, End.Y + _settings.LINE_ENDING_WIDTH);
                g.FillPolygon(Brushes.White, new Point[] { top, side1, side2 });
                g.DrawPolygon(pen, new Point[] { top, side1, side2 });
            }
        }
    }
}
