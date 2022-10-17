using System.Diagnostics;
using UMLCreator.Relationships;

namespace UMLCreator
{
    public partial class Main : Form
    {
        private LayerManager _layers = new LayerManager();
        private List<Relationships.Relationship> _relationships = new List<Relationships.Relationship>();
        private Class _selected = null;
        private Relationships.Relationship _selectedRelationship = null;
        private ClassAnchor _selectedAnchor = null;
        private ClassAnchor _connectedAnchor = null;
        private bool _drag = false;
        private bool _resize = false;
        private int _xOffset = 0;
        private int _yOffset = 0;
        private bool _xStart = false;
        private bool _yStart = false;
        private ResizeMode _resizeMode;
        private ClassSettings _settings = ClassSettings.Instance;
        private Point _mousePos;

        public Main()
        {
            InitializeComponent();

            // Tester Classes
            Class c = new Class("Class Test", false, pictureBox1.CreateGraphics());
            c.Properties.Add(new Property(AccessModifier.Public, "int", "Property"));
            c.Properties.Add(new Property(AccessModifier.Public, "int", "Property"));
            c.Properties.Add(new Property(AccessModifier.Public, "int", "Property"));
            c.Methods.Add(new Method(AccessModifier.Public, "int", "Property"));
            c.Methods.Add(new Method(AccessModifier.Public, "int", "Property"));
            c.Methods.Add(new Method(AccessModifier.Public, "int", "Property"));
            c.Adjust();
            _layers.Add(c);
            c = new Class("Class Test 2", false, pictureBox1.CreateGraphics());
            c.Properties.Add(new Property(AccessModifier.Public, "int", "Property"));
            c.Properties.Add(new Property(AccessModifier.Public, "int", "Property"));
            c.Properties.Add(new Property(AccessModifier.Public, "int", "Property"));
            c.Methods.Add(new Method(AccessModifier.Public, "int", "Property"));
            c.Methods.Add(new Method(AccessModifier.Public, "int", "Property"));
            c.Methods.Add(new Method(AccessModifier.Public, "int", "Property"));
            c.Adjust();
            _layers.Add(c);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(_selected != null)
            {
                ClassAnchor selectedAnchor = _selected.CheckAnchorCollison(e.X, e.Y);
                if(selectedAnchor != null)
                {
                    _selectedAnchor = selectedAnchor;
                    _mousePos = new Point(e.X, e.Y);
                    this.pictureBox1.Refresh();
                    return;
                }
            }

            _selected = null;
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

                // Set drag to true if resize false and otherwise
                _drag = !_resize;

                _selected = c;

                // Calculate offset of mouse position from anchor of diagram node
                _xOffset = e.X - c.Background.X;
                _yOffset = e.Y - c.Background.Y;
            }
            if(_selected != null)
            {
                if (_selectedRelationship != null)
                {
                    _selectedRelationship.LinePen = _selectedRelationship.LinePen == _settings.LINE_PEN_SELECTED ? _settings.LINE_PEN : _settings.LINE_PEN_DASHED;
                    _selectedRelationship = null;
                }

                // If collision is triggered move diagram node to top layer and refresh
                _layers.MoveUp(_selected);

                if (e.Clicks == 2)
                {
                    // Start new window with edit
                    EditClass();
                    _drag = false;
                }
            }
            else
            {
                if(_selectedRelationship != null)
                {
                    _selectedRelationship.LinePen = _selectedRelationship.LinePen == _settings.LINE_PEN_SELECTED ? _settings.LINE_PEN : _settings.LINE_PEN_DASHED;
                    _selectedRelationship = null;
                }
                int index = 0;
                int editIndex = -1;
                foreach (var line in _relationships)
                {
                    if (line.CheckCollision(e.X, e.Y))
                    {
                        _selectedRelationship = line;
                        _selectedRelationship.LinePen = _selectedRelationship.LinePen == _settings.LINE_PEN ? _settings.LINE_PEN_SELECTED : _settings.LINE_PEN_DASHED_SELECTED;

                        if(e.Clicks == 2)
                        {
                            Relationship frm = new Relationship(_selectedRelationship.StartAnchor);
                            if(frm.ShowDialog() == DialogResult.OK)
                            {
                                frm.relationship.EndAnchor = _selectedRelationship.EndAnchor;
                                _selectedRelationship = frm.relationship;
                                editIndex = index; 
                            }     
                        }

                        break;
                    }
                    index++;
                }
                if(editIndex >= 0)
                {
                    _relationships[editIndex] = _selectedRelationship;
                    _selectedRelationship = null;
                }
            }

            this.pictureBox1.Refresh();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // Reset all variables
            _drag = false;
            _resize = false;
            _resizeMode = ResizeMode.None;
            _xStart = false;
            _yStart = false;

            if(_connectedAnchor != null)
            {
                Relationship frm = new Relationship(_selectedAnchor);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Relationships.Relationship relationship = frm.relationship;
                    relationship.EndAnchor = _connectedAnchor;
                    _relationships.Add(relationship);
                }
            }
            _selectedAnchor = null;
            _connectedAnchor = null;

            this.Refresh();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(_selectedAnchor != null)
            {
                _mousePos = new Point(e.X, e.Y);

                _connectedAnchor = null;

                foreach(Class c in _layers)
                {
                    ClassAnchor anchor = c.CheckPointCollison(_mousePos.X, _mousePos.Y);
                    if(anchor != null)
                    {
                        _mousePos = new Point(anchor.X, anchor.Y);
                        _connectedAnchor = anchor;
                    }
                }

                this.pictureBox1.Refresh();
                return;
            }

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

            if (_resize)
                _selected.Resize(e.X - _selected.Background.X, e.Y - _selected.Background.Y, _resizeMode, _xStart, _yStart, pictureBox1);
            else if (_drag)
                _selected.Background = new Rectangle(e.X - _xOffset, e.Y - _yOffset, _selected.Background.Width, _selected.Background.Height);
            else
                return;

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
            foreach(var line in _relationships)
            {
                line.Draw(g);
            }
            foreach(Class c in _layers)
            {
                c.Draw(g, c == _selected);
                if (_selectedAnchor != null && c != _selected)
                    c.DrawPoints(g);
            }
            if (_selected != null && _selectedAnchor == null)
                _selected.DrawAnchors(g);
            if(_selectedAnchor != null)
            {
                g.DrawLine(_settings.BORDER_PEN, new Point(_selectedAnchor.X, _selectedAnchor.Y), _mousePos);
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            // Create empty class
            Class c = new Class("", false, pictureBox1.CreateGraphics());
            Class ef = new Class(c, _layers);
            if(ef.ShowDialog() == DialogResult.OK)
            {
                // Add to layer manager
                _layers.Add(c);
                c.Adjust();
                this.pictureBox1.Refresh();
            }
        }


        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (_selected != null)
            {
                // Remove class from view
                _layers.Remove(_selected);
                _relationships.RemoveAll(x => x.StartAnchor.Parent == _selected || x.EndAnchor.Parent == _selected);
                _selected = null;
            }
            else if (_selectedRelationship != null)
            {
                _relationships.Remove(_selectedRelationship);
                _selectedRelationship = null;
            }
            this.pictureBox1.Refresh();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            // Set cursor to default when leaving the picturebox
            Cursor = Cursors.Default;
        }

        private void EditClass()
        {
            // Show new dialog with edit
            Class ef = new Class(_selected, _layers);
            if (ef.ShowDialog() == DialogResult.OK)
            {
                _selected.Adjust();
            }
        }
    }
}