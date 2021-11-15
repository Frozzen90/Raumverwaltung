using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raumverwaltung.Controllers
{
    public class ServerController
    {
        private bool _isSSHopened;
        private SshClient _Client;

        public bool IsSshOpened { get => _isSSHopened; set => _isSSHopened = value; }
        public SshClient Client { get => _Client; set => _Client = value; }

        public ServerController()
        {
            IsSshOpened = false;
            Client = null;
        }


        private void ConnectSSH()
        {
            string host = "";
            string username = "";
            string dings = "";
            Client = new SshClient(host, username, dings);
            Client.Connect();
            IsSshOpened = Client.IsConnected;
        }

        private void ConnectMySql(string SqlStr)
        {
            string Server = "";
            string Port = "";
            string UserID = "";
            string dings = "";
            string DbName = "";
            string MySqlStr = $"SERVER={Server};PORT={Port};UID={UserID};PASSWORD={dings};DATABASE={DbName}";
            MySqlCommand Com = new MySqlCommand();
            Com.Connection = new MySqlConnection(MySqlStr);
            Com.CommandText = SqlStr;

        }

        private void DisconnectSSH()
        {
            Client.Disconnect();
            Client = null;
            IsSshOpened = false;
        }

        public void SQLRequest()
        {
            try
            {
                ConnectSSH();
                if (IsSshOpened)
                {

                    
                }
                else
                {
                    //nicht verbunden
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                DisconnectSSH();
            }
        }
    }
}