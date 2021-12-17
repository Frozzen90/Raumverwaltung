using MySql.Data.MySqlClient;
using Raumverwaltung.Models;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;

namespace Raumverwaltung.Controllers
{
    public class ServerController
    {
        private MySqlCommand _Com;
        private SqlCommand _TestCom;
        private bool _isTestDB;

        public MySqlCommand Com { get => _Com; set => _Com = value; }
        public SqlCommand TestCom { get => _TestCom; set => _TestCom = value; }
        public bool IsTestDB { get => _isTestDB; set => _isTestDB = value; }
        

        public ServerController()
        {
            Com = new MySqlCommand();
            TestCom = new SqlCommand();
            IsTestDB = false;
        }


        public bool TryConnectToMySql()
        {
            bool ret = false;
            string Server = "192.168.14.74";
            string Port = "80";
            string UserID = "sebastian";
            string dings = "BurgApfel";
            string DbName = "central_db";
            string MySqlStr = $"SERVER={Server};PORT={Port};UID={UserID};PASSWORD={dings};DATABASE={DbName}";
            Com.Connection = new MySqlConnection(MySqlStr);
            try
            {
                Com.Connection.Open();
                Com.Connection.Close();
                ret = true;
            }
            catch
            {
                ret = false;
            }
            finally
            {

            }
            return ret;
        }

        #region SQLite
        public bool TryConnectToTestDB()
        {
            bool ret = false;
            string DBname = "LocalTestDB.db";
            string PathToDB = AppDomain.CurrentDomain.BaseDirectory + DBname;

            if (!File.Exists(PathToDB))
            {
                File.Create(PathToDB);
            }

            if (File.Exists(PathToDB))
            {
                TestCom = new SqlCommand();
                TestCom.Connection = new SqlConnection($"Data Source='{PathToDB}';"); //;Version=3;";
                ret = true;
            }
            else
            {
                Global.cMainController.LogFile("TestDatenbank konnte nicht angelegt werden!");
                ret = false;
            }
            return ret;
        }
        #endregion

        private int GetSqlId(int ID)
        {
            int SqlId = -1;
            
            return SqlId;
        }

        public void LoescheDaten(int ID)
        {
            ID = GetSqlId(ID);
        }

        public List<Raum> LoadRaeumeFromDb()
        {
            List<Raum> RaumListe = new List<Raum>();
            try
            {
                Com.CommandText =
                    "SELECT " +
                    "   R.ID AS rID, " +
                    "   Z.ID, " +
                    "   Z.Zweck, " +
                    "   R.Außerbetrieb " +
                    "FROM " +
                    "   Raum AS R " +
                    "INNER JOIN " +
                    "   Zweck_Raum AS Z " +
                    "ON " +
                    "   R.ID_Zweck = Z.ID";
                Com.Connection.Open();
                MySqlDataReader SqlDR = Com.ExecuteReader();
                while (SqlDR.Read())
                {
                    Raum aRaum = new Raum();
                    aRaum.rID = Int16.Parse(SqlDR["rID"].ToString());
                    aRaum.Betriebsstatus = Boolean.Parse(SqlDR["ID"].ToString());
                    aRaum.ZweckID = Int16.Parse(SqlDR["zID"].ToString());
                    aRaum.ZweckName = SqlDR["ID"].ToString();
                    RaumListe.Add(aRaum);
                }
            }
            catch (Exception e)
            {
                RaumListe = null;
                Global.cMainController.LogFile(e.Message);
            }
            finally
            {
                Com.Connection.Close();
            }
            return RaumListe;
        }

        public List<Raum> LoadRaeumeFromTestDb()
        {
            List<Raum> RaumListe = new List<Raum>();
            try
            {
                Com.CommandText =
                    "SELECT " +
                    "   R.ID AS rID, " +
                    "   Z.ID, " +
                    "   Z.Zweck, " +
                    "   R.Außerbetrieb " +
                    "FROM " +
                    "   Raum AS R " +
                    "INNER JOIN " +
                    "   Zweck_Raum AS Z " +
                    "ON " +
                    "   R.ID_Zweck = Z.ID";
                Com.Connection.Open();
                MySqlDataReader SqlDR = Com.ExecuteReader();
                while (SqlDR.Read())
                {
                    Raum aRaum = new Raum();
                    aRaum.rID = Int16.Parse(SqlDR["rID"].ToString());
                    aRaum.Betriebsstatus = Boolean.Parse(SqlDR["ID"].ToString());
                    aRaum.ZweckID = Int16.Parse(SqlDR["zID"].ToString());
                    aRaum.ZweckName = SqlDR["ID"].ToString();
                    RaumListe.Add(aRaum);
                }
            }
            catch (Exception e)
            {
                RaumListe = null;
                Global.cMainController.LogFile(e.Message);
            }
            finally
            {
                Com.Connection.Close();
            }
            return RaumListe;
        }

        public List<Patientenzimmer> LoadPatientenzimmerFromDb()
        {
            List<Patientenzimmer> PzListe = new List<Patientenzimmer>();
            try
            {
                Com.CommandText =
                    "SELECT " +
                    "   P.ID AS pID, " +
                    "   P.Plätze AS BettenMax, " +
                    "   P.DavonBelegt AS BettenBelegt" +
                    "FROM " +
                    "   Patientenzimmer AS P ";
                Com.Connection.Open();
                MySqlDataReader SqlDR = Com.ExecuteReader();
                while (SqlDR.Read())
                {
                    Patientenzimmer aPz = new Patientenzimmer();
                    aPz.pzID = Int16.Parse(SqlDR["pID"].ToString());
                    aPz.BettenBelegt = Int16.Parse(SqlDR["BettenMax"].ToString());
                    aPz.BettenMaxAnzahl = Int16.Parse(SqlDR["BettenBelegt"].ToString());
                    PzListe.Add(aPz);
                }
            }
            catch (Exception e)
            {
                PzListe = null;
                Global.cMainController.LogFile(e.Message);
            }
            finally
            {

            }
            return PzListe;
        }

        public List<Patientenzimmer> LoadPatientenzimmerFromTestDb()
        {
            List<Patientenzimmer> PzListe = new List<Patientenzimmer>();
            try
            {
                Com.CommandText =
                    "SELECT " +
                    "   P.ID AS pID, " +
                    "   P.Plätze AS BettenMax, " +
                    "   P.DavonBelegt AS BettenBelegt" +
                    "FROM " +
                    "   Patientenzimmer AS P ";
                Com.Connection.Open();
                MySqlDataReader SqlDR = Com.ExecuteReader();
                while (SqlDR.Read())
                {
                    Patientenzimmer aPz = new Patientenzimmer();
                    aPz.pzID = Int16.Parse(SqlDR["pID"].ToString());
                    aPz.BettenBelegt = Int16.Parse(SqlDR["BettenMax"].ToString());
                    aPz.BettenMaxAnzahl = Int16.Parse(SqlDR["BettenBelegt"].ToString());
                    PzListe.Add(aPz);
                }
            }
            catch (Exception e)
            {
                PzListe = null;
                Global.cMainController.LogFile(e.Message);
            }
            finally
            {

            }
            return PzListe;
        }

        public void SaveToDb()
        {

        }
    }
}