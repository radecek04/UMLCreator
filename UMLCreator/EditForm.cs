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
        private Class _current;
        private BindingList<Property> _properties;
        private BindingList<Method> _methods;
        public EditForm(Class current)
        {
            InitializeComponent();

            _current = current;

            // Set all fields to class properties
            this.textBox_Class_Name.Text = _current.Name;
            this.checkBox_Abstract.Checked = _current.IsAbstract;

            this.comboBox_Access.DataSource = Enum.GetValues(typeof(AccessModifier));
            this._properties = new BindingList<Property>(_current.Properties.ToList());
            this._methods = new BindingList<Method>(_current.Methods.ToList());

            this.listBox_Properties.DataSource = _properties;
            this.listBox_Methods.DataSource = _methods;
        }

        private void listBox_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox_Properties.SelectedItem == null)
                return;

            //if (!this.ValidateChildren())
            //{
            //    this.listBox_Properties.SelectedIndex = _lastIndex;
            //    return;
            //}

            this.listBox_Methods.SelectedIndex = -1;

            this._IsProperty = true;

            // Load selected property
            this.errorProvider.Clear();
            Property current = this.listBox_Properties.SelectedItem as Property;
            this.textBox_Name.Text = current.Name;
            this.textBox_Type.Text = current.Type;
            this.comboBox_Access.SelectedItem = current.Access;

            _lastIndex = this.listBox_Properties.SelectedIndex;
        }

        private void listBox_Methods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox_Methods.SelectedItem == null)
                return;

            // Zkus count == 0

            //if (!this.ValidateChildren())
            //{
            //    this.listBox_Methods.SelectedIndex = _lastIndex;
            //    return;
            //}

            this.listBox_Properties.SelectedIndex = -1;

            this._IsProperty = false;

            // Load selected method
            this.errorProvider.Clear();
            Method current = this.listBox_Methods.SelectedItem as Method;
            this.textBox_Name.Text = current.Name;
            this.textBox_Type.Text = current.Type;
            this.comboBox_Access.SelectedItem = current.Access;
            _lastIndex = this.listBox_Methods.SelectedIndex;
        }

        private void btn_Add_Property_Click(object sender, EventArgs e)
        {
            // add new dummy property and select it
            _properties.Add(new Property(AccessModifier.Public, "int", "Property"));
            listBox_Properties.ClearSelected();
            listBox_Properties.SelectedIndex = listBox_Properties.Items.Count - 1;
        }

        private void btn_Remove_Property_Click(object sender, EventArgs e)
        {
            if (listBox_Properties.SelectedItem == null)
                return;

            _properties.Remove(listBox_Properties.SelectedItem as Property);
        }

        private void btn_Add_Method_Click(object sender, EventArgs e)
        {
            // add new dummy method and select it
            _methods.Add(new Method(AccessModifier.Public, "void", "Method"));
            listBox_Methods.ClearSelected();
            listBox_Methods.SelectedIndex = listBox_Methods.Items.Count - 1;
        }

        private void btn_Remove_Method_Click(object sender, EventArgs e)
        {
            if (listBox_Methods.SelectedItem == null)
                return;

            _methods.Remove(listBox_Methods.SelectedItem as Method);
        }

        private void textBox_Name_TextChanged(object sender, EventArgs e)
        {
            if(_IsProperty)
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

        private void textBox_Type_TextChanged(object sender, EventArgs e)
        {
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
            // Save all to class, return dialog result OK
            _current.Name = this.textBox_Class_Name.Text;
            _current.IsAbstract = this.checkBox_Abstract.Checked;

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

        //private void textBox_Name_Validating(object sender, CancelEventArgs e)
        //{
        //    this.errorProvider.SetError(this.textBox_Name, null);
        //    if (string.IsNullOrWhiteSpace(this.textBox_Name.Text))
        //    {
        //        e.Cancel = true;
        //        this.errorProvider.SetError(this.textBox_Name, "Name is mandatory");
        //    }
        //    else if (!Regex.IsMatch(this.textBox_Name.Text, @"^(?!\d)[\w$]+$"))
        //    {
        //        e.Cancel = true;
        //        this.errorProvider.SetError(this.textBox_Name, "Name is in wrong format");
        //    }
        //    else if (_IsProperty ? this.textBox_Name.Text != (this.listBox_Properties.SelectedItem as Property).Name : this.textBox_Name.Text != (this.listBox_Methods.SelectedItem as Method).Name)
        //    {
        //        if (_IsProperty ? _properties.Any(x => x.Name == this.textBox_Name.Text) : _methods.Any(x => x.Name == this.textBox_Name.Text))
        //        {
        //            e.Cancel = true;
        //            this.errorProvider.SetError(this.textBox_Name, "Name must be unique");
        //        }
        //    }
        //}
    }
}
