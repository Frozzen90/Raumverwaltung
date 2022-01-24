using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raumverwaltung.Models
{
    public class RaumZweck
    {
        private int _ID;
        private string _Bezeichnung;
        private bool _Added;
        private bool _Bearbeitet;

        public int ID { get => _ID; set => _ID = value; }
        public string Bezeichnung { get => _Bezeichnung; set => _Bezeichnung = value; }
        public bool Added { get => _Added; set => _Added = value; }
        public bool Bearbeitet { get => _Bearbeitet; set => _Bearbeitet = value; }

        public RaumZweck()
        {
            ID = 0;
            Bezeichnung = "";
            Added = false;
            Bearbeitet = false;
        }

        public RaumZweck(int iD, string bezeichnung, bool added = false, bool bearbeitet = false)
        {
            ID = iD;
            Bezeichnung = bezeichnung;
            Added = added;
            Bearbeitet = bearbeitet;
        }
    }
}