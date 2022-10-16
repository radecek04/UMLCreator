using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLCreator
{
    public class ClassAnchor
    {
        public Class Parent { get; set; }
        private ClassSettings _settings = ClassSettings.Instance;
        public int X { get; set; }
        public int Y { get; set; }
        public int XCoeficient { get; private set; }
        public int YCoeficient { get; private set; }

        public ClassAnchor(Class parent, int x, int y, int xCoeficient, int yCoeficient)
        {
            Parent = parent;
            X = x;
            Y = y;
            XCoeficient = xCoeficient;
            YCoeficient = yCoeficient;
        }

        public void Draw(Graphics g)
        {
            Pen pen = new Pen(Color.Red, 5);

            int lineXStart = X;
            int lineYStart = Y;
            int lineXEnd = X + (_settings.ANCHOR_HEIGHT - _settings.ANCHOR_ARROW_HEAD) * XCoeficient;
            int lineYEnd = Y + (_settings.ANCHOR_HEIGHT - _settings.ANCHOR_ARROW_HEAD) * YCoeficient;

            g.DrawLine(pen, lineXStart , lineYStart, lineXEnd, lineYEnd);

            Point ArrowTop = new Point(X + _settings.ANCHOR_HEIGHT * XCoeficient, Y + _settings.ANCHOR_HEIGHT * YCoeficient);
            int x = X + (_settings.ANCHOR_HEIGHT - _settings.ANCHOR_ARROW_HEAD) * XCoeficient;
            int y = Y + (_settings.ANCHOR_HEIGHT - _settings.ANCHOR_ARROW_HEAD) * YCoeficient;
            Point ArrowSide1 = new Point(x - Math.Abs(_settings.ANCHOR_WIDTH / 2 * YCoeficient), y - Math.Abs(_settings.ANCHOR_WIDTH / 2 * XCoeficient));
            Point ArrowSide2 = new Point(x + Math.Abs(_settings.ANCHOR_WIDTH / 2 * YCoeficient), y + Math.Abs(_settings.ANCHOR_WIDTH / 2 * XCoeficient));


            g.FillPolygon(Brushes.Red, new Point[] { ArrowTop, ArrowSide1, ArrowSide2 });
        }
        
        public void DrawPoint(Graphics g)
        {
            g.FillEllipse(Brushes.SeaGreen, X - _settings.ANCHOR_WIDTH / 2, Y - _settings.ANCHOR_WIDTH / 2,_settings.ANCHOR_WIDTH,_settings.ANCHOR_WIDTH);
            g.DrawEllipse(_settings.BORDER_PEN, X - _settings.ANCHOR_WIDTH / 2, Y - _settings.ANCHOR_WIDTH / 2,_settings.ANCHOR_WIDTH,_settings.ANCHOR_WIDTH);
        }

        public bool CheckCollision(int x, int y)
        {
            if(YCoeficient != 0)
            {
                if (x <= X - _settings.ANCHOR_WIDTH || x > X + _settings.ANCHOR_WIDTH)
                    return false;
                if (y <= Y - (_settings.ANCHOR_HEIGHT - _settings.ANCHOR_HEIGHT * YCoeficient) / 2 || y > Y + (_settings.ANCHOR_HEIGHT + _settings.ANCHOR_HEIGHT * YCoeficient) / 2)
                    return false;
            }
            else
            {
                if (x <= X - (_settings.ANCHOR_HEIGHT - _settings.ANCHOR_HEIGHT * XCoeficient) / 2 || x > X + (_settings.ANCHOR_HEIGHT + _settings.ANCHOR_HEIGHT * XCoeficient) / 2)
                    return false;
                if (y <= Y - _settings.ANCHOR_WIDTH || y > Y + _settings.ANCHOR_WIDTH)
                    return false;
            }

            return true;
        }

        public bool CheckPointCollision(int x, int y)
        {
            if (x < X - _settings.ANCHOR_WIDTH || x >= X + _settings.ANCHOR_WIDTH)
                return false;
            if (y < Y - _settings.ANCHOR_WIDTH || y >= Y + _settings.ANCHOR_WIDTH)
                return false;
            return true;
        }
    }
}
