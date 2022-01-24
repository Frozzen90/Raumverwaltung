﻿namespace Raumverwaltung.Models
{
    public class Raum
    {
        private int _ID;
        private int _ZweckID;           //foreign Key
        private bool _AußerBetrieb;     //außer Betrieb = True; in Betrieb = false;
        private bool _Bearbeitet;
        private bool _Added;

        public int rID { get => _ID; set => _ID = value; }
        public int ZweckID { get => _ZweckID; set => _ZweckID = value; }
        public bool AußerBetrieb { get => _AußerBetrieb; set => _AußerBetrieb = value; }
        public bool Bearbeitet { get => _Bearbeitet; set => _Bearbeitet = value; }
        public bool Added { get => _Added; set => _Added = value; }

        public Raum()
        {
            rID = 0;
            ZweckID = 0;
            AußerBetrieb = false;
            Bearbeitet = false;
            Added = false;
        }

        public Raum(Raum aRaum)
        {
            this.rID = aRaum.rID;
            this.ZweckID = aRaum.ZweckID;
            this.AußerBetrieb = aRaum.AußerBetrieb;
            this.Bearbeitet = aRaum.Bearbeitet;
            this.Added = aRaum.Added;
        }

        public Raum(int iD, int zweckID, bool betriebsstatus, bool bearbeitet = false, bool added = false)
        {
            rID = iD;
            ZweckID = zweckID;
            AußerBetrieb = betriebsstatus;
            Bearbeitet = bearbeitet;
            Added = added;
        }
    }
}