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
        private ClassAnchor _startAnchor;
        public Relationship(ClassAnchor c)
        {
            InitializeComponent();

            _startAnchor = c;

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
                    relationship = new AssociationRelationship(_startAnchor);
                    break;
                case "Inheritance":
                    relationship = new InheritanceRelationship(_startAnchor);
                    break;
                case "Implementation":
                    relationship = new ImplementationRelationship(_startAnchor);
                    break;
                case "Dependency":
                    relationship = new DependencyRelationship(_startAnchor);
                    break;
                case "Aggregation":
                    relationship = new AggregationRelationship(_startAnchor);
                    break;
                case "Composition":
                    relationship = new CompositionRelationship(_startAnchor);
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
