namespace UMLCreator
{
    public partial class Main : Form
    {
        private LayerManager _layers = new LayerManager();
        private Class _selected = null;
        private int _xOffset = 0;
        private int _yOffset = 0;
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
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_selected == null)
                return;

            // Set background position relative to mouse cursor
            _selected.Background = new Rectangle(e.X - _xOffset, e.Y - _yOffset, _selected.Background.Width, _selected.Background.Height);
            
            // Keep diagram node inside editor box
            if (e.X - _xOffset < 0)
                _selected.Background = new Rectangle(0, _selected.Background.Y, _selected.Background.Width, _selected.Background.Height);
            else if (e.X - _xOffset + _selected.Background.Width >= pictureBox1.Width)
                _selected.Background = new Rectangle(pictureBox1.Width - _selected.Background.Width, _selected.Background.Y, _selected.Background.Width, _selected.Background.Height);
            if (e.Y - _yOffset < 0)
                _selected.Background = new Rectangle(_selected.Background.X, 0, _selected.Background.Width, _selected.Background.Height);
            else if (e.Y - _yOffset + _selected.Background.Height >= pictureBox1.Height)
                _selected.Background = new Rectangle(_selected.Background.X, pictureBox1.Height - _selected.Background.Height, _selected.Background.Width, _selected.Background.Height);
            
            this.pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach(Class c in _layers)
            {
                // automatically adjust size of diagram node
                c.Adjust();
                c.Draw(g);
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            // TODO editor
            Class c = new Class("Strašnì dlouhý text", true, pictureBox1.CreateGraphics());
            c.Properties.Add(new Property(AccessModifier.Public, "int", "Count"));
            c.Properties.Add(new Property(AccessModifier.Private, "int", "Check"));
            c.Properties.Add(new Property(AccessModifier.Protected, "int", "This"));
            c.Methods.Add(new Method(AccessModifier.Public, "int", "Count"));
            c.Methods.Add(new Method(AccessModifier.Private, "int", "Check"));
            c.Methods.Add(new Method(AccessModifier.Protected, "int", "This"));
            _layers.Add(c);
            this.pictureBox1.Refresh();
        }
    }
}