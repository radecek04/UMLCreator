using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLCreator.Export;

namespace UMLCreator
{
    public partial class ExportSelect : Form
    {
        private LayerManager _layers;
        private List<Relationships.Relationship> _relation;
        private PictureBox _pb;

        private Dictionary<string, IExport> _exports = new Dictionary<string, IExport>();
        public ExportSelect(LayerManager layers, List<Relationships.Relationship> relations, PictureBox pb)
        {
            InitializeComponent();

            _layers = layers;
            _relation = relations;
            _pb = pb;

            _exports.Add(".JSON", new JsonService(layers, relations));
            _exports.Add(".PNG", new PngExport(layers, relations, pb));
            _exports.Add(".CS", new CodeExport(layers, relations, ""));

            comboBox1.Items.Add(".JSON");
            comboBox1.Items.Add(".PNG");
            comboBox1.Items.Add(".CS");

            comboBox1.SelectedIndex = 0;
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            string selected = comboBox1.SelectedItem as string;

            if (selected == ".CS")
                _exports[selected] = new CodeExport(_layers, _relation, textBox_Namespace.Text);

            if(selected == ".CS" && !Directory.Exists(textBox_Path.Text))
            {
                MessageBox.Show("Path invalid!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (selected != ".CS" && !File.Exists(textBox_Path.Text))
            {
                MessageBox.Show("Path invalid!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _exports[selected].Export(textBox_Path.Text);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox_Path.Text = _exports[comboBox1.SelectedItem as string].OpenPath();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_Path.Text = "";
            if (comboBox1.SelectedItem as string != ".CS")
                textBox_Namespace.Enabled = false;
            else
                textBox_Namespace.Enabled = true;
        }
    }
}
