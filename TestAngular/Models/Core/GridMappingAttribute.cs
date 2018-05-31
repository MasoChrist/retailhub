using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace RetailHubWeb.Models
{


    /// <summary>
    /// una ricerca tramite webapi tornerà tipicamente un DTO con id e X proprietà.
    /// Al modello passerò anche una lista di questi attributi in modo che possa costruire la griglia.
    /// Così mantengo solo le Api e non mi devo sbattere a scrivere il javascript per mappare le DTOEntitites, che sono molto strutturate
    /// </summary>
        public class GridMappingAttribute : Attribute
        {
            public int Order { get; set; }
            public bool Visible { get; set; }
            public string Name { get; set; }

            public string Caption { get; set; }

            public static List<GridMappingAttribute> GetList(Type tipo)
            {
           
               var  _visiblita = new List<GridMappingAttribute>();
                foreach (var prop in tipo.GetProperties())
                {
                    var map = new GridMappingAttribute

                    {
                        Caption = prop.Name,
                        Name = prop.Name,
                        Visible = false
                    };
                    foreach (var attr in prop.GetCustomAttributes(true))
                    {
                        var tattr = attr as GridMappingAttribute;
                        if (tattr != null)
                        {
                            map.Caption = string.IsNullOrEmpty(tattr.Caption) ? map.Name : tattr.Caption;
                            map.Visible = tattr.Visible;
                            break;
                        }

                    }
                    _visiblita.Add(map);
                }
                _visiblita = _visiblita.OrderBy(x => x.Order).ToList();
                return _visiblita;
            }
       
        }
    
       
}