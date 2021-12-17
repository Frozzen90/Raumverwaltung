using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Raumverwaltung.Models;

namespace Raumverwaltung.Controllers
{
    public class MainController
    {
        private ServerController _SrvCntr;
        private List<Patientenzimmer> _Patientenzimmers;
        private List<Raum> _Raeume;

        private ServerController SrvCntr { get => _SrvCntr; set => _SrvCntr = value; }
        public List<Patientenzimmer> Patientenzimmers { get => _Patientenzimmers; set => _Patientenzimmers = value; }
        public List<Raum> Raeume { get => _Raeume; set => _Raeume = value; }

        public MainController()
        {
            SrvCntr = new ServerController();
            Patientenzimmers = null;
            Raeume = null;
        }

        public void SetPatientenzimmers(List<Patientenzimmer> pzL)
        {
            Patientenzimmers = pzL;
        }

        public List<Patientenzimmer> GetPatientenzimmers()
        {
            return Patientenzimmers;
        }

        public void AddPatientenzimmer(Patientenzimmer pz)
        {
            Patientenzimmers.Add(pz);
        }

        public void SetRaeume(List<Raum> r)
        {
            Raeume = r;
        }

        public List<Raum> GetRaeume()
        {
            return Raeume;
        }

        public void AddRaum()
        {

        }

        public bool LoescheDaten(int ID)
        {
            bool lDeleted = false;

            return lDeleted;
        }

        public void LadeDaten(bool refresh)
        {
            if (SrvCntr.TryConnectToMySql())
            {
                Raeume = SrvCntr.LoadRaeumeFromDb();
                Patientenzimmers = SrvCntr.LoadPatientenzimmerFromDb();
            }
            else if (SrvCntr.TryConnectToTestDB())
            {
                Raeume = SrvCntr.LoadRaeumeFromTestDb();
                Patientenzimmers = SrvCntr.LoadPatientenzimmerFromTestDb();
            }
            else
            {
                //Fehlermeldung ausgeben, dass keine DB erreichbar ist.
            }
        }

        public void LogFile(string logText)
        {
            logText = DateTime.Now.ToString() + ": " + logText + "; \n";
            string pathToLogFile = AppDomain.CurrentDomain.BaseDirectory + "LogFile.txt";
            File.AppendAllText(pathToLogFile, logText, Encoding.UTF8);
        }
    }
}