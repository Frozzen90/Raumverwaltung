using MySql.Data.MySqlClient;
using Raumverwaltung.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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

        public static string ConnectionString()
        {
//            string Server = "10.91.56.6"; // Thomas Schul-PC
            string Server = "192.168.14.78"; // Patrick Laptop
            string Port = "3306";
            string UserID = "sebastian";
            string dings = "BurgApfel";
            string DbName = "central_db";
            return $"SERVER={Server};PORT={Port};UID={UserID};PASSWORD={dings};DATABASE={DbName}";
        }

        public bool TryConnectToMySql()
        {
            bool ret = false;
            string MySqlStr = ConnectionString();
            Com.Connection = new MySqlConnection(MySqlStr);
            try
            {
                Com.Connection.Open();
                Com.Connection.Close();
                ret = true;
            }
            catch (Exception e)
            {
                Global.cMainController.LogFile(e.Message);
            }
            return ret;
        }

        public bool TryConnectToMySql(string Server, string Port, string UserID, string Passwort)
        {
            bool ret = false;
            string DbName = "central_db";
            string MySqlStr = $"SERVER={Server};PORT={Port};UID={UserID};PASSWORD={Passwort};DATABASE={DbName}";
            Com.Connection = new MySqlConnection(MySqlStr);
            try
            {
                Com.Connection.Open();
                Com.Connection.Close();
                ret = true;
            }
            catch (Exception e)
            {
                Global.cMainController.LogFile(e.Message);
            }
            return ret;
        }

        #region MySQLDB
        private void SavingQueryToDB(List<string> Querys)
        {
            Com.Connection.Open();
            try
            { 
                foreach (string Query in Querys)
                {
                    Com.CommandText = Query;
                    Com.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Global.cMainController.LogFile(e.Message);
            }
            finally
            {
                Com.Connection.Close();
            }
        }

        #region Raeume
        public List<Raum> LoadRaeumeFromDb()
        {
            List<Raum> RaumListe = new List<Raum>();
            try
            {
                Com.CommandText =
                    "SELECT " +
                    "   R.ID_RaumNummer AS rID, " +
                    "   Z.ID AS zID, " +
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
                    aRaum.AußerBetrieb = Boolean.Parse(SqlDR["Außerbetrieb"].ToString());
                    aRaum.ZweckID = Int16.Parse(SqlDR["zID"].ToString());
                    aRaum.ZweckName = SqlDR["Zweck"].ToString();
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

        public void SaveRaeumeToDb()
        {
            List<string> SQLQuerys = new List<string>();
            foreach (Raum R in Global.cMainController.Raeume)
            {
                if (R.Added)
                {
                    string RaumNummer = "";
                    string RaumNummerWert = "";
                    if (R.rID > 0)
                    {
                        RaumNummer = "ID_RaumNummer, ";
                        RaumNummerWert = $"{R.rID.ToString()}, ";
                    }
                    else
                    {
                        //nichts
                    }

                    SQLQuerys.Add(
                        $"INSERT INTO Raum ({RaumNummer} ID_Zweck, Außerbetrieb) " +
                        $"VALUES {RaumNummerWert} {R.ZweckID}, {R.AußerBetrieb} "
                    );
                    R.Added = false;
                }
                if (R.Bearbeitet)
                {
                    SQLQuerys.Add(
                        $"UPDATE Raum " +
                        $"SET ID_Zweck = {R.ZweckID}, Außerbetrieb = {R.AußerBetrieb} " +
                        $"WHERE ID_RaumNummer = {R.rID} "
                    );
                    R.Bearbeitet = false;
                }
            }
            SavingQueryToDB(SQLQuerys);
            SQLQuerys.Clear();
        }
        #endregion

        #region Patientenzimmer
        public List<Patientenzimmer> LoadPatientenzimmerFromDb()
        {
            List<Patientenzimmer> PzListe = new List<Patientenzimmer>();
            try
            {
                Com.CommandText =
                    "SELECT " +
                    "   p.ID_RaumNummer AS pID, " +
                    "   p.Plätze AS BettenMax, " +
                    "   p.DavonBelegt AS BettenBelegt " +
                    "FROM " +
                    "   Patientenzimmer AS p ";
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
                Com.Connection.Close();
            }
            return PzListe;
        }

        public void SavePatientenzimmerToDb()
        {
            List<string> SQLQuerys = new List<string>();
            foreach (Patientenzimmer P in Global.cMainController.Patientenzimmers)
            {
                if (P.Added)
                {
                    string RaumNummer = "";
                    string RaumNummerWert = "";
                    if (P.pzID > 0)
                    {
                        RaumNummer = "ID_RaumNummer, ";
                        RaumNummerWert = $"{P.pzID.ToString()}, ";
                    }
                    else
                    {
                        //nichts
                    }

                    SQLQuerys.Add(
                        $"INSERT INTO Patientenzimmer ({RaumNummer} Plätze, DavonBelegt) " +
                        $"VALUES ({RaumNummerWert} {P.BettenMaxAnzahl}, {P.BettenBelegt}"
                    );
                    P.Added = false;
                }
                if (P.Bearbeitet)
                {
                    SQLQuerys.Add(
                        $"UPDATE Patientenzimmer " +
                        $"SET Plätze = {P.BettenMaxAnzahl}, DavonBelegt = {P.BettenBelegt} " +
                        $"WHERE ID_RaumNummer = {P.pzID} "
                    );
                    P.Bearbeitet = false;
                }
            }
            SavingQueryToDB(SQLQuerys);
            SQLQuerys.Clear();
        }
        #endregion
        
        #region RaumZwecke
        public List<RaumZweck> LoadRaumZweckeFromDb()
        {
            List<RaumZweck> ZweckListe = new List<RaumZweck>();
            try
            {
                Com.CommandText =
                    "SELECT " +
                    "   ID, " +
                    "   Zweck " +
                    "FROM " +
                    "   Zweck_Raum ";
                Com.Connection.Open();
                MySqlDataReader SqlDR = Com.ExecuteReader();
                while (SqlDR.Read())
                {
                    RaumZweck aZweck = new RaumZweck();
                    aZweck.ID = Int16.Parse(SqlDR["ID"].ToString());
                    aZweck.Bezeichnung = SqlDR["ID"].ToString();
                    ZweckListe.Add(aZweck);
                }
            }
            catch (Exception e)
            {
                ZweckListe = null;
                Global.cMainController.LogFile(e.Message);
            }
            finally
            {
                Com.Connection.Close();
            }
            return ZweckListe;
        }

        public void SaveZweckToDb()
        {
            List<string> SQLQuerys = new List<string>();
            foreach (RaumZweck Z in Global.cMainController.RaumZwecks)
            {
                if (Z.Added)
                {
                    string ZweckNummer = "";
                    string ZweckNummerWert = "";
                    if (Z.ID > 0)
                    {
                        ZweckNummer = "ID, ";
                        ZweckNummerWert = $"\"{Z.ID.ToString()}, \"";
                    }
                    else
                    {
                        //nichts
                    }

                    SQLQuerys.Add(
                        $"INSERT INTO Patientenzimmer ({ZweckNummer} DavonBelegt) " +
                        $"VALUES ({ZweckNummerWert} {Z.Bezeichnung}) "
                    );
                    Z.Added = false;
                }
                if (Z.Bearbeitet)
                {
                    SQLQuerys.Add(
                        $"UPDATE Patientenzimmer " +
                        $"SET Zweck = \"{Z.Bezeichnung}\" " +
                        $"WHERE ID_RaumNummer = {Z.ID} "
                    );
                    Z.Bearbeitet = false;
                }
            }
            SavingQueryToDB(SQLQuerys);
            SQLQuerys.Clear();
        }
        #endregion
        #endregion
    }
}