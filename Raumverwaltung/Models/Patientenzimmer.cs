namespace Raumverwaltung.Models
{
    public class Patientenzimmer
    {
        private int _ID;
        private int _BettenMaxAnzahl;
        private int _BettenBelegt;
        private bool _Bearbeitet;
        private bool _Added;

        public int pzID { get => _ID; set => _ID = value; }
        public int BettenMaxAnzahl { get => _BettenMaxAnzahl; set => _BettenMaxAnzahl = value; }
        public int BettenBelegt { get => _BettenBelegt; set => _BettenBelegt = value; }
        public bool Bearbeitet { get => _Bearbeitet; set => _Bearbeitet = value; }
        public bool Added { get => _Added; set => _Added = value; }

        public Patientenzimmer()
        {
            pzID = 0;
            BettenMaxAnzahl = 0;
            BettenBelegt = 0;
            Bearbeitet = false;
            Added = false;
        }

        public Patientenzimmer(int iD, int bettenMaxAnzahl, int bettenBelegt, int RaumID, bool bearbeitet = false, bool added = false)
        {
            pzID = iD;
            BettenMaxAnzahl = bettenMaxAnzahl;
            BettenBelegt = bettenBelegt;
            Bearbeitet = bearbeitet;
            Added = added;
        }

        public void incMaxBetten(int beds)
        {
            BettenMaxAnzahl += beds;
        }

        public void decMaxBetten(int beds)
        {
            BettenMaxAnzahl -= beds;
        }

        public void BettBelegen()
        {
            BettenBelegt++;
        }

        public void BettFreigeben()
        {
            BettenBelegt--;
        }

        public bool ZimmerBelegbar()
        {
            if ((BettenMaxAnzahl - BettenBelegt) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}