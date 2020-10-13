using CoberturasFianzas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfExamen
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServiceExamen
    {

        [OperationContract]
        [ServiceKnownType(typeof(List<RamosModels>))]
        List<RamosModels> BuscarRamos(int id, string Descripcion, string Pais);

        [OperationContract]
        [ServiceKnownType(typeof(List<ProductosModels>))]
        List<ProductosModels> BuscarProductos(int id, string Descripcion, string Pais, int idRamos);

        [OperationContract]
        string VerificarUsuario(string userName, string psw);

        // TODO: agregue aquí sus operaciones de servicio
    }
}
