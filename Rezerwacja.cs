using System;
using System.Collections.Generic;
namespace LiniaLotnicza
{
    public class Rezerwacja
    {
        private Klient klient;
        private List<Bilet> Bilety;
        public Rezerwacja() { }

        //Zwykly konstruktor
        public Rezerwacja(Klient k)
        {
            this.klient = k;
            this.Bilety = new List<Bilet>();
        }

        public Klient getKlient()
        {
            return klient;
        }
        public List<Bilet> getBilety()
        {
            return Bilety;
        }
        public void dodajBilet(Bilet b)
        {
            Bilety.Add(b);
        }
        public void usunBilet(Bilet b)
        {
            //Metoda przeglada cala liste biletow i porownuje pola obiektow poprzez metode porownajBilet, a nastepnie usuwa poszczegolne bilety.
            for (int i = 0; i < this.Bilety.Count; i++)
            {
                if (b.Equals(this.Bilety[i]))
                    Bilety.RemoveAt(i);
            }
        }

        // Metoda Equals porownuje poszczegolne pola i wywoluje prywatna metode porownajBilety ktora porownuje listy biletow i zwraca true jezeli listy sa takie same
        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Rezerwacja r = (Rezerwacja)obj;
                List<Bilet> b = r.getBilety();     // Sprawdzenie czy listy
                if (b.Count != this.Bilety.Count)     // posiadaja taka sama
                    return false;                       // liczbe elementow.
                if (this.klient.Equals(r.getKlient()) && porownajBilety(r))
                    return true;
                else
                    return false;
            }
        }
        private bool porownajBilety(Rezerwacja r)
        {
            List<Bilet> b = r.getBilety();
            bool zmienna = false;
            for (int i = 0; i < b.Count; i++)
            {
                if (b[i].Equals(this.Bilety[i]))
                        zmienna = true;
                else
                    return false;
            }
            return zmienna;
        }
    }
    public class Bilet
        {
            private string MiejsceDocelowe;
            private DateTime dataPocz;
            private DateTime dataKon;
            public Bilet() { }
            public Bilet(string MDocelowe, int r1, int m1, int d1, int g1, int mi1,int r2,int m2, int d2, int g2, int mi2)
            {
                this.MiejsceDocelowe = MDocelowe;
                this.dataPocz = new DateTime(r1, m1, d1, g1, m1, 0);
                this.dataKon = new DateTime(r2, m2, d2, g2, m2, 0);
            }
            public Bilet(string MDocelowe, DateTime data1,DateTime data2)
            {
                this.MiejsceDocelowe = MDocelowe;
                this.dataPocz = data1;
                this.dataKon = data2;
            }
            public string getMiejsceDocelowe()
            {
                return this.MiejsceDocelowe;
            }
            public DateTime getDataPocz()
            {
                return this.dataPocz;
            }
            public DateTime getDataKon()
            {
                return this.dataKon;
            }
            public override bool Equals(Object obj)
            {
                if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                {
                    return false;
                }
                else
                {
                    Bilet bil = (Bilet)obj;
                    return (this.dataPocz == bil.getDataPocz() && this.dataKon == bil.getDataKon() && this.MiejsceDocelowe == bil.getMiejsceDocelowe());
                }

            }
        }
}