using System;
using System.Collections.Generic;
using System.Text;
using DataObjects;

namespace DataAccess
{
    public  class NavBarService
    {
        private static List<DTONavBar> Items
        {
            get
       
            {
                var t = new DTONavBar();
                t.CurrentItem = new NavBarItem
                {
                    Caption = "Prodotti"
                };
                t.ChildItems = new List<NavBarItem>();
                var ch = new NavBarItem();
                ch.Caption = "Anagrafica";
                ch.Action = "index";
                ch.Controller = "ProdottoView";
                t.ChildItems.Add(ch);
                return new List<DTONavBar>() {t};
            }
            
        }
        public List<DTONavBar> GetItems()
        {
            return Items;
        }
    }
}
