using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjects
{

    public class DTOFunctionGroup
    {
        public  string Caption { get; set; }
        public  List<DTOFunction> Functions { get; set; } = new List<DTOFunction>();
        
    }

    public class DTOFunction
    {
        public  string Caption { get; set; }
        public string Action { get; set; }

    }
}
