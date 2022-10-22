using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLCreator.Export
{
    public class ExportData
    {
        public List<Class> Classes;
        public List<Relationships.Relationship> Relationships;

        public ExportData(List<Class> classes, List<Relationships.Relationship> relationships)
        {
            Classes = classes;
            Relationships = relationships;
        }
    }
}
