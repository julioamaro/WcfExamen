using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CoberturasFianzas.Models
{
    [System.Runtime.Serialization.DataContract]
    [Serializable()]
    public class ProductosModels
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Pais { get; set; }
        [DataMember]
        public int idRamo { get; set; }
    }
}