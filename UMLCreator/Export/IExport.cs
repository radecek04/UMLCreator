using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLCreator.Export
{
    public interface IExport
    {
        protected LayerManager _layerManager { get; set; }
        protected List<Relationships.Relationship> _relationshipList { get; set; }
        protected PictureBox _pictureBox { get; set; }
        public void Export();
    }
}
