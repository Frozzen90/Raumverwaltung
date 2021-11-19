namespace Raumverwaltung.Models
{
    public class Raum
    {
        private int _ID;
        private int _ZweckID;           //foreign Key
        private string _ZweckName;
        private bool _Betriebsstatus;   //z.B. außer Betrieb 

        public int rID { get => _ID; set => _ID = value; }
        public int ZweckID { get => _ZweckID; set => _ZweckID = value; }
        public string ZweckName { get => _ZweckName; set => _ZweckName = value; }
        public bool Betriebsstatus { get => _Betriebsstatus; set => _Betriebsstatus = value; }

        public Raum()
        {
            rID = 0;
            ZweckID = 0;
            ZweckName = "";
            Betriebsstatus = false;
        }

        public Raum(Raum aRaum)
        {
            this.rID = aRaum.rID;
            this.ZweckID = aRaum.ZweckID;
            this.ZweckName = aRaum.ZweckName;
            this.Betriebsstatus = aRaum.Betriebsstatus;
        }

        public Raum(int iD, int zweckID, string zweckName, bool betriebsstatus)
        {
            rID = iD;
            ZweckID = zweckID;
            ZweckName = zweckName;
            Betriebsstatus = betriebsstatus;
        }

        public void setBetriebsstatus(bool ausserBetrieb)
        {
            Betriebsstatus = ausserBetrieb;
        }

        public bool getBetriebsstatus()
        {
            return Betriebsstatus;
        }
    }
}