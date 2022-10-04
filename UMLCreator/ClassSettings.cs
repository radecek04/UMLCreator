﻿using System;
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
        #endregion
        #region fonts
        public readonly Font FONT = new Font("Cascadia Code", 8);
        public readonly Font NAME_FONT = new Font("Cascadia Code", 12);
        public readonly Font ABSTRACT_FONT = new Font("Cascadia Code", 12, FontStyle.Italic);
        #endregion
        #region numeric data
        public readonly float CLASSNAME_HEIGHT = 28;
        public readonly int NAME_MARGIN = 5;
        public readonly int LINESPACING = 2;
        #endregion

        public readonly Rectangle BACKGROUND = new Rectangle(0, 0, 100, 150);

        public static readonly ClassSettings Instance = new ClassSettings();
        private ClassSettings()
        {
            BORDER_PEN.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            DIVIDER_PEN.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
        }
    }

    public enum AccessModifier
    {
        Private = '-',
        Public = '+',
        Protected = '#'
    }
}