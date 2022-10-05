using Accessibility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace UMLCreator
{
    public class Class
    {
        public string Name { get; private set; }
        public List<Property> Properties { get; set; }
        public List<Method> Methods { get; set; }
        public bool IsAbstract { get; set; }
        public Rectangle Background { get; set; }

        public int MinWidth { get; private set; }
        public int MinHeight { get; private set; }

        private ClassSettings _settings;
        private Graphics _graphics;

        public Class(string name, bool isAbstract, Graphics graphics)
        {
            this.Name = name;
            this.Properties = new List<Property>();
            this.Methods = new List<Method>();
            this.IsAbstract = isAbstract;
            this._settings = ClassSettings.Instance;
            this.Background = _settings.BACKGROUND;
            this._graphics = graphics;

            Adjust();
        }

        public void Draw(Graphics g)
        {
            // Draw background for class diagram node
            g.FillRectangle(Brushes.DeepSkyBlue, Background);
            g.DrawRectangle(_settings.BORDER_PEN, Background);

            // Draw in name of the class
            SizeF className = g.MeasureString(Name, IsAbstract ? _settings.ABSTRACT_FONT : _settings.NAME_FONT);
            g.DrawString(Name, IsAbstract ? _settings.ABSTRACT_FONT : _settings.NAME_FONT, Brushes.Black, Background.X + Background.Width / 2 - className.Width / 2, Background.Y + _settings.CLASSNAME_HEIGHT / 2 - className.Height / 2);
            g.DrawLine(_settings.BORDER_PEN, Background.X, Background.Y + _settings.CLASSNAME_HEIGHT, Background.X + Background.Width, Background.Y + _settings.CLASSNAME_HEIGHT);

            // Draw in all properties of the class
            float lineY = Background.Y + _settings.CLASSNAME_HEIGHT + _settings.LINESPACING;
            foreach (Property property in Properties)
            {
                g.DrawString(property.ToString(), _settings.FONT, Brushes.Black, _settings.NAME_MARGIN + Background.X, lineY);
                lineY += (int)g.MeasureString(property.ToString(), _settings.FONT).Height + _settings.LINESPACING;
            }

            // Draw divider between properties and methods
            g.DrawLine(_settings.DIVIDER_PEN, Background.X, lineY, Background.X + Background.Width - 1, lineY);
            lineY += _settings.DIVIDER_PEN.Width;

            // Draw in all methods of the class
            foreach (Method method in Methods)
            {
                g.DrawString(method.ToString(), _settings.FONT, Brushes.Black, _settings.NAME_MARGIN + Background.X, lineY + _settings.LINESPACING);
                lineY += (int)g.MeasureString(method.ToString(), _settings.FONT).Height + _settings.LINESPACING;
            }
        }

        public void Adjust()
        { 
            // Keeps track of widest text in diagram node
            SizeF maxTextWidth = _graphics.MeasureString(Name, IsAbstract ? _settings.ABSTRACT_FONT : _settings.NAME_FONT);

            // Keeps track of total calculated height of the diagram cell
            float totalHeight = _settings.CLASSNAME_HEIGHT + _settings.LINESPACING;

            foreach(Property property in Properties)
            {
                SizeF current = _graphics.MeasureString(property.ToString(), _settings.FONT);
                totalHeight += current.Height + _settings.LINESPACING;
                if (maxTextWidth.Width < current.Width)
                    maxTextWidth = current;
            }

            foreach (Method method in Methods)
            {
                SizeF current = _graphics.MeasureString(method.ToString(), _settings.FONT);
                totalHeight += current.Height + _settings.LINESPACING;
                if (maxTextWidth.Width < current.Width)
                    maxTextWidth = current;
            }


            MinWidth = (int)maxTextWidth.Width + _settings.NAME_MARGIN * 2;
            MinHeight = (int)totalHeight + _settings.LINESPACING;

            // If text overflows through current width extend width to maximum text size plus margin
            if (MinWidth >= Background.Width - _settings.NAME_MARGIN * 2)
                Background = new Rectangle(Background.X, Background.Y, MinWidth, Background.Height);

            // If text overflows through current height extend height to total calculated height
            if(MinHeight >= Background.Height)
                Background = new Rectangle(Background.X, Background.Y, Background.Width, MinHeight);
        }

        public void Resize(int width, int height, ResizeMode mode, bool xStart, bool yStart, PictureBox p)
        {
            int xSize;
            int ySize;

            if (!xStart)
                xSize = width < MinWidth ? MinWidth : width;
            else
                xSize = Background.Width - width;
            if (!yStart)
                ySize = height < MinHeight ? MinHeight : height;
            else
                ySize = Background.Height - height;

            int x = xStart && xSize >= MinWidth ? Background.X + width : Background.X;
            int y = yStart && ySize >= MinHeight ? Background.Y + height : Background.Y;

            if (x < 0)
            {
                x = 0;
                xSize = 0;
            }
            if (y < 0)
            {
                y = 0;
                ySize = 0;
            }

            int finalWidth = mode != ResizeMode.Vertical && xSize >= MinWidth ? xSize : Background.Width;
            int finalHeight = mode != ResizeMode.Horizontal && ySize >= MinHeight ? ySize : Background.Height;

            if(x + finalWidth > p.Width)
            {
                x = Background.X;
                finalWidth = p.Width - x;
            }
            if (y + finalHeight > p.Height)
            {
                y = Background.Y;
                finalHeight = p.Height - y;
            }

            Background = new Rectangle(x, y, finalWidth, finalHeight);
        }
    }
}
