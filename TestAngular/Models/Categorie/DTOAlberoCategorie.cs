using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace RetailHubWeb.Models
{
    public class DTOAlberoCategorie
    {
        public DTOAlberoCategorie(DTOCategoria dto)
        {
            ID = dto.Identifier.ID.ToString();
            Descrizione = dto.Nome;
        }
        public string ID { get; set; }
        public string Descrizione { get; set; }
        /// <summary>
        /// Appoggio, lasciare tutto null
        /// </summary>
        public AlberoCategorieState State { get; set; } = new AlberoCategorieState(false,false,false);
        public List<DTOAlberoCategorie> Children { get; set; }

    }

    public class AlberoCategorieState
    {
        public bool Opened = false;
        public bool Disabled = false;
        public bool Selected = false;

        public AlberoCategorieState(bool opened, bool disabled, bool selected)
        {
            Opened = opened;
            Disabled = disabled;
            Selected = selected;
        }
    }
}