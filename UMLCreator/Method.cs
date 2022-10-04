using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLCreator
{
    public class Method
    {
        public AccessModifier Access { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        public Method(AccessModifier access, string type, string name)
        {
            this.Access = access;
            this.Type = type;
            this.Name = name;
        }

        public override string ToString()
        {
            return (char)Access + Name + "() : " + Type;
        }
    }
}
