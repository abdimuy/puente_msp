using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirebirdSql.Data.FirebirdClient;
using ApiBas = ApisMicrosip.ApiMspBasicaExt;

namespace SysInventory.Controllers
{
    public class ValuesController : ApiController
    {
        private FbConnection connection;
        // GET api/values
        public int Get()
        {
            string connectionStr = "User=SYSDBA;Password=masterkey;Database=C:\\dev\\MUEBLERA_SNP.fdb;DataSource=localhost;Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;";
            connection = new FbConnection(connectionStr);
            connection.Open();
            string sql = "SELECT FIRST 1 ALMACEN_ID, NOMBRE FROM ALMACENES";
            FbDataReader reader = createCommand(sql);


            int db = ApiBas.DBConnect(0, "C:\\dev\\MUEBLERA_SNP.fdb", "sysdba", "masterkey");

            return db;

            while (reader.Read())
            {
                Console.WriteLine(reader.GetFieldValue<int>(0));
                int mmm = reader.GetFieldValue<int>(0);
            return mmm;
            }
            return 000000;
        }

        private FbDataReader createCommand(string sql)
        {
            FbCommand command = new FbCommand(sql, connection);
            return command.ExecuteReader();
        }

        // GET api/values/5
        public string Get(int id)
        {
            string sql = "SELECT ALMACEN_ID, NOMBRE FROM ALMACENES";
            FbDataReader reader = createCommand(sql);
            while(reader.Read())
            {
                Console.WriteLine(reader.GetFieldValue<int>(0));
            }
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
            Console.WriteLine("Hola");
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
