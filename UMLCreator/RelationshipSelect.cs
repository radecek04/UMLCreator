using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLCreator.Relationships;

namespace UMLCreator
{
    public partial class Relationship : Form
    {
        public Relationships.Relationship relationship;
        private Class _start;
        public Relationship(Class c)
        {
            InitializeComponent();

            _start = c;

            comboBox1.Items.Add("Association");
            comboBox1.Items.Add("Inheritance");
            comboBox1.Items.Add("Implementation");
            comboBox1.Items.Add("Dependency");
            comboBox1.Items.Add("Aggregation");
            comboBox1.Items.Add("Composition");

            comboBox1.SelectedIndex = 0;
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem as string)
            {
                case "Association":
                    relationship = new AssociationRelationship(_start);
                    break;
                case "Inheritance":
                    relationship = new InheritanceRelationship(_start);
                    break;
                case "Implementation":
                    relationship = new ImplementationRelationship(_start);
                    break;
                case "Dependency":
                    relationship = new DependencyRelationship(_start);
                    break;
                case "Aggregation":
                    relationship = new AggregationRelationship(_start);
                    break;
                case "Composition":
                    relationship = new CompositionRelationship(_start);
                    break;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
