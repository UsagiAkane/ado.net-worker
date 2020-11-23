using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLWorker
{
    public class ServerInfo
    {
        public event EventHandler ConnectionChanged;

        private string server_ip;
        public string ServerIp {
            get => server_ip;
            set {
                server_ip = value;
                ConnectionChanged?.Invoke(this, new EventArgs());
            }
        }

        private string login;
        public string Login {
            get => login;
            set {
                login = value;
                ConnectionChanged?.Invoke(this, new EventArgs());
            }
        }

        private string password;
        public string Password {
            get => password;
            set {
                password = value;
                ConnectionChanged?.Invoke(this, new EventArgs());
            }
        }

        private string database;
        public string Database {
            get => database;
            set {
                database = value;
                ConnectionChanged?.Invoke(this, new EventArgs());
            }
        }

        private string table_name;
        public string TableName {
            get => table_name;
            set {
                table_name = value;
                ConnectionChanged?.Invoke(this, new EventArgs());
            }
        }

        private bool integrated_security;
        public bool IntegratedSecurity {
            get => integrated_security;
            set {
                integrated_security = value;
                ConnectionChanged?.Invoke(this, new EventArgs());
            }
        }

        public ServerInfo()
        {
            this.ServerIp = string.Empty;
            this.Database = string.Empty;
            this.Login = string.Empty;
            this.Password = string.Empty;
            this.TableName = string.Empty;
            this.IntegratedSecurity = false;
        }
    }
}
