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
        public string ConnectionPath { get; set; }
        public string TableName { get; set; }
        public ServerInfo ServerInfo { get; set; }

        public Connector(ServerInfo server_info)
        {
            UpdateConnectionPath(server_info);
            this.ServerInfo = server_info;
            this.ServerInfo.ConnectionChanged += ConnectionChanged;
        }

        private void ConnectionChanged(object sender, EventArgs e)
        {
            if (sender is ServerInfo info) {
                UpdateConnectionPath(info);
            }
        }
        private void UpdateConnectionPath(in ServerInfo info)
        {
            this.ConnectionPath = $@"Data Source={info.ServerIp};Initial Catalog={info.Database};User Id = {info.Login};Password = {info.Password};Integrated Security={info.IntegratedSecurity};Connection Timeout=1;";
        }

        public object[,] ReadTable()
        {
            try {
                return Execute.QuerySelect("*", this.TableName, this.ConnectionPath);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public void InsertTable(in string[] col_names, in object[] values)
        {
            try {
                Execute.QueryInsertMultyValues(this.TableName, col_names, values, this.ConnectionPath);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
