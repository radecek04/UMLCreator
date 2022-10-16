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
        public ClassAnchor StartAnchor { get; set; }
        public ClassAnchor EndAnchor { get; set; }
        public Pen LinePen { get; set; }

        protected ClassSettings _settings = ClassSettings.Instance;

        public Relationship(ClassAnchor start)
        {
            StartAnchor = start;
        }

        public void Draw(Graphics g)
        {
            if (Math.Abs(StartAnchor.XCoeficient) == Math.Abs(EndAnchor.XCoeficient) && StartAnchor.XCoeficient != 0)
            {
                int x = EndAnchor.X - StartAnchor.X;
                g.DrawLine(LinePen, StartAnchor.X, StartAnchor.Y, StartAnchor.X + x / 2, StartAnchor.Y);
                g.DrawLine(LinePen, EndAnchor.X, EndAnchor.Y, EndAnchor.X - x / 2, EndAnchor.Y);
                g.DrawLine(LinePen, StartAnchor.X + x / 2, StartAnchor.Y, EndAnchor.X - x / 2, EndAnchor.Y);
            }
            else if (Math.Abs(StartAnchor.YCoeficient) == Math.Abs(EndAnchor.YCoeficient) && StartAnchor.YCoeficient != 0)
            {
                int y = EndAnchor.Y - StartAnchor.Y;
                g.DrawLine(LinePen, StartAnchor.X, StartAnchor.Y, StartAnchor.X, StartAnchor.Y + y / 2);
                g.DrawLine(LinePen, EndAnchor.X, EndAnchor.Y, EndAnchor.X, EndAnchor.Y - y / 2);
                g.DrawLine(LinePen, StartAnchor.X, StartAnchor.Y + y / 2, EndAnchor.X, EndAnchor.Y - y / 2);
            }
            else
            {
                g.DrawLine(LinePen, StartAnchor.X, StartAnchor.Y, StartAnchor.XCoeficient != 0 ? EndAnchor.X : StartAnchor.X, StartAnchor.YCoeficient != 0 ? EndAnchor.Y : StartAnchor.Y);
                g.DrawLine(LinePen, EndAnchor.X, EndAnchor.Y, EndAnchor.XCoeficient == 0 ? EndAnchor.X : StartAnchor.X, EndAnchor.YCoeficient == 0 ? EndAnchor.Y : StartAnchor.Y);
            }

            DrawLineEnd(g);
        }

        public bool CheckCollision(int x, int y)
        {
            if (Math.Abs(StartAnchor.XCoeficient) == Math.Abs(EndAnchor.XCoeficient) && StartAnchor.XCoeficient != 0)
            {
                ClassAnchor left = StartAnchor.X < EndAnchor.X ? StartAnchor : EndAnchor;
                ClassAnchor right = StartAnchor.X < EndAnchor.X ? EndAnchor : StartAnchor;
                ClassAnchor up = StartAnchor.Y < EndAnchor.Y ? StartAnchor : EndAnchor;
                ClassAnchor down = StartAnchor.Y < EndAnchor.Y ? EndAnchor : StartAnchor;

                int bendingPoint = (right.X - left.X) / 2;
                
                if(left.X < x && left.X + bendingPoint >= x && left.Y - _settings.LINE_MARGIN < y && left.Y + _settings.LINE_MARGIN >= y)
                    return true;
                if(right.X >= x && right.X - bendingPoint < x && right.Y - _settings.LINE_MARGIN < y && right.Y + _settings.LINE_MARGIN >= y)
                    return true;
                if (left.X + bendingPoint - _settings.LINE_MARGIN < x && left.X + bendingPoint + _settings.LINE_MARGIN >= x &&
                    up.Y - _settings.LINE_MARGIN < y && down.Y + _settings.LINE_MARGIN >= y)
                    return true;

                Debug.WriteLine($"No Collision: {x} {y}");

                return false;
            }
            else if (Math.Abs(StartAnchor.YCoeficient) == Math.Abs(EndAnchor.YCoeficient) && StartAnchor.YCoeficient != 0)
            {
                ClassAnchor left = StartAnchor.X < EndAnchor.X ? StartAnchor : EndAnchor;
                ClassAnchor right = StartAnchor.X < EndAnchor.X ? EndAnchor : StartAnchor;
                ClassAnchor up = StartAnchor.Y < EndAnchor.Y ? StartAnchor : EndAnchor;
                ClassAnchor down = StartAnchor.Y < EndAnchor.Y ? EndAnchor : StartAnchor;

                int bendingPoint = (down.Y - up.Y) / 2;

                if (up.Y < y && up.Y + bendingPoint >= y && up.X - _settings.LINE_MARGIN < x && up.X + _settings.LINE_MARGIN >= x)
                    return true;
                if (down.Y >= y && down.Y - bendingPoint < y && down.X - _settings.LINE_MARGIN < x && down.X + _settings.LINE_MARGIN >= x)
                    return true;
                if (up.Y + bendingPoint - _settings.LINE_MARGIN < y && up.Y + bendingPoint + _settings.LINE_MARGIN >= y &&
                    left.X - _settings.LINE_MARGIN < x && right.X + _settings.LINE_MARGIN >= x)
                    return true;

                Debug.WriteLine($"No Collision: {x} {y}");
                return false;
            }
            else
            {
                ClassAnchor left = StartAnchor.X < EndAnchor.X ? StartAnchor : EndAnchor;
                ClassAnchor right = StartAnchor.X < EndAnchor.X ? EndAnchor : StartAnchor;
                ClassAnchor up = StartAnchor.Y < EndAnchor.Y ? StartAnchor : EndAnchor;
                ClassAnchor down = StartAnchor.Y < EndAnchor.Y ? EndAnchor : StartAnchor;

                if (up.Y - _settings.LINE_MARGIN < y && down.Y + _settings.LINE_MARGIN >= y && (up.YCoeficient == 0 ? down.X : up.X) - _settings.LINE_MARGIN < x && (up.YCoeficient == 0 ? down.X : up.X) + _settings.LINE_MARGIN >= x)
                    return true;
                if (left.X - _settings.LINE_MARGIN < x && right.X + _settings.LINE_MARGIN >= x && (left.XCoeficient == 0 ? right.Y : left.Y) - _settings.LINE_MARGIN < y && (left.XCoeficient == 0 ? right.Y : left.Y) + _settings.LINE_MARGIN >= y)
                    return true;

                return false;
            }
            return false;
        }

        protected virtual void DrawLineEnd(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}
