using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLCreator
{
    public class Method
    {
        private BindingList<MethodArgument> arguments;

        public AccessModifier Access { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public BindingList<MethodArgument> Arguments { get; set; }

        public Method(AccessModifier access, string type, string name)
        {
            this.Access = access;
            this.Type = type;
            this.Name = name;
            Arguments = new BindingList<MethodArgument>();
        }

        public void AddArgument(string name, string type)
        {
            Arguments.Add(new MethodArgument(name, type));
        }

        public void RemoveArgument(int index)
        {
            Arguments.RemoveAt(index);
        }

        public override string ToString()
        {
            return (char)Access + Name + $"({string.Join(',', Arguments)}) : " + Type;
        }
    }
}
