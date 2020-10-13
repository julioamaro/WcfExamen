using CoberturasFianzas.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;

namespace WcfExamen
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IServiceExamen
    {

        public List<RamosModels> BuscarRamos(int id, string Descripcion, string Pais)
        {
            try
            {
                string documento = "C:/Users/Marcs/source/repos/WcfExamen/WcfExamen/Documentos/Productos_LATAM.xml";

                if (!File.Exists(documento))
                {
                    return null;
                }

                XElement xElement = XElement.Load(documento);
                List<RamosModels> lsRamos = new List<RamosModels>();
                RamosModels newRamos = new RamosModels();

                var consulta =
                    (from oRamos in xElement.Elements("Ramo")
                     where (oRamos.Attribute("id").Value.ToString() == id.ToString() ||
                            oRamos.Attribute("Descripcion").Value.Equals(Descripcion)) &&
                            oRamos.Attribute("Pais").Value.Equals(Pais) 
                     select oRamos).ToList();

                foreach (var elemento in consulta)
                {
                    newRamos = new RamosModels()
                    {
                        id          = Convert.ToInt32(elemento.Attribute("id").Value),
                        Descripcion = elemento.Attribute("Descripcion").Value
                    };

                    lsRamos.Add(newRamos);
                }
                return lsRamos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<ProductosModels> BuscarProductos(int id, string Descripcion, string Pais, int idRamos)
        {
            try
            {
                string documento = "C:/Users/Marcs/source/repos/WcfExamen/WcfExamen/Documentos/Productos_LATAM.xml";

                if (!File.Exists(documento))
                {
                    return null;
                }

                XElement xElement = XElement.Load(documento);
                List<ProductosModels> lsProductos = new List<ProductosModels>();

                ProductosModels newProducto = new ProductosModels();

                var consulta =
                   (from oRamos in xElement.Elements("Ramo")
                    where oRamos.Attribute("id").Value.ToString() == idRamos.ToString()
                    select oRamos).ToList();

                foreach (var father in consulta)
                {
                    var consultaProductos =
                   (from oProductos in father.Elements("Productos")
                    where (Convert.ToInt32(oProductos.Attribute("id").Value) == id ||
                           oProductos.Attribute("Descripcion").Value.Equals(Descripcion)) &&
                           oProductos.Attribute("Pais").Value.Equals(Pais)
                    select oProductos).ToList();

                    foreach (var ChildrenProdutos in consultaProductos)
                    {
                        newProducto = new ProductosModels()
                        {
                            Descripcion = ChildrenProdutos.Attribute("Descripcion").Value,
                            id = Convert.ToInt32(ChildrenProdutos.Attribute("id").Value),
                            idRamo = Convert.ToInt32(idRamos)
                        };

                        lsProductos.Add(newProducto);
                    }
                }

                return lsProductos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string VerificarUsuario(string userName, string psw)
        {
            try
            {
                string Pais = string.Empty;
                string documento = "C:/Users/Marcs/source/repos/WcfExamen/WcfExamen/Documentos/Usuarios_LATAM.xml";

                if (!File.Exists(documento))
                {
                    return null;
                }

                XElement xElement = XElement.Load(documento);
                List<UsariosModels> lsaurios = new List<UsariosModels>();

                UsariosModels user = new UsariosModels();

                var consulta =
                    (from Usuarios in xElement.Descendants("UserPais").Elements("User")
                     where Usuarios.Attribute("UserName").Value.Equals(userName) &&
                           Usuarios.Attribute("Pws").Value.Equals(psw)
                     select Usuarios).ToList();

                foreach (var elemento in consulta)
                {
                    user = new UsariosModels()
                    {
                        Id = Convert.ToInt32(elemento.Attribute("id").Value),
                        Name = elemento.Attribute("Name").Value,
                        UserName = elemento.Attribute("UserName").Value,
                        Pws = elemento.Attribute("Pws").Value,
                        Pais = elemento.Attribute("Pais").Value
                    };

                    Pais = user.Pais;
                }
                return Pais;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
