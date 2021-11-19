using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;

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

        #region SSH/MySQL
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
        #endregion

        #region SQLite
        private void CreateTestDB(string path)
        {
            SQLiteConnection.CreateFile(path);

            //***** Tabellen erstellen
            string conStr = $"Data Source='{path}';";
            List<string> querryStr = new List<string>();
            querryStr.Add($"CREATE TABLE Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            querryStr.Add($"CREATE TABLE Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            querryStr.Add($"CREATE TABLE Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            querryStr.Add($"CREATE TABLE Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            querryStr.Add($"CREATE TABLE Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            querryStr.Add($"CREATE TABLE Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            querryStr.Add($"CREATE TABLE Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            querryStr.Add($"CREATE TABLE Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            querryStr.Add($"CREATE TABLE Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            querryStr.Add($"CREATE TABLE Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            querryStr.Add($"CREATE TABLE Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            SQLiteConnection con = new SQLiteConnection(conStr);
            try
            {
                con.Open();
                for (int I = 0; I < querryStr.Count; I++)
                {
                    SQLiteCommand com = new SQLiteCommand(querryStr[I], con);
                    com.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            //*****
        }

        private void TestdatenAnlegen(string path)
        {
            string conStr = $"Data Source='{path}';";
            List<string> querryStr = new List<string>();
            #region Testdaten Patientenzimmer
            querryStr.Add($"INSERT INTO Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            querryStr.Add($"INSERT INTO Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            querryStr.Add($"INSERT INTO Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            #endregion
            #region Testdaten Raum
            querryStr.Add($"INSERT INTO Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            querryStr.Add($"INSERT INTO Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            querryStr.Add($"INSERT INTO Patientenzimmer(" +
                          $"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          $" , " +
                          $" , " +
                          $" );");
            #endregion
            #region Testdaten 
            #endregion
            #region Testdaten 
            #endregion
            #region Testdaten 
            #endregion

            SQLiteConnection con = new SQLiteConnection(conStr);
            try
            {
                con.Open();
                for (int I = 0; I < querryStr.Count; I++)
                {
                    SQLiteCommand com = new SQLiteCommand(querryStr[I], con);
                    com.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            //*****

        }

        private void ConnectToTestDB(string SqlStr)
        {
            string DBname = "LocalTestDB.db";
            string PathToDB = AppDomain.CurrentDomain.BaseDirectory + DBname;

            if (!File.Exists(PathToDB))
            {
                CreateTestDB(PathToDB);
            }

            SqlCommand Com = new SqlCommand();
            Com.Connection = new SqlConnection($"Data Source='{PathToDB}';"); //;Version=3;";
            Com.CommandText = SqlStr;
        }
        #endregion

        public void SQLRequest(string SqlStr)
        {
            bool ConnetionFailed = false;
            //***** Zentraldatenbank öffnen
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
                ConnetionFailed = true;
            }
            finally
            {
                DisconnectSSH();
            }
            //*****
            //***** Testdatenbank öffnen
            if (ConnetionFailed)  
            {
                ConnetionFailed = false;
                try
                {
                    ConnectToTestDB(SqlStr);
                }
                catch (Exception)
                {
                    ConnetionFailed = true;
                }
                finally
                {

                }
            }
            //*****
            if (ConnetionFailed)
            {
                //Meldung ausgeben, dass keine DB erreichbar ist.
            }
        }
    }
}