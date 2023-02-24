using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskLogger
{
    public class FuncConfigurations
    {
        private FuncConfigurations(string value) { Value = value; }

        public string Value { get; private set; }

        public static FuncConfigurations DataURL { get { return new FuncConfigurations("https://api.publicapis.org/random?auth=null"); } }

        public override string ToString()
        {
            return Value;
        }
    }
}