using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raumverwaltung.Models
{
    public class Patientenzimmer : Raum
    {
        private int _ID;
        private int _BettenMaxAnzahl;
        private int _BettenBelegt;

        public int pzID { get => _ID; set => _ID = value; }
        public int BettenMaxAnzahl { get => _BettenMaxAnzahl; set => _BettenMaxAnzahl = value; }
        public int BettenBelegt { get => _BettenBelegt; set => _BettenBelegt = value; }

        public Patientenzimmer() : base()
        {
            pzID = 0;
            BettenMaxAnzahl = 0;
            BettenBelegt = 0;
        }

        public Patientenzimmer(Raum aRaum) : base(aRaum)
        {
            pzID = 0;
            BettenMaxAnzahl = 0;
            BettenBelegt = 0;
        }

        public Patientenzimmer(int iD, int bettenMaxAnzahl, int bettenBelegt, Raum aRaum) : base(aRaum)
        {
            pzID = iD;
            BettenMaxAnzahl = bettenMaxAnzahl;
            BettenBelegt = bettenBelegt;
        }

        public Patientenzimmer(int iD, int bettenMaxAnzahl, int bettenBelegt, int RaumID, int zweckID, string zweckName, bool betriebsstatus) 
            : base (RaumID, zweckID, zweckName, betriebsstatus)
        {
            pzID = iD;
            BettenMaxAnzahl = bettenMaxAnzahl;
            BettenBelegt = bettenBelegt;
        }

        public void incMaxBetten(int beds)
        {
            BettenMaxAnzahl += beds;
        }

        public void decMaxBetten(int beds)
        {
            BettenMaxAnzahl -= beds;
        }

        public void setMaxBetten(int Anzahl)
        {
            BettenMaxAnzahl = Anzahl;
        }

        public int getMaxBetten()
        {
            return BettenMaxAnzahl;
        }

        public int getFreieBetten()
        {
            return BettenMaxAnzahl - BettenBelegt;
        }

        public void BettBelegen()
        {
            BettenBelegt++;
        }

        public void BettFreigeben()
        {
            BettenBelegt--;
        }
    }
}