using System;

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

        private bool integrated_security;

        public bool IntegratedSecurity {
            get => integrated_security;
            set {
                integrated_security = value;
                ConnectionChanged?.Invoke(this, new EventArgs());
            }
        }

        public ServerInfo() {
            this.server_ip = string.Empty;
            this.database = string.Empty;
            this.login = string.Empty;
            this.password = string.Empty;
            this.integrated_security = false;
        }
    }
}