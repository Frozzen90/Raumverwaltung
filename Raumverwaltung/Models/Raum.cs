namespace Raumverwaltung.Models
{
    public class Raum
    {
        private int _ID;
        private int _ZweckID;           //foreign Key
        private string _ZweckName;
        private bool _Betriebsstatus;   //z.B. außer Betrieb 
        private bool _Bearbeitet;

        public int rID { get => _ID; set => _ID = value; }
        public int ZweckID { get => _ZweckID; set => _ZweckID = value; }
        public string ZweckName { get => _ZweckName; set => _ZweckName = value; }
        public bool Betriebsstatus { get => _Betriebsstatus; set => _Betriebsstatus = value; }
        public bool Bearbeitet { get => _Bearbeitet; set => _Bearbeitet = value; }

        public Raum()
        {
            rID = 0;
            ZweckID = 0;
            ZweckName = "";
            Betriebsstatus = false;
            Bearbeitet = false;
        }

        public Raum(Raum aRaum)
        {
            this.rID = aRaum.rID;
            this.ZweckID = aRaum.ZweckID;
            this.ZweckName = aRaum.ZweckName;
            this.Betriebsstatus = aRaum.Betriebsstatus;
            this.Bearbeitet = aRaum.Bearbeitet;
        }

        public Raum(int iD, int zweckID, string zweckName, bool betriebsstatus, bool bearbeitet = false)
        {
            rID = iD;
            ZweckID = zweckID;
            ZweckName = zweckName;
            Betriebsstatus = betriebsstatus;
            Bearbeitet = bearbeitet;
        }

        private void setRaumID(int ID)
        {
            rID = ID;
        }

        public int getRaumID()
        {
            return rID;
        }

        private void setZweckID(int ID)
        {
            ZweckID = ID;
        }

        public int getZweckID()
        {
            return ZweckID;
        }

        private void setZweckName(string Name)
        {
            ZweckName = Name;
        }

        public string getZweckName()
        {
            return ZweckName;
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