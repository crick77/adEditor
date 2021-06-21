using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adEditor
{
    class RefData
    {
        public object data = null;
        public int refCount = 0;
        public string guid = null;

        public RefData(object data)
        {
            this.data = data;
            this.guid = Guid.NewGuid().ToString();
        }

        public RefData(object data, int refCount)
        {
            this.data = data;
            this.refCount = refCount;
            this.guid = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return data + " (Reference count: " + refCount + ")";
        }
    }
}
