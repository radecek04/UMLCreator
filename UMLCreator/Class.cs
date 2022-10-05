using System;
using System.Collections.Generic;
using System.IO.Pipes;
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

            // If text overflows through current width extend width to maximum text size plus margin
            if (maxTextWidth.Width >= Background.Width - _settings.NAME_MARGIN * 2)
                Background = new Rectangle(Background.X, Background.Y, (int)maxTextWidth.Width + _settings.NAME_MARGIN * 2, Background.Height);

            // If text overflows through current height extend height to total calculated height
            if(totalHeight >= Background.Height)
                Background = new Rectangle(Background.X, Background.Y, Background.Width, (int)totalHeight - _settings.LINESPACING);
        }
    }
}
