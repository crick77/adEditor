using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adEditor
{
    class ComboBoxVar
    {
        public string varName;
        public object varData;

        public ComboBoxVar(string varName, object varData)
        {
            this.varName = varName;
            this.varData = varData;
        }

        public override string ToString()
        {
            return this.varName;
        }
    }
}
