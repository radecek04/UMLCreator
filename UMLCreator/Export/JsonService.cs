using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UMLCreator.Export
{
    public class JsonService : IExport
    {
        public LayerManager _layerManager { get; set; }
        public List<Relationships.Relationship> _relationshipList { get; set; }
        private JsonSerializerSettings _serializerSettings;
        public PictureBox _pictureBox { get; set; }

        public JsonService(LayerManager layers, List<Relationships.Relationship> relationships)
        {
            _layerManager = layers;
            _relationshipList = relationships;
            _serializerSettings = new JsonSerializerSettings();
            _serializerSettings.TypeNameHandling = TypeNameHandling.All;
            _serializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
        }

        public void Import()
        {
            OpenFileDialog sf = new OpenFileDialog();
            sf.Filter = "JSON (*.json)|*.json";
            if (sf.ShowDialog() != DialogResult.OK)
                return;

            string json;
            using (StreamReader sr = new StreamReader(sf.FileName))
            {
                json = sr.ReadToEnd();
            }
            try
            {
                ExportData data = JsonConvert.DeserializeObject<ExportData>(json, _serializerSettings);
                _layerManager.LoadFromList(data.Classes);
                _relationshipList.Clear();
                _relationshipList.AddRange(data.Relationships);
            }
            catch
            {
                MessageBox.Show("File cannot be loaded.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        public void Export()
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = "Save.json";
            sf.Filter = "JSON (*.json)|*.json";
            if (sf.ShowDialog() != DialogResult.OK)
                return;

            ExportData data = new ExportData(_layerManager.ToList(), _relationshipList);
            using (StreamWriter sw  = new StreamWriter(sf.FileName))
            {
                sw.Write(JsonConvert.SerializeObject(data, _serializerSettings));
            }
        }
    }
}
