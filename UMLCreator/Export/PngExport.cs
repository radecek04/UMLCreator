using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLCreator.Export
{
    public class PngExport : IExport
    {
        public LayerManager _layerManager { get; set; }
        public List<Relationships.Relationship> _relationshipList { get; set; }
        public PictureBox _pictureBox { get; set; }

        public PngExport(LayerManager layers, List<Relationships.Relationship> relationships, PictureBox pb)
        {
            _layerManager = layers;
            _relationshipList = relationships;
            _pictureBox = pb;
        }
        public void Export()
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = "Diagram.png";
            sf.Filter = "PNG (*.png)|*.png";
            if(sf.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(_pictureBox.DisplayRectangle.Width, _pictureBox.DisplayRectangle.Height);
                _pictureBox.DrawToBitmap(bmp, _pictureBox.DisplayRectangle);
                bmp.Save(sf.FileName);
            }
        }
    }
}
