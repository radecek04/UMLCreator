using System.Diagnostics;

namespace UMLCreator
{
    public partial class Main : Form
    {
        private LayerManager _layers = new LayerManager();
        private Class _selected = null;
        private bool _resize = false;
        private int _xOffset = 0;
        private int _yOffset = 0;
        private bool _xStart = false;
        private bool _yStart = false;
        private ResizeMode _resizeMode;
        private ClassSettings _settings = ClassSettings.Instance;
        public Main()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {        
            foreach(Class c in _layers)
            {
                // Check collision
                if (e.X < c.Background.X || e.X >= c.Background.X + c.Background.Width)
                    continue;
                else if (e.Y < c.Background.Y || e.Y >= c.Background.Y + c.Background.Height)
                    continue;

                _resize = false;

                // Check collision with border and assing resize mode
                // If horizontal is added to vertical it creates diagonal
                if (e.X >= c.Background.X + c.Background.Width - _settings.RESIZE_BORDER)
                {
                    _resize = true;
                    _resizeMode += (int)ResizeMode.Horizontal;
                }
                if (e.X < c.Background.X + _settings.RESIZE_BORDER)
                {
                    _resize = true;
                    _resizeMode += (int)ResizeMode.Horizontal;
                    _xStart = true;
                }
                if (e.Y >= c.Background.Y + c.Background.Height - _settings.RESIZE_BORDER)
                {
                    _resize = true;
                    _resizeMode += (int)ResizeMode.Vertical;
                }
                if (e.Y < c.Background.Y + _settings.RESIZE_BORDER)
                {
                    _resize = true;
                    _resizeMode += (int)ResizeMode.Vertical;
                    _yStart = true;
                }
                _selected = c;

                // Calculate offset of mouse position from anchor of diagram node
                _xOffset = e.X - c.Background.X;
                _yOffset = e.Y - c.Background.Y;
            }
            if(_selected != null)
            {
                // If collision is triggered move diagram node to top layer and refresh
                _layers.MoveUp(_selected);
                this.pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _selected = null;
            _resize = false;
            _resizeMode = ResizeMode.None;
            _xStart = false;
            _yStart = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor tmp = Cursors.Default;
            if (!_resize)
            {
                foreach (Class c in _layers)
                {
                    // Check collision
                    if (e.X < c.Background.X || e.X >= c.Background.X + c.Background.Width)
                        continue;
                    else if (e.Y < c.Background.Y || e.Y >= c.Background.Y + c.Background.Height)
                        continue;

                    tmp = Cursors.Default;

                    // Check collision on border and set cursor
                    if (e.X >= c.Background.X + c.Background.Width - _settings.RESIZE_BORDER)
                    {
                        tmp = Cursors.SizeWE;
                        if (e.Y < c.Background.Y + _settings.RESIZE_BORDER)
                            tmp = Cursors.SizeNESW;
                        else if (e.Y >= c.Background.Y + c.Background.Height - _settings.RESIZE_BORDER)
                            tmp = Cursors.SizeNWSE;
                    }
                    else if (e.X < c.Background.X + _settings.RESIZE_BORDER)
                    {
                        tmp = Cursors.SizeWE;
                        if (e.Y >= c.Background.Y + c.Background.Height - _settings.RESIZE_BORDER)
                            tmp = Cursors.SizeNESW;
                        if (e.Y < c.Background.Y + _settings.RESIZE_BORDER)
                            tmp = Cursors.SizeNWSE;
                        
                    }
                    else if (e.Y >= c.Background.Y + c.Background.Height - _settings.RESIZE_BORDER)
                    {
                        tmp = Cursors.SizeNS;
                    }
                    else if (e.Y < c.Background.Y + _settings.RESIZE_BORDER)
                    {
                        tmp = Cursors.SizeNS;
                    }
                }
                Cursor = tmp;
            }

            if (_selected == null)
                return;

            if(_resize)
                _selected.Resize(e.X - _selected.Background.X, e.Y - _selected.Background.Y, _resizeMode, _xStart, _yStart, pictureBox1);
            else
                _selected.Background = new Rectangle(e.X - _xOffset, e.Y - _yOffset, _selected.Background.Width, _selected.Background.Height);
            
            // Keep diagram node inside editor box
            if (_selected.Background.X < 0)
                _selected.Background = new Rectangle(0, _selected.Background.Y, _selected.Background.Width, _selected.Background.Height);
            else if (_selected.Background.X + _selected.Background.Width >= pictureBox1.Width)
                _selected.Background = new Rectangle(pictureBox1.Width - _selected.Background.Width, _selected.Background.Y, _selected.Background.Width, _selected.Background.Height);
            if (_selected.Background.Y < 0)
                _selected.Background = new Rectangle(_selected.Background.X, 0, _selected.Background.Width, _selected.Background.Height);
            else if (_selected.Background.Y + _selected.Background.Height >= pictureBox1.Height)
                _selected.Background = new Rectangle(_selected.Background.X, pictureBox1.Height - _selected.Background.Height, _selected.Background.Width, _selected.Background.Height);
            
            this.pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach(Class c in _layers)
            {
                c.Draw(g);
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            // TODO editor
            Class c = new Class("Stra�n� dlouh� text", false, pictureBox1.CreateGraphics());
            c.Properties.Add(new Property(AccessModifier.Public, "int", "COunt"));
            c.Properties.Add(new Property(AccessModifier.Private, "int", "Check"));
            c.Properties.Add(new Property(AccessModifier.Protected, "int", "This"));
            c.Methods.Add(new Method(AccessModifier.Public, "int", "Count"));
            c.Methods.Add(new Method(AccessModifier.Private, "int", "Check"));
            c.Methods.Add(new Method(AccessModifier.Protected, "int", "This"));
            _layers.Add(c);
            c.Adjust();
            this.pictureBox1.Refresh();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
    }
}