using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLCreator
{
    public class ClassSettings
    {
        #region pens
        public readonly Pen BORDER_PEN = new Pen(Brushes.Black, 2);
        public readonly Pen DIVIDER_PEN = new Pen(Brushes.Black, 1);
        [NonSerialized]
        public readonly Pen LINE_PEN = new Pen(Brushes.Black, 2);
        [NonSerialized]
        public readonly Pen LINE_PEN_DASHED = new Pen(Brushes.Black, 2);
        [NonSerialized]
        public readonly Pen LINE_PEN_SELECTED = new Pen(Brushes.Red, 2);
        [NonSerialized]
        public readonly Pen LINE_PEN_DASHED_SELECTED = new Pen(Brushes.Red, 2);
        #endregion
        #region brushes
        public readonly Brush CLASS_BRUSH = Brushes.DeepSkyBlue;
        public readonly Brush SELECTED_CLASS_BRUSH = Brushes.SeaGreen;
        #endregion
        #region fonts
        public readonly Font FONT = new Font("Cascadia Code", 8);
        public readonly Font NAME_FONT = new Font("Cascadia Code", 12);
        public readonly Font ABSTRACT_FONT = new Font("Cascadia Code", 12, FontStyle.Italic);
        #endregion
        #region numeric data
        public readonly float CLASSNAME_HEIGHT = 28;
        public readonly int RESIZE_BORDER = 7;
        public readonly int NAME_MARGIN = 5;
        public readonly int LINESPACING = 2;
        public readonly int ANCHOR_WIDTH = 10;
        public readonly int ANCHOR_HEIGHT = 15;
        public readonly int ANCHOR_ARROW_HEAD = 7;
        public readonly int LINE_ENDING_LENGTH = 10;
        public readonly int LINE_ENDING_WIDTH = 20;
        public readonly int LINE_MARGIN = 5;
        #endregion
        public Graphics GRAPHICS = new PictureBox().CreateGraphics();

        public readonly Rectangle BACKGROUND = new Rectangle(0, 0, 1, 1);

        public static readonly ClassSettings Instance = new ClassSettings();
        private ClassSettings()
        {
            BORDER_PEN.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            DIVIDER_PEN.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            LINE_PEN.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            LINE_PEN_SELECTED.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            LINE_PEN_DASHED.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            LINE_PEN_DASHED_SELECTED.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        }
    }

    public enum AccessModifier
    {
        Private = '-',
        Public = '+',
        Protected = '#'
    }

    public enum ResizeMode
    {
        None = 0,
        Horizontal = 1,
        Vertical = 2,
        Diagonal = 3
    }
}
