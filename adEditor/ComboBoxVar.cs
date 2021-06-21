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
        public string varGuid;
        public object varData;

        public ComboBoxVar(string varName, string varGuid, object varData)
        {
            this.varName = varName;
            this.varGuid = varGuid;
            this.varData = varData;
        }

        public override string ToString()
        {
            return this.varName;
        }
    }
}
