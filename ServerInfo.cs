﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLWorker
{
    public class ServerInfo
    {
        
        public string ConnectionPath { get; set; }

        private string server_ip;
        public string ServerIp { 
            get => server_ip;
            set {
                server_ip = value;
                UpdateConnectionPath();
            } 
        }

        private string login;
        public string Login {
            get => login;
            set {
                login = value;
                UpdateConnectionPath();
            }
        }

        private string password;
        public string Password {
            get => password;
            set {
                password = value;
                UpdateConnectionPath();
            }
        }

        private string database_name;
        public string DatabaseName{
            get => database_name;
            set {
                database_name = value;
                UpdateConnectionPath();
            }
        }

        private string table_name;
        public string TableName {
            get => table_name;
            set {
                table_name = value;
                UpdateConnectionPath();
            }
        }

        private bool integrated_security;
        public bool IntegratedSecurity {
            get => integrated_security;
            set {
                integrated_security = value;
                UpdateConnectionPath();
            }
        }

        public ServerInfo()
        {
            this.ConnectionPath = string.Empty;
            this.server_ip = string.Empty;
            this.database_name = string.Empty;
            this.login = string.Empty;
            this.password = string.Empty;
            this.table_name = string.Empty;
            this.integrated_security = false;
        }

        public void UpdateConnectionPath()
        {
            this.ConnectionPath = $@"Data Source={this.ServerIp};Initial Catalog={this.DatabaseName};User Id = {this.Login};Password = {this.Password};Integrated Security={this.IntegratedSecurity};";
        }
    }
}