using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjects
{
    public class DTONavBar
    {
       
        public  NavBarItem CurrentItem { get; set; }
        public  List<NavBarItem> ChildItems { get; set; }
        
    }
}
