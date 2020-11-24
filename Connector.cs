using System;

namespace MySQLWorker
{
    public class Connector
    {
        public string ConnectionPath { get; set; }
        public string TableName { get; set; }
        public ServerInfo ServerInfo { get; set; }

        public Connector(ServerInfo server_info) {
            UpdateConnectionPath(server_info);
            this.ServerInfo = server_info;
            this.ServerInfo.ConnectionChanged += ConnectionChanged;
        }

        private void ConnectionChanged(object sender, EventArgs e) {
            if (sender is ServerInfo info) {
                UpdateConnectionPath(info);
            }
        }

        private void UpdateConnectionPath(in ServerInfo info) {
            this.ConnectionPath = $@"Data Source={info.ServerIp};Initial Catalog={info.Database};User Id = {info.Login};Password = {info.Password};Integrated Security={info.IntegratedSecurity};Connection Timeout=1;";
        }

        public object[,] ReadTable(in string what_to_select = "*", in string select_where = null) {
            try {
                return Execute.QuerySelect(what_to_select, this.TableName, select_where, this.ConnectionPath);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void InsertInTable(in string[] col_names, in object[] values) {
            try {
                Execute.QueryInsertMultyValues(this.TableName, col_names, values, this.ConnectionPath);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void CustomQuery(in string query) {
            try {
                Execute.CustomQueryExec(query, this.ConnectionPath);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}