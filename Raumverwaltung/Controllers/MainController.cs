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
        private List<RaumZweck> _RaumZwecks;

        private ServerController SrvCntr { get => _SrvCntr; set => _SrvCntr = value; }
        public List<Patientenzimmer> Patientenzimmers { get => _Patientenzimmers; set => _Patientenzimmers = value; }
        public List<Raum> Raeume { get => _Raeume; set => _Raeume = value; }
        public List<RaumZweck> RaumZwecks { get => _RaumZwecks; set => _RaumZwecks = value; }

        public MainController()
        {
            SrvCntr = new ServerController();
            Patientenzimmers = null;
            Raeume = null;
            RaumZwecks = null;
        }

        public void LadeDaten(bool refresh)
        {
            if (SrvCntr.TryConnectToMySql())
            {
                Raeume = SrvCntr.LoadRaeumeFromDb();
                Patientenzimmers = SrvCntr.LoadPatientenzimmerFromDb();
                RaumZwecks = SrvCntr.LoadRaumZweckeFromDb();
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