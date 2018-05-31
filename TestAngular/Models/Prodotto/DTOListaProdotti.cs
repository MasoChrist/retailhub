using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;
using RetailHubWeb.Models;

namespace TestAngular.Models
{


    public class DTOListaProdottiModel : BaseModel
    {
        public DTOListaProdottiModel(string token) : base(token)
        {
        }
        public  GridModel<DTOListaProdotti> DTOListaProdotti { get; } = new GridModel<DTOListaProdotti>();

    }


    public class DTOListaProdotti
    {
        public static DTOListaProdotti Map(DTOProdotto prodotto)
        {
            return new DTOListaProdotti
            {
                ID = prodotto.ID,
                Descrizione = prodotto.Descrizione,
                DescrizioneBreve = prodotto.DescrizioneBreve
               
              //  PrezzoAcquisto = prodotto?.PrezziAcquisto?.FirstOrDefault()?.Price ?? 0,

                //PrezzoVendita = prodotto?.PrezziVendita?.FirstOrDefault()?.Price ?? 0,   ScontoAcquisto = 0,ScontoVendita = 0,SKU = prodotto.SKU

            };
        }
        [GridMapping(Visible = false,Order = 0)]
        public  Guid ID { get; set; }
        [GridMapping(Visible = true,Caption = "Descrizione",Order = 1)]
        public  string Descrizione { get; set; }
        [GridMapping(Visible = true, Caption = "Descrizione Breve", Order = 2)]
        public  string DescrizioneBreve { get; set; }
        

    }

    
}