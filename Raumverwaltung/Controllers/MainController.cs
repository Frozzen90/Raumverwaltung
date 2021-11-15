using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace Raumverwaltung.Controllers
{
    public class MainController
    {
        private ServerController _SrvCntr;

        public ServerController SrvCntr { get => _SrvCntr; set => _SrvCntr = value; }

        public MainController()
        {
            SrvCntr = new ServerController();
        }

        public void LoadSqlCommand()
        {
            try
            {
                using (var client = new SshClient("p602752@p602752.mittwaldserver.info", "p602752", "ya+97#sovjatDq")) // establishing ssh connection to server where MySql is hosted
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                        var portForwarded = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                        client.AddForwardedPort(portForwarded);
                        portForwarded.Start();
                        using (MySqlConnection con = new MySqlConnection("SERVER=127.0.0.1;PORT=3306;UID=someuser;PASSWORD=somepassword;DATABASE=DbName"))
                        {
                            using (MySqlCommand com = new MySqlCommand("SELECT * FROM tableName", con))
                            {
                                com.CommandType = CommandType.Text;
                                DataSet ds = new DataSet();
                                MySqlDataAdapter da = new MySqlDataAdapter(com);
                                da.Fill(ds);
                                foreach (DataRow drow in ds.Tables[0].Rows)
                                {
                                    Console.WriteLine("From MySql: " + drow[1].ToString());
                                }
                            }
                        }
                        client.Disconnect();
                    }
                    else
                    {
                        Console.WriteLine("Client cannot be reached...");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            /*
                //vorher Tunnel aufbauen: ssh -L 2222:p602752@p602752.mittwaldserver.info:80 -l p602752 db1325.mydbserver.com
                PasswordConnectionInfo ConInfo = new PasswordConnectionInfo("db1325.mydbserver.com", "p602752", "%eavcUj56ro7");

                var client = new SshClient(ConInfo);
                client.Connect();
                var portfwd = new ForwardedPortLocal(IPAddress.Loopback.ToString(), 2222, "localhost", 3306);
                client.AddForwardedPort(portfwd);
                portfwd.Start();
            }
            */
        }

        /*
        public static (SshClient SshClient, uint Port) ConnectSsh
            (
                string sshHostName,
                string sshUserName,
                string sshPassword = null,
                string sshKeyFile = null,
                string sshPassPhrase = null,
                int sshPort = 22,
                string databaseServer = "localhost",
                int databasePort = 3306
            )
        {
            // check arguments
            if (string.IsNullOrEmpty(sshHostName))
                throw new ArgumentException($"{nameof(sshHostName)} must be specified.", nameof(sshHostName));
            if (string.IsNullOrEmpty(sshHostName))
                throw new ArgumentException($"{nameof(sshUserName)} must be specified.", nameof(sshUserName));
            if (string.IsNullOrEmpty(sshPassword) && string.IsNullOrEmpty(sshKeyFile))
                throw new ArgumentException($"One of {nameof(sshPassword)} and {nameof(sshKeyFile)} must be specified.");
            if (string.IsNullOrEmpty(databaseServer))
                throw new ArgumentException($"{nameof(databaseServer)} must be specified.", nameof(databaseServer));

            // define the authentication methods to use (in order)
            var authenticationMethods = new List<AuthenticationMethod>();
            if (!string.IsNullOrEmpty(sshKeyFile))
            {
                authenticationMethods.Add(new PrivateKeyAuthenticationMethod(sshUserName,
                    new PrivateKeyFile(sshKeyFile, string.IsNullOrEmpty(sshPassPhrase) ? null : sshPassPhrase)));
            }
            if (!string.IsNullOrEmpty(sshPassword))
            {
                authenticationMethods.Add(new PasswordAuthenticationMethod(sshUserName, sshPassword).Dump());
            }

            // connect to the SSH server
            var sshClient = new SshClient(new ConnectionInfo(sshHostName, sshPort, sshUserName, authenticationMethods.ToArray()));
            sshClient.Connect();

            // forward a local port to the database server and port, using the SSH server
            var forwardedPort = new ForwardedPortLocal("127.0.0.1", databaseServer, (uint)databasePort);
            sshClient.AddForwardedPort(forwardedPort);
            forwardedPort.Start();

            return (sshClient, forwardedPort.BoundPort);
        }

        public void Load1()
        {
            var server = "your db & ssh server";
            var sshUserName = "your SSH user name";
            var sshPassword = "your SSH password";
            var databaseUserName = "your database user name";
            var databasePassword = "your database password";

            var (sshClient, localPort) = ConnectSsh(server, sshUserName, sshPassword);
            using (sshClient)
            {
                MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder
                {
                    Server = "127.0.0.1",
                    Port = localPort,
                    UserID = databaseUserName,
                    Password = databasePassword,
                };

                var connection = new MySqlConnection(csb.ConnectionString);
                connection.Open();
            }
        }

        public void Load2()
        {
            var sshServer = "your ssh server";
            var sshUserName = "your SSH user name";
            var sshPassword = "your SSH password";
            var databaseServer = "your database server";
            var databaseUserName = "your database user name";
            var databasePassword = "your database password";

            var (sshClient, localPort) = ConnectSsh(sshServer, sshUserName, sshPassword, databaseServer: databaseServer);
            using (sshClient)
            {
                MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder
                {
                    Server = "127.0.0.1",
                    Port = localPort,
                    UserID = databaseUserName,
                    Password = databasePassword,
                };

                var connection = new MySqlConnection(csb.ConnectionString);
                connection.Open();
            }
        }
        */
    }
}
