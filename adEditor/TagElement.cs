﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adEditor
{
    class TagElement
    {
        public string type;
        public string name;
        public object data;
        public bool editable;
        public string extension;

        public TagElement(string type)
        {
            this.type = type;
            this.name = null;
            this.data = null;
            this.editable = false;
        }

        public TagElement(string type, string name, object data)
        {
            this.type = type;
            this.name = name;
            this.data = data;
            this.editable = false;
        }

        public TagElement(string type, string name, object data, bool editable)
        {
            this.type = type;
            this.name = name;
            this.data = data;
            this.editable = editable;
        }

        public TagElement(bool editable)
        {
            this.type = this.name = "*";
            this.data = null;
            this.editable = editable;
        }
    }
}
