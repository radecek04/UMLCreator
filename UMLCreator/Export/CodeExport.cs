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

        public void Export(string path)
        {
            foreach (Class c in _layerManager)
            {
                using (StreamWriter sw = new StreamWriter(path + "\\" + c.Name + ".cs"))
                {
                    sw.WriteLine("using System;");
                    sw.WriteLine("using System.Collections.Generic;");
                    sw.WriteLine("using System.Linq;");
                    sw.WriteLine("using System.Text;");
                    sw.WriteLine();
                    sw.WriteLine($"namespace {_namespace}");
                    sw.WriteLine("{");
                    List<string> connectedRelationships = _relationshipList.Where(r => r.EndClass == c && (r is InheritanceRelationship || r is ImplementationRelationship)).Select(x => x.StartClass.Name).ToList();
                    List<Class> implementationClasses = _relationshipList.Where(r => r.EndClass == c && r is ImplementationRelationship && r.StartClass.Type == ObjectType.Interface).Select(x => x.StartClass).ToList();

                    string objectType = "interface";
                    if (c.Type == ObjectType.None)
                        objectType = "class";
                    else if (c.Type != ObjectType.Interface)
                        objectType = $"{Enum.GetName<ObjectType>(c.Type).ToLower()} class";

                    sw.WriteLine($"\tpublic {objectType} {c.Name}{(connectedRelationships.Count > 0 ? " : " : "")}{string.Join(", ", connectedRelationships)}");
                    sw.WriteLine("\t{");
                    foreach (Property p in c.Properties)
                    {
                        sw.WriteLine($"\t\t{Enum.GetName<AccessModifier>(p.Access).ToLower()} {p.Type} {p.Name} {{ get; set; }}");
                    }
                    foreach (Class cl in implementationClasses)
                    {
                        foreach (Property p in cl.Properties)
                        {
                            sw.WriteLine($"\t\t{Enum.GetName<AccessModifier>(p.Access).ToLower()} {p.Type} {p.Name} {{ get => throw new NotImplementedException(); set => throw new NotImplementedException(); }}");
                        }
                    }
                    foreach (Method m in c.Methods)
                    {
                        sw.Write($"\t\t{Enum.GetName<AccessModifier>(m.Access).ToLower()} {m.Type} {m.Name}({string.Join(", ", m.Arguments)})");
                        if (c.Type == ObjectType.Interface)
                        {
                            sw.WriteLine(";");
                            break;
                        }
                        sw.WriteLine();
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\tthrow new NotImplementedException();");
                        sw.WriteLine("\t\t}");
                    }
                    foreach (Class cl in implementationClasses)
                    {
                        foreach (Method m in cl.Methods)
                        {
                            sw.Write($"\t\t{Enum.GetName<AccessModifier>(m.Access).ToLower()} {m.Type} {m.Name}({string.Join(", ", m.Arguments)})");
                            if (c.Type == ObjectType.Interface)
                            {
                                sw.WriteLine(";");
                                break;
                            }
                            sw.WriteLine();
                            sw.WriteLine("\t\t{");
                            sw.WriteLine("\t\t\tthrow new NotImplementedException();");
                            sw.WriteLine("\t\t}");
                        }
                    }
                    sw.WriteLine("\t}");
                    sw.WriteLine("}");
                }
            }
        }

        public string OpenPath()
        {
            FolderBrowserDialog sf = new FolderBrowserDialog();
            if (sf.ShowDialog() != DialogResult.OK)
            {
                return "";
            }
            return sf.SelectedPath;
        }
    }
}
