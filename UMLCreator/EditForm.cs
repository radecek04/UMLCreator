using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UMLCreator
{
    public partial class EditForm : Form
    {
        private int _lastIndex = -1;
        private bool _IsProperty = false;
        private bool _changed = false;
        private bool _assigning = false;
        private Class _current;
        private BindingList<Property> _properties;
        private BindingList<Method> _methods;
        private LayerManager _layers;
        public EditForm(Class current, LayerManager layers)
        {
            InitializeComponent();

            _layers = layers;
            _current = current;

            // Set all fields to class properties
            this.textBox_Class_Name.Text = _current.Name;

            this.comboBox_Access.DataSource = Enum.GetValues(typeof(AccessModifier));
            this.comboBox_Type.DataSource = Enum.GetValues(typeof(ObjectType));
            this.comboBox_Type.SelectedItem = _current.Type;
            this._properties = new BindingList<Property>(_current.Properties.ToList());
            this._methods = new BindingList<Method>(_current.Methods.ToList());

            this.listBox_Properties.DataSource = _properties;
            this.listBox_Methods.DataSource = _methods;

            HideArguments();

            this.textBox_Argument_Name.CausesValidation = false;
            this.textBox_Argument_Type.CausesValidation = false;

            if (current.Name == "")
            {
                this.btn_Save.Text = "Create";
                DisableEditFields();
            }
            else
            {
                EnableEditFields();
            }
        }

        private void listBox_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox_Properties.SelectedItem == null)
            {
                // Disable edit fields when nothing is selected 
                if(_properties.Count == 0)
                    DisableEditFields();
                return;
            }

            EnableEditFields();

            this.errorProvider.Clear();

            bool validated = true;

            if (/*_properties.Count > 1 && */!_changed)
            {
                this.textBox_Class_Name.CausesValidation = false;
                validated = this.ValidateChildren();
                this.textBox_Class_Name.CausesValidation = true;

                if (!validated)
                {
                    if (listBox_Methods.SelectedIndex == -1)
                        this.listBox_Properties.SelectedIndex = _lastIndex;
                    else
                    {
                        this.listBox_Properties.SelectedIndex = -1;
                        return;
                    }
                }
            }

            HideArguments();

            _changed = false;

            if(validated)
                this.listBox_Methods.SelectedIndex = -1;

            this._IsProperty = true;

            // Load selected property
            Property current = this.listBox_Properties.SelectedItem as Property;
            _assigning = true;

            this.textBox_Name.Text = current.Name;
            this.textBox_Type.Text = current.Type;
            this.comboBox_Access.SelectedItem = current.Access;

            _assigning = false;


            _lastIndex = this.listBox_Properties.SelectedIndex;
        }

        private void listBox_Methods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox_Methods.SelectedItem == null)
            {
                // Disable edit fields when nothing is selected 
                if (_methods.Count == 0)
                    DisableEditFields();
                return;
            }

            EnableEditFields();

            this.errorProvider.Clear();

            bool validated = true;

            if (_methods.Count > 1 && !_changed)
            {
                this.textBox_Class_Name.CausesValidation = false;
                validated = this.ValidateChildren();
                this.textBox_Class_Name.CausesValidation = true;

                if (!validated)
                {
                    if (listBox_Properties.SelectedIndex == -1)
                        this.listBox_Methods.SelectedIndex = _lastIndex;
                    else
                    {
                        this.listBox_Methods.SelectedIndex = -1;
                        return;
                    }
                }
            }

            ShowArguments();

            _changed = false;
            if(validated)
                this.listBox_Properties.SelectedIndex = -1;

            this._IsProperty = false;

            // Load selected method
            Method current = this.listBox_Methods.SelectedItem as Method;
            _assigning = true;

            this.textBox_Name.Text = current.Name;
            this.textBox_Type.Text = current.Type;
            this.comboBox_Access.SelectedItem = current.Access;
            this.listBox_Arguments.DataSource = current.Arguments;

            _assigning = false;

            _lastIndex = this.listBox_Methods.SelectedIndex;
        }

        private void btn_Add_Property_Click(object sender, EventArgs e)
        {
            // validate everything except class name before adding
            this.textBox_Class_Name.CausesValidation = false;
            bool validated = this.ValidateChildren();
            this.textBox_Class_Name.CausesValidation = true;
            if (!validated)
                return;

            _changed = true;

            // add new dummy property and select it
            _properties.Add(new Property(AccessModifier.Public, "int", "Property"));
            listBox_Properties.ClearSelected();
            listBox_Properties.SelectedIndex = listBox_Properties.Items.Count - 1;
        }

        private void btn_Remove_Property_Click(object sender, EventArgs e)
        {
            if (listBox_Properties.SelectedItem == null)
                return;

            _changed = true;

            _properties.Remove(listBox_Properties.SelectedItem as Property);
        }

        private void btn_Add_Method_Click(object sender, EventArgs e)
        {
            // validate everything except class name before adding
            this.textBox_Class_Name.CausesValidation = false;
            bool validated = this.ValidateChildren();
            this.textBox_Class_Name.CausesValidation = true;
            if (!validated)
                return;

            _changed = true;

            // add new dummy method and select it
            _methods.Add(new Method(AccessModifier.Public, "void", "Method"));
            listBox_Methods.ClearSelected();
            listBox_Methods.SelectedIndex = listBox_Methods.Items.Count - 1;
        }

        private void btn_Remove_Method_Click(object sender, EventArgs e)
        {
            if (listBox_Methods.SelectedItem == null)
                return;

            _changed = true;

            _methods.Remove(listBox_Methods.SelectedItem as Method);
        }

        private void textBox_Type_TextChanged(object sender, EventArgs e)
        {
            if (_assigning)
                return;

            _changed = true;

            if (_IsProperty)
            {
                if (listBox_Properties.SelectedItem == null)
                    return;

                // Update property type and refresh list box
                Property p = listBox_Properties.SelectedItem as Property;
                p.Type = textBox_Type.Text;

                listBox_Properties.DataSource = null;
                listBox_Properties.DataSource = _properties;
            }
            else
            {
                if (listBox_Methods.SelectedItem == null)
                    return;

                // Update method type and refresh list box
                Method m = listBox_Methods.SelectedItem as Method;
                m.Type = textBox_Type.Text;

                listBox_Methods.DataSource = null;
                listBox_Methods.DataSource = _methods;
            }
        }

        private void comboBox_Access_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_assigning)
                return;

            _changed = true;

            if (_IsProperty)
            {
                if (listBox_Properties.SelectedItem == null)
                    return;

                // Update property access modifier and refresh list box
                Property p = listBox_Properties.SelectedItem as Property;
                p.Access = Enum.Parse<AccessModifier>(comboBox_Access.SelectedValue.ToString());

                listBox_Properties.DataSource = null;
                listBox_Properties.DataSource = _properties;
            }
            else
            {
                // Update method access modifier and refresh list box
                if (listBox_Methods.SelectedItem == null)
                    return;
                Method m = listBox_Methods.SelectedItem as Method;
                m.Access = Enum.Parse<AccessModifier>(comboBox_Access.SelectedValue.ToString());

                listBox_Methods.DataSource = null;
                listBox_Methods.DataSource = _methods;
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;

            // Save all to class, return dialog result OK
            _current.Name = this.textBox_Class_Name.Text;
            _current.Type = Enum.Parse<ObjectType>(this.comboBox_Type.SelectedValue.ToString());
            _current.Properties = _properties.ToList();
            _current.Methods = _methods.ToList();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            // Close with no saving
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBox_Class_Name_Validating(object sender, CancelEventArgs e)
        {
            this.errorProvider.SetError(this.textBox_Class_Name, null);
            if (string.IsNullOrWhiteSpace(this.textBox_Class_Name.Text))
            {
                e.Cancel = true;
                this.errorProvider.SetError(this.textBox_Class_Name, "Name is mandatory");
            }
            else if (!Regex.IsMatch(this.textBox_Class_Name.Text, @"^(?!\d)[\w$]+$"))
            {
                e.Cancel = true;
                this.errorProvider.SetError(this.textBox_Class_Name, "Name is in wrong format");
            }
            else if (this.textBox_Class_Name.Text != _current.Name && _layers.Contains(this.textBox_Class_Name.Text))
            {
                e.Cancel = true;
                this.errorProvider.SetError(this.textBox_Class_Name, "Name must be unique");
            }
        }

        private void textBox_Name_TextChanged(object sender, EventArgs e)
        {
            if (_assigning)
                return;

            _changed = true;

            if (_IsProperty)
            {
                if (listBox_Properties.SelectedItem == null)
                    return;

                // Update property name and refresh list box
                Property p = listBox_Properties.SelectedItem as Property;
                p.Name = textBox_Name.Text;

                listBox_Properties.DataSource = null;
                listBox_Properties.DataSource = _properties;
            }
            else
            {
                if (listBox_Methods.SelectedItem == null)
                    return;

                // Update method name and refresh list box
                Method m = listBox_Methods.SelectedItem as Method;
                m.Name = textBox_Name.Text;

                listBox_Methods.DataSource = null;
                listBox_Methods.DataSource = _methods;
            }
        }

        private void textBox_Name_Validating(object sender, CancelEventArgs e)
        {
            this.errorProvider.SetError(this.textBox_Name, null);

            if (listBox_Properties.SelectedItem == null && listBox_Methods.SelectedItem == null)
                return;

            if (string.IsNullOrWhiteSpace(this.textBox_Name.Text)) 
            {
                e.Cancel = true;
                this.errorProvider.SetError(this.textBox_Name, "Name is mandatory");
            }
            else if (!Regex.IsMatch(this.textBox_Name.Text, @"^(?!\d)[\w$]+$"))
            {
                e.Cancel = true;
                this.errorProvider.SetError(this.textBox_Name, "Name is in wrong format");
            }
            else if (_IsProperty ? _properties.Count(x => x.Name == textBox_Name.Text) > 1 : _methods.Count(x => x.Name == textBox_Name.Text) > 1)
            {
                e.Cancel = true;
                this.errorProvider.SetError(this.textBox_Name, "Name must be unique");
            }
        }

        private void textBox_Type_Validating(object sender, CancelEventArgs e)
        {
            this.errorProvider.SetError(this.textBox_Type, null);

            if (_properties.Count == 0 && _methods.Count == 0)
                return;

            if (string.IsNullOrWhiteSpace(this.textBox_Type.Text))
            {
                e.Cancel = true;
                this.errorProvider.SetError(this.textBox_Type, "Type is mandatory");
            }
            else if (!Regex.IsMatch(this.textBox_Type.Text, @"[a-zA-Z]+((\[\])+|(\*)+)?"))
            {
                e.Cancel = true;
                this.errorProvider.SetError(this.textBox_Type, "Type is in wrong format");
            }
        }

        private void DisableEditFields()
        {
            this.textBox_Name.Enabled = false;
            this.textBox_Name.Text = "";
            this.textBox_Type.Enabled = false;
            this.textBox_Type.Text = "";
            this.comboBox_Access.Enabled = false;
        }

        private void EnableEditFields()
        {
            this.textBox_Name.Enabled = true;
            this.textBox_Type.Enabled = true;
            this.comboBox_Access.Enabled = true;
        }

        private void HideArguments()
        {
            this.textBox_Argument_Name.Text = "";
            this.textBox_Argument_Type.Text = "";
            this.textBox_Argument_Name.Visible = false;
            this.textBox_Argument_Type.Visible = false;
            this.listBox_Arguments.Visible = false;
            this.btn_Add_Argument.Visible = false;
            this.btn_Remove_Argument.Visible = false;
            this.label_Argument_Name.Visible = false;
            this.label_Argument_Type.Visible = false;
            this.label_Arguments.Visible = false;
        }

        private void ShowArguments()
        {
            this.textBox_Argument_Name.Visible = true;
            this.textBox_Argument_Type.Visible = true;
            this.listBox_Arguments.Visible = true;
            this.btn_Add_Argument.Visible = true;
            this.btn_Remove_Argument.Visible = true;
            this.label_Argument_Name.Visible = true;
            this.label_Argument_Type.Visible = true;
            this.label_Arguments.Visible = true;
        }

        private void btn_Add_Argument_Click(object sender, EventArgs e)
        {
            this.textBox_Class_Name.CausesValidation = false;
            this.textBox_Name.CausesValidation = false;
            this.textBox_Type.CausesValidation = false;
            this.textBox_Argument_Name.CausesValidation = true;
            this.textBox_Argument_Type.CausesValidation = true;

            bool validated = this.ValidateChildren();

            this.textBox_Class_Name.CausesValidation = true;
            this.textBox_Name.CausesValidation = true;
            this.textBox_Type.CausesValidation = true;
            this.textBox_Argument_Name.CausesValidation = false;
            this.textBox_Argument_Type.CausesValidation = false;

            if (!validated)
                return;

            Method selected = listBox_Methods.SelectedItem as Method;
            selected.AddArgument(textBox_Argument_Name.Text, textBox_Argument_Type.Text);

            this.textBox_Argument_Name.Text = "";
            this.textBox_Argument_Type.Text = "";

            listBox_Methods.DataSource = null;
            listBox_Methods.DataSource = _methods;
        }

        private void btn_Remove_Argument_Click(object sender, EventArgs e)
        {
            Method selected = listBox_Methods.SelectedItem as Method;
            int selectedIndex = listBox_Arguments.SelectedIndex;
            selected.RemoveArgument(selectedIndex);

            listBox_Methods.DataSource = null;
            listBox_Methods.DataSource = _methods;
        }

        private void textBox_Argument_Name_Validating(object sender, CancelEventArgs e)
        {
            this.errorProvider.SetError(textBox_Argument_Name, null);

            Method selected = listBox_Methods.SelectedItem as Method;

            if (string.IsNullOrWhiteSpace(this.textBox_Argument_Name.Text))
            {
                e.Cancel = true;
                this.errorProvider.SetError(this.textBox_Argument_Name, "Name is mandatory");
            }
            else if (!Regex.IsMatch(this.textBox_Argument_Name.Text, @"^(?!\d)[\w$]+$"))
            {
                e.Cancel = true;
                this.errorProvider.SetError(this.textBox_Argument_Name, "Name is in wrong format");
            }
            else if (selected.Arguments.Any(x => x.Name == textBox_Argument_Name.Text))
            {
                e.Cancel = true;
                this.errorProvider.SetError(this.textBox_Argument_Name, "Name must be unique");
            }
        }

        private void textBox_Argument_Type_Validating(object sender, CancelEventArgs e)
        {
            this.errorProvider.SetError(textBox_Argument_Type, null);

            if (string.IsNullOrWhiteSpace(this.textBox_Argument_Type.Text))
            {
                e.Cancel = true;
                this.errorProvider.SetError(this.textBox_Argument_Type, "Name is mandatory");
            }
            else if (!Regex.IsMatch(this.textBox_Argument_Type.Text, @"[a-zA-Z]+((\[\])+|(\*)+)?"))
            {
                e.Cancel = true;
                this.errorProvider.SetError(this.textBox_Argument_Type, "Name is in wrong format");
            }
        }
    }
}
