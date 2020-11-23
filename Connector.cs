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
        public ServerInfo ServerInfo { get; set; }

        public Connector()
        {
            this.ServerInfo = new ServerInfo();
        }

        public object[,] ReadTable() => Execute.QuerySelect("*", this.ServerInfo.TableName, this.ServerInfo.ConnectionPath);
    }
}
