using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace TestAngular.Models
{

    
    public class DTOListaProdotti
    {
        public static DTOListaProdotti Map(DTOProdotto prodotto)
        {
            return new DTOListaProdotti
            {
                ID = prodotto.ID,
                AliquotaIvaAcquisto = 0,
                AliquotaIvaVendita = 0,
                Descrizione = prodotto.Descrizione,
                DescrizioneBreve = prodotto.DescrizioneBreve
                ,
              //  PrezzoAcquisto = prodotto?.PrezziAcquisto?.FirstOrDefault()?.Price ?? 0,

                //PrezzoVendita = prodotto?.PrezziVendita?.FirstOrDefault()?.Price ?? 0,   ScontoAcquisto = 0,ScontoVendita = 0,SKU = prodotto.SKU

            };
        }
        public  Guid ID { get; set; }
        public  string Descrizione { get; set; }
        public  string DescrizioneBreve { get; set; }
        public  string SKU { get; set; }

        public  decimal PrezzoVendita { get; set; }
        public  decimal AliquotaIvaVendita { get; set; }
        public  decimal ScontoVendita { get; set; }
       

        public  decimal PrezzoAcquisto { get; set; }
        public  decimal AliquotaIvaAcquisto { get; set; }
        public decimal ScontoAcquisto { get; set; }
        

    }
}