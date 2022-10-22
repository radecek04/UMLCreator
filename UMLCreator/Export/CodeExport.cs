using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLCreator.Relationships;

namespace UMLCreator.Export
{
    public class CodeExport : IExport
    {
        public LayerManager _layerManager { get; set; }
        public List<Relationships.Relationship> _relationshipList { get; set; }
        public PictureBox _pictureBox { get; set; }
        private string _namespace;

        public CodeExport(LayerManager layers, List<Relationships.Relationship> relationships, string namespaces)
        {
            _layerManager = layers;
            _relationshipList = relationships;
            _namespace = namespaces;
        }

        public void Export()
        {
            FolderBrowserDialog sf = new FolderBrowserDialog();
            if (sf.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (Class c in _layerManager)
            {
                using(StreamWriter sw = new StreamWriter(sf.SelectedPath + "\\" +  c.Name + ".cs"))
                {
                    sw.WriteLine("using System;");
                    sw.WriteLine("using System.Collections.Generic;");
                    sw.WriteLine("using System.Linq;");
                    sw.WriteLine("using System.Text;");
                    sw.WriteLine();
                    sw.WriteLine($"namespace {_namespace}");
                    sw.WriteLine("{");
                    List<string> connectedRelationships = _relationshipList.Where(r => r.EndClass == c && (r is InheritanceRelationship || r is ImplementationRelationship)).Select(x => x.StartClass.Name).ToList();
                    sw.WriteLine($"\tpublic{(c.IsAbstract ? " abstract" : "")} class {c.Name}{(connectedRelationships.Count > 0 ? " : " : "")}{string.Join(", ",connectedRelationships)}");
                    sw.WriteLine("\t{");
                    foreach(Property p in c.Properties)
                    {
                        sw.WriteLine($"\t\t{Enum.GetName<AccessModifier>(p.Access).ToLower()} {p.Type} {p.Name} {{ get; set; }}");
                    }
                    foreach(Method m in c.Methods)
                    {
                        sw.WriteLine($"\t\t{Enum.GetName<AccessModifier>(m.Access).ToLower()} {m.Type} {m.Name}({string.Join(", ", m.Arguments)})");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\tthrow new NotImplementedException();");
                        sw.WriteLine("\t\t}");
                    }
                    sw.WriteLine("\t}");
                    sw.WriteLine("}");
                }
            }
        }
    }
}
