using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLCreator.Relationships
{
    public class Relationship
    {
        public Class StartClass { get; set; }
        public Class EndClass { get; set; }
        public string StartCardinal { get; set; }
        public string EndCardinal { get; set; }
        protected Point Start;
        protected Point End;
        protected Point BendingPoint;
        [NonSerialized]
        public Pen LinePen;
        public Direction Direction { get; set; }

        protected ClassSettings _settings = ClassSettings.Instance;

        public Relationship(Class start)
        {
            StartClass = start;

            StartCardinal = "";
            EndCardinal = "";
        }

        public void Draw(Graphics g)
        {
            if (StartClass.Background.IntersectsWith(EndClass.Background))
                return;

            Rectangle smallX = StartClass.Background.Width < EndClass.Background.Width ? StartClass.Background : EndClass.Background;
            Rectangle smallY = StartClass.Background.Height < EndClass.Background.Height ? StartClass.Background : EndClass.Background;
            Rectangle largeX = smallX == StartClass.Background ? EndClass.Background : StartClass.Background;
            Rectangle largeY = smallY == StartClass.Background ? EndClass.Background : StartClass.Background;

            int xDiff = 0;
            int yDiff = 0;

            if (smallX.X >= largeX.X && smallX.X + smallX.Width < largeX.X + largeX.Width)
            {
                xDiff = smallX.X + smallX.Width / 2;
            }
            else if (smallX.X >= largeX.X)
            {
                xDiff = smallX.X + (largeX.X + largeX.Width - smallX.X) / 2;
            }
            else if (smallX.X + smallX.Width < largeX.X + largeX.Width)
            {
                xDiff = smallX.X + smallX.Width - (smallX.X + smallX.Width - largeX.X) / 2;
            }

            if (smallY.Y >= largeY.Y && smallY.Y + smallY.Height < largeY.Y + largeY.Height)
            {
                yDiff = smallY.Y + smallY.Height / 2;
            }
            else if (smallY.Y >= largeY.Y)
            {
                yDiff = smallY.Y + (largeY.Y + largeY.Height - smallY.Y) / 2;
            }
            else if (smallY.Y + smallY.Height < largeY.Y + largeY.Height)
            {
                yDiff = smallY.Y + smallY.Height - (smallY.Y + smallY.Height - largeY.Y) / 2;
            }

            if (xDiff > largeX.X && xDiff < largeX.X + largeX.Width)
            {
                if (smallX.Y >= largeX.Y)
                {
                    if(smallX == StartClass.Background)
                    {
                        Start = new Point(xDiff, smallX.Y);
                        End = new Point(xDiff, largeX.Y + largeX.Height);
                        Direction = Direction.Down;
                    }
                    else
                    {
                        End = new Point(xDiff, smallX.Y);
                        Start = new Point(xDiff, largeX.Y + largeX.Height);
                        Direction = Direction.Up;
                    }
                }
                else if (smallX.Y + smallX.Height < largeX.Y)
                {
                    if (smallX == StartClass.Background)
                    {
                        Start = new Point(xDiff, smallX.Y + smallX.Height);
                        End = new Point(xDiff, largeX.Y);
                        Direction = Direction.Up;
                    }
                    else
                    {
                        End = new Point(xDiff, smallX.Y + smallX.Height);
                        Start = new Point(xDiff, largeX.Y);
                        Direction = Direction.Down;
                    }
                }
                BendingPoint = Start;
                g.DrawLine(LinePen, Start, End);
            }
            else if (yDiff > largeY.Y && yDiff < largeY.Y + largeY.Height)
            {
                if (smallY.X >= largeY.X)
                {
                    if (smallY == StartClass.Background)
                    {
                        Start = new Point(smallY.X, yDiff);
                        End = new Point(largeY.X + largeY.Width, yDiff);
                        Direction = Direction.Right;
                    }
                    else
                    {
                        End = new Point(smallY.X, yDiff);
                        Start = new Point(largeY.X + largeY.Width, yDiff);
                        Direction = Direction.Left;
                    }
                }
                else if (smallY.X + smallY.Width < largeY.X)
                {
                    if (smallY == StartClass.Background)
                    {
                        Start = new Point(smallY.X + smallY.Width, yDiff);
                        End = new Point(largeY.X, yDiff);
                        Direction = Direction.Left;
                    }
                    else
                    {
                        End = new Point(smallY.X + smallY.Width, yDiff);
                        Start = new Point(largeY.X, yDiff);
                        Direction = Direction.Right;
                    }
                }
                BendingPoint = Start;
                g.DrawLine(LinePen, Start, End);
            }
            else
            {
                if (StartClass.Background.X < EndClass.Background.X)
                {
                    if(StartClass.Background.Y + StartClass.Background.Height > EndClass.Background.Y)
                        Start = new Point(StartClass.Background.X + StartClass.Background.Width / 2, StartClass.Background.Y);
                    else
                        Start = new Point(StartClass.Background.X + StartClass.Background.Width / 2, StartClass.Background.Y + StartClass.Background.Height);
                    End = new Point(EndClass.Background.X, EndClass.Background.Y + EndClass.Background.Height / 2);
                    
                    Direction = Direction.Left;
                }
                else
                {
                    if (StartClass.Background.Y + StartClass.Background.Height > EndClass.Background.Y)
                        Start = new Point(StartClass.Background.X + StartClass.Background.Width / 2, StartClass.Background.Y);
                    else
                        Start = new Point(StartClass.Background.X + StartClass.Background.Width / 2, StartClass.Background.Y + StartClass.Background.Height);
                    End = new Point(EndClass.Background.X + EndClass.Background.Width, EndClass.Background.Y + EndClass.Background.Height / 2);
                    Direction = Direction.Right;
                }
                BendingPoint = new Point(Start.X, End.Y);
                g.DrawLine(LinePen, Start, BendingPoint);
                g.DrawLine(LinePen, End, BendingPoint);
            }

            DrawCandinarls(g);
            
            DrawLineEnd(g);
        }

        public bool CheckCollision(int x, int y)
        {
            Rectangle click = new Rectangle(x, y, 0, 0);
            Rectangle line = new Rectangle(0,0,0,0);
            int yDiff = Start.Y - BendingPoint.Y;

            if(yDiff < 0)
            {
                line = Rectangle.FromLTRB(Start.X, Start.Y, BendingPoint.X, BendingPoint.Y);
                line.Inflate(_settings.LINE_MARGIN, _settings.LINE_MARGIN);
            }
            else
            {
                line = Rectangle.FromLTRB(BendingPoint.X, BendingPoint.Y, Start.X, Start.Y);
                line.Inflate(_settings.LINE_MARGIN, _settings.LINE_MARGIN);
            }

            if (click.IntersectsWith(line) && BendingPoint.Y != Start.Y)
            {
                return true;
            }


            if (Direction == Direction.Right || Direction == Direction.Down)
            {
                line = Rectangle.FromLTRB(End.X, End.Y, BendingPoint.X, BendingPoint.Y);
                line.Inflate(_settings.LINE_MARGIN, _settings.LINE_MARGIN);
            }
            else if(Direction == Direction.Left || Direction == Direction.Up)
            {
                line = Rectangle.FromLTRB(BendingPoint.X, BendingPoint.Y, End.X, End.Y);
                line.Inflate(_settings.LINE_MARGIN, _settings.LINE_MARGIN);
            }

            if(click.IntersectsWith(line))
            {
                return true;
            }

            return false;

        }

        private void DrawCandinarls(Graphics g)
        {
            SizeF start = g.MeasureString(StartCardinal, _settings.CARDINAL_FONT);
            SizeF end = g.MeasureString(EndCardinal, _settings.CARDINAL_FONT);

            if(Direction == Direction.Left)
            {
                if(Start.Y < End.Y)
                    g.DrawString(StartCardinal, _settings.CARDINAL_FONT, LinePen.Brush, Start.X + _settings.CARDINAL_MARGIN, Start.Y + _settings.CARDINAL_MARGIN);
                else
                    g.DrawString(StartCardinal, _settings.CARDINAL_FONT, LinePen.Brush, Start.X + _settings.CARDINAL_MARGIN, Start.Y - _settings.CARDINAL_MARGIN - start.Height);
                g.DrawString(EndCardinal, _settings.CARDINAL_FONT, LinePen.Brush, End.X - _settings.CARDINAL_MARGIN - end.Width, End.Y - _settings.CARDINAL_MARGIN - end.Height);
            }
            else if (Direction == Direction.Right)
            {
                if (Start.Y < End.Y)
                    g.DrawString(StartCardinal, _settings.CARDINAL_FONT, LinePen.Brush, Start.X - _settings.CARDINAL_MARGIN - start.Width, Start.Y + _settings.CARDINAL_MARGIN);
                else
                    g.DrawString(StartCardinal, _settings.CARDINAL_FONT, LinePen.Brush, Start.X - _settings.CARDINAL_MARGIN - start.Width, Start.Y - _settings.CARDINAL_MARGIN - start.Height);
                g.DrawString(EndCardinal, _settings.CARDINAL_FONT, LinePen.Brush, End.X + _settings.CARDINAL_MARGIN, End.Y - _settings.CARDINAL_MARGIN - end.Height);
            }
            else if (Direction == Direction.Up)
            {
                g.DrawString(StartCardinal, _settings.CARDINAL_FONT, LinePen.Brush, Start.X + _settings.CARDINAL_MARGIN, Start.Y + _settings.CARDINAL_MARGIN);
                g.DrawString(EndCardinal, _settings.CARDINAL_FONT, LinePen.Brush, End.X + _settings.CARDINAL_MARGIN, End.Y - _settings.CARDINAL_MARGIN - end.Height);
            }
            else if (Direction == Direction.Down)
            {
                g.DrawString(StartCardinal, _settings.CARDINAL_FONT, LinePen.Brush, Start.X + _settings.CARDINAL_MARGIN, Start.Y - _settings.CARDINAL_MARGIN - end.Height);
                g.DrawString(EndCardinal, _settings.CARDINAL_FONT, LinePen.Brush, End.X + _settings.CARDINAL_MARGIN, End.Y + _settings.CARDINAL_MARGIN);
            }
        }

        protected virtual void DrawLineEnd(Graphics g)
        {
            throw new NotImplementedException();
        }
    }

    public enum Direction 
    {
        Left,
        Right,
        Up,
        Down
    }
}
