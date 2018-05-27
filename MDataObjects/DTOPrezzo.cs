
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataObjects
{
    public enum eTipoPrezzo:int
    {
        Vendita=1,
        Acquisto=2
    }
    public class DTOPrezzoIdentifier : IKey
    {

       

        public int CompareTo(object obj)
        {
            var dto = obj as DTOPrezzoIdentifier;
            if (dto == null) return -1;
            if ((dto.Listino == null && Listino != null) || Listino == null && dto.Listino != null) return -1;
            if ((dto.ProdottoDiListino == null && ProdottoDiListino != null) || ProdottoDiListino == null && dto.ProdottoDiListino != null) return -1;
            return (ProdottoDiListino.CompareTo(dto.ProdottoDiListino)) == 0 ? Listino.CompareTo(dto.Listino) : ProdottoDiListino.CompareTo(dto.ProdottoDiListino);

        }
        public DTOProdottoDiListino ProdottoDiListino { get; set; }
        public DTOListino Listino { get; set; }
        
    }

    public class DTOPrezzo : DTOPrezzoIdentifier,IDTO<DTOPrezzoIdentifier>
    {
        public DTOPrezzoIdentifier Identifier => this;

        public eDTOState State { get; set; } = eDTOState.InsertedModified;

        public decimal Prezzo { get; set; }
        
        public decimal Maggiorazione { get; set; } //-->todo magiorazione dovrebbe poter essere accumulata  
        //a partire da tutti i livelli( categoria ricorsiva, prodotto, gruppo attributi, valore gruppo attributi, prodotto di listino)
    }
}