using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLCreator
{
    public class MethodArgument
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public MethodArgument(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public override string ToString()
        {
            return Type + " " + Name;
        }
    }
}
