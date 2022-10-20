using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLCreator
{
    public class Class
    {
        public string Name { get; set; }
        public List<Property> Properties { get; set; }
        public List<Method> Methods { get; set; }
        public bool IsAbstract { get; set; }
        public Rectangle Background { get; set; }

        public int MinWidth { get; private set; }
        public int MinHeight { get; private set; }

        private List<ClassAnchor> _anchors;
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
            this._anchors = new List<ClassAnchor>();

            _anchors.Add(new ClassAnchor(this, Background.X + Background.Width / 2,
            Background.Y, 0, -1));
            _anchors.Add(new ClassAnchor(this, Background.X,
            Background.Y + Background.Height / 2, -1, 0));
            _anchors.Add(new ClassAnchor(this, Background.X + Background.Width,
            Background.Y + Background.Height / 2, 1, 0));
            _anchors.Add(new ClassAnchor(this, Background.X + Background.Width / 2,
            Background.Y + Background.Height, 0, 1));

            Adjust();
        }

        public void Draw(Graphics g, bool selected)
        {
            // Draw background for class diagram node
            g.FillRectangle(selected ? _settings.SELECTED_CLASS_BRUSH : _settings.CLASS_BRUSH, Background); ;
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
            if (Properties.Count > 0 && Methods.Count > 0)
            {
                g.DrawLine(_settings.DIVIDER_PEN, Background.X, lineY, Background.X + Background.Width - 1, lineY);
                lineY += _settings.DIVIDER_PEN.Width;
            }


            // Draw in all methods of the class
            foreach (Method method in Methods)
            {
                g.DrawString(method.ToString(), _settings.FONT, Brushes.Black, _settings.NAME_MARGIN + Background.X, lineY + _settings.LINESPACING);
                lineY += (int)g.MeasureString(method.ToString(), _settings.FONT).Height + _settings.LINESPACING;
            }

            _anchors[0].X = Background.X + Background.Width / 2;
            _anchors[0].Y = Background.Y;
            _anchors[1].X = Background.X;
            _anchors[1].Y = Background.Y + Background.Height / 2;
            _anchors[2].X = Background.X + Background.Width;
            _anchors[2].Y = Background.Y + Background.Height / 2;
            _anchors[3].X = Background.X + Background.Width / 2;
            _anchors[3].Y = Background.Y + Background.Height;
        }

        public void Adjust()
        {
            // Keeps track of widest text in diagram node
            SizeF maxTextWidth = _graphics.MeasureString(Name, IsAbstract ? _settings.ABSTRACT_FONT : _settings.NAME_FONT);

            // Keeps track of total calculated height of the diagram cell
            float totalHeight = _settings.CLASSNAME_HEIGHT + _settings.LINESPACING;

            foreach (Property property in Properties)
            {
                SizeF current = _graphics.MeasureString(property.ToString(), _settings.FONT);
                totalHeight += (int)current.Height + _settings.LINESPACING;
                if (maxTextWidth.Width < current.Width)
                    maxTextWidth = current;
            }

            foreach (Method method in Methods)
            {
                SizeF current = _graphics.MeasureString(method.ToString(), _settings.FONT);
                totalHeight += (int)current.Height + _settings.LINESPACING;
                if (maxTextWidth.Width < current.Width)
                    maxTextWidth = current;
            }


            MinWidth = (int)maxTextWidth.Width + _settings.NAME_MARGIN * 2;
            MinHeight = (int)totalHeight + _settings.LINESPACING * 2;

            // If text overflows through current width extend width to maximum text size plus margin
            if (MinWidth >= Background.Width - _settings.NAME_MARGIN * 2)
                Background = new Rectangle(Background.X, Background.Y, MinWidth, Background.Height);

            // If text overflows through current height extend height to total calculated height
            if (MinHeight >= Background.Height)
                Background = new Rectangle(Background.X, Background.Y, Background.Width, MinHeight);

            _anchors[0].X = Background.X + Background.Width / 2;
            _anchors[0].Y = Background.Y;
            _anchors[1].X = Background.X;
            _anchors[1].Y = Background.Y + Background.Height / 2;
            _anchors[2].X = Background.X + Background.Width;
            _anchors[2].Y = Background.Y + Background.Height / 2;
            _anchors[3].X = Background.X + Background.Width / 2;
            _anchors[3].Y = Background.Y + Background.Height;
        }

        public void Resize(int width, int height, ResizeMode mode, bool xStart, bool yStart, PictureBox p)
        {
            int xSize;
            int ySize;

            // Calculate coeficient depending on where user resized
            if (!xStart)
                xSize = width < MinWidth ? MinWidth : width;
            else
                xSize = Background.Width - width;
            if (!yStart)
                ySize = height < MinHeight ? MinHeight : height;
            else
                ySize = Background.Height - height;


            // Calculate x and y cords
            int x = xStart && xSize >= MinWidth ? Background.X + width : Background.X;
            int y = yStart && ySize >= MinHeight ? Background.Y + height : Background.Y;

            // Adjust to picturebox
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

            // Calculate width and height
            int finalWidth = mode != ResizeMode.Vertical && xSize >= MinWidth ? xSize : Background.Width;
            int finalHeight = mode != ResizeMode.Horizontal && ySize >= MinHeight ? ySize : Background.Height;


            // Adjust to picturebox
            if (x + finalWidth > p.Width)
            {
                x = Background.X;
                finalWidth = p.Width - x;
            }
            if (y + finalHeight > p.Height)
            {
                y = Background.Y;
                finalHeight = p.Height - y;
            }

            // set cords and size to background
            Background = new Rectangle(x, y, finalWidth, finalHeight);
        }

        public void DrawAnchors(Graphics g)
        {
            foreach (var anchor in _anchors)
            {
                anchor.Draw(g);
            }
        }

        public ClassAnchor? CheckAnchorCollison(int x, int y)
        {
            ClassAnchor? output = null;
            foreach (var anchor in _anchors)
            {
                if (anchor.CheckCollision(x, y))
                    output = anchor;
            }
            return output;
        }
    }
}
