using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class DTOValoreAttributoIdentifier : IKey
    {
       
        public DTOGruppoAttributi GruppoAttributo {get;set;}
        public DTOProdottoDiListino ProdottoDiListino { get; set; }

        public int CompareTo(object obj)
        {
            var dto = obj as DTOValoreAttributoIdentifier;
            if (dto == null) return -1;
            if ((dto.GruppoAttributo == null && GruppoAttributo!=null )|| GruppoAttributo==null && dto.GruppoAttributo!=null) return -1;
            if ((dto.ProdottoDiListino == null && ProdottoDiListino != null) || ProdottoDiListino == null && dto.ProdottoDiListino != null) return -1;
            return (GruppoAttributo.CompareTo(dto.GruppoAttributo)) == 0 ? ProdottoDiListino.CompareTo(dto.ProdottoDiListino) : GruppoAttributo.CompareTo(dto.GruppoAttributo);

        }
    }

    public class DTOValoreAttributo : DTOValoreAttributoIdentifier,IDTO<DTOValoreAttributoIdentifier>
    {
        public DTOValoreAttributoIdentifier Identifier => new DTOValoreAttributoIdentifier { GruppoAttributo = this.GruppoAttributo, ProdottoDiListino = this.ProdottoDiListino };

        public eDTOState State { get; set; } = eDTOState.InsertedModified;
    }
}
