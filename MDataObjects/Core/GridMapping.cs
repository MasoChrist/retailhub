using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjects.Core
{
    public class GridMappingAttribute:Attribute
    {
        public int Order { get; set; }
        public  bool Visible { get; set; }
        public  string Name { get; set; }

        public  string Caption { get; set; }

    }
}
