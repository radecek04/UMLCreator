using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        public Relationship(Class c, Relationships.Relationship relation = null)
        {
            InitializeComponent();

            comboBox1.Items.Add("Association");
            comboBox1.Items.Add("Inheritance");
            comboBox1.Items.Add("Implementation");
            comboBox1.Items.Add("Dependency");
            comboBox1.Items.Add("Aggregation");
            comboBox1.Items.Add("Composition");
            comboBox1.SelectedIndex = 0;

            _start = c;

            if (relation == null)
                return;

            relationship = relation;

            textBox_StartCardinal.Text = relationship.StartCardinal;
            textBox_EndCardinal.Text = relationship.EndCardinal;
            comboBox1.SelectedItem = relation.ToString().Split('.').Last().Split("R")[0];

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

            this.relationship.StartCardinal = textBox_StartCardinal.Text;
            this.relationship.EndCardinal = textBox_EndCardinal.Text;

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
