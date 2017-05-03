using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect.core.Services.Models
{
   public class DB
    {
        private readonly SQLiteAsyncConnection conn;

        public string StatusMessage { get; set; }

        public void Repository(string dbPath)
        {
            var conexion = new SQLiteAsyncConnection(dbPath);
            conexion.CreateTableAsync<DatosClima>().Wait();
            
        }

        public async Task CreateBill(DatosClima DatosClima)
        {
           
                var result = await conn.InsertAsync(DatosClima).ConfigureAwait(continueOnCapturedContext: false);
                StatusMessage = $"{result} record(s) added [Customer Email: {DatosClima.Pais})";
            
        }


    }
}
