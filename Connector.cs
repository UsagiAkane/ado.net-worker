using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLWorker
{
    public class Connector
    {
        public string TableName { get; set; }
        public ServerInfo ServerInfo { get; set; }

        public Connector(ServerInfo server)
        {
            this.ServerInfo = new ServerInfo();
        }
       
        public object[,] ReadTable() => Execute.QuerySelect("*", this.TableName, this.ServerInfo.ConnectionPath);
    }
}
