using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using ApiBas = ApisMicrosip.ApiMspBasicaExt;
using ApiIn = ApisMicrosip.ApiMspInventExt;

namespace SysInventory.Controllers
{
    public class Movimiento
    {
        public int concepto_id { get; set; }
        public int almacen_init_id { get; set; }
        public int almacen_finish_id { get; set; }
        public string fecha { get; set; }
        public string descripcion { get; set; }
        public List<MovimientoIndiv> lista { get; set; }
    }

    public class MovimientoIndiv
    {
        public int articulo_id { get; set; }
        public int unidades { get; set; }
    }
    public class InventoryController : ApiController
    {
        // GET: Inventory
        //public string Index()
        //{
        //    return "Hola";
        //}

        public List<int> Get()
        {
            var result = new List<int>();
            int db = ApiBas.DBConnect(0, "c:\\dev\\MUEBLERA_SNP.fdb", "sysdba", "masterkey");
            int resInConnect = ApiIn.SetDBInventarios(0);   
            int res1 = ApiIn.NuevaSalida(36, 11342, 11343, "26/04/2022", "", "Descripcion de prueba", 0);
            int res2 = ApiIn.RenglonSalida(474, 1, 0, 0);
            int res3 = ApiIn.AplicaSalida();
            int disconnect = ApiBas.DBDisconnect(-1);
            result.Add(db);
            result.Add(resInConnect);
            result.Add(res1);
            result.Add(res2);
            result.Add(res3);
            result.Add(disconnect);
            return result;
        }

        public List<int> Post([FromBody] Movimiento movimiento)
        {
            var result = new List<int>();
            int db = ApiBas.DBConnect(0, "c:\\dev\\MUEBLERA_SNP.fdb", "sysdba", "masterkey");
            int resInConnect = ApiIn.SetDBInventarios(0);
            int res1 = ApiIn.NuevaSalida(
                movimiento.concepto_id,
                movimiento.almacen_init_id,
                movimiento.almacen_finish_id,
                movimiento.fecha,
                "",
                movimiento.descripcion,
                0
            );
            //var res123 = new List<int>();
            for (int i = 0; i < movimiento.lista.Count(); i++)
            {
                result.Add(ApiIn.RenglonSalida(movimiento.lista[i].articulo_id, movimiento.lista[i].unidades, 0, 0));
            }
            int res3 = ApiIn.AplicaSalida();
            int disconnect = ApiBas.DBDisconnect(-1);
            result.Add(db);
            result.Add(resInConnect);
            result.Add(res1);
            //result.Add(res2);
            //result.Add(res123);
            result.Add(res3);
            result.Add(disconnect);
            result.Add(movimiento.lista.Count());
            return result;
            //return movimiento;
        }
    }
}