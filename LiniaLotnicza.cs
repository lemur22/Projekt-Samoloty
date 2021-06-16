using System;
using System.Collections.Generic;
namespace LiniaLotnicza
{
	public class LiniaLotnicza
	{
		private string nazwaLinii;
		private List<Samolot> samoloty;
		private List<Lot> loty;
		private List<Trasa> trasy;
		private List<Klient> klienci;
		private List<Lotnisko> lotniska;

		//Konstruktor
		public LiniaLotnicza(string nazwa)
		{
			this.nazwaLinii = nazwa;
			samoloty = new List<Samolot>();
			loty = new List<Lot>();
			trasy = new List<Trasa>();
			klienci = new List<Klient>();
			lotniska = new List<Lotnisko>();
		}

		//gettery
		public string getNazwaLinii() { return this.nazwaLinii; }
		public List<Lot> getLoty() { return this.loty; }
		public List<Samolot> getSamoloty() { return this.samoloty; }
		public List<Trasa> getTrasy() { return this.trasy; }
		public List<Klient> getKlient() { return this.klienci; }
		public List<Lotnisko> getLotniska() { return this.lotniska; }

		//dodawanie do list
		public void dodajSamolot(Samolot s)
		{
			for (int i = 0; i < samoloty.Count; i++)
			{
				if (s.Equals(samoloty[i]))
					throw new ListaException("Wybrany samolot zostal juz dodany.");
			}
			samoloty.Add(s);
		}

		public void dodajKlienta(Klient k)
		{
			for (int i = 0; i < klienci.Count; i++)
			{
				if (k.Equals(klienci[i]))
					throw new ListaException("Wybrany klient zostal juz dodany.");
			}
			klienci.Add(k);
		}
		public void dodajTrase(Trasa t)
		{
			for (int i = 0; i < trasy.Count; i++)
			{
				if (t.Equals(trasy[i]))
					throw new ListaException("Wybrana trasa zostala juz dodana.");
			}
			trasy.Add(t);
		}
		public void dodajLot(Lot l)
		{
			for (int i = 0; i < loty.Count; i++)
			{
				if (l.Equals(loty[i]))
					throw new ListaException("Wybrany lot zostal juz dodany.");
			}
			loty.Add(l);
		}

		public void dodajLotnisko(Lotnisko l)
		{
			for (int i = 0; i < lotniska.Count; i++)
			{
				if (l.Equals(lotniska[i]))
					throw new ListaException("Wybrane lotnisko zostalo juz dodane.");
			}
			lotniska.Add(l);
		}

		//Usuwanie Samolotu z listy
		public void usunSamolot(Samolot s)
		{
			//Metoda porownanieSamolotSamoloty sprawdza czy dany samolot wystepuje na liscie samolotow.
			//Metoda zwraca true jezeli samolot wystepuje oraz false gdy nie wystepuje.
			if (!porownanieSamolotSamoloty(s))
				throw new UsunException("Dany samolot nie wystepuje na liscie.");

			//Metoda porownanieSamolotLoty sprawdza czy dany samolot posiada przypisane pewne loty.
			//Metoda zwraca true jezeli samolot posiada przypisane loty.
			if (porownanieSamolotLoty(s))
				throw new UsunException("Dany samolot ma przypisane loty. Nie mozna usunac.");
			samoloty.Remove(s);
		}
		private bool porownanieSamolotSamoloty(Samolot s)
		{
			for (int i = 0; i < this.samoloty.Count; i++)
			{
				if (s.Equals(samoloty[i]))
				{
					return true;
				}
			}
			return false;
		}

		private bool porownanieSamolotLoty(Samolot s)
		{
			for (int i = 0; i < this.loty.Count; i++)
			{
				if (s.Equals(loty[i].getSamolot()))
				{
					return true;
				}
			}
			return false;
		}

		//Usuwanie trasy z listy
		public void usunTrase(Trasa t)
		{
			//Metoda porownanieTrasaTrasy sprawdza czy dana trasa wystepuje na liscie tras.
			//Metoda zwraca true jezeli trasa wystepuje oraz false gdy nie wystepuje.
			if (!porownanieTrasaTrasy(t))
				throw new UsunException("Dana trasa nie wystepuje na liscie.");

			//Metoda porownanieTrasaLoty sprawdza czy dana trasa posiada przypisane pewne loty.
			//Metoda zwraca true jezeli trasa posiada przypisane loty.
			if (porownanieTrasaLoty(t))
				throw new UsunException("Dana trasa ma przypisane loty. Nie mozna usunac.");
			trasy.Remove(t);
		}
		private bool porownanieTrasaTrasy(Trasa t)
		{
			for (int i = 0; i < this.trasy.Count; i++)
			{
				if (t.Equals(trasy[i]))
				{
					return true;
				}
			}
			return false;
		}

		private bool porownanieTrasaLoty(Trasa t)
		{
			for (int i = 0; i < this.loty.Count; i++)
			{
				if (t.Equals(loty[i].getTrasa()))
				{
					return true;
				}
			}
			return false;
		}

		//Usuwanie klienta z listy
		public void usunKlienta(Klient k)
		{
			//Metoda porownanieKlientKlienci sprawdza czy dany klient wystepuje na liscie klientow.
			//Metoda zwraca true jezeli klient wystepuje oraz false gdy nie wystepuje.
			if (!porownanieKlientKlienci(k))
				throw new UsunException("Dany klient nie wystepuje na liscie.");
			//Metoda porownanieKlientRezerwacje sprawdza czy dany klient posiada rezerwacje dla wszystkich lotow.
			//Metoda zwraca true jezeli klient posiada rezerwacje oraz false jezeli ich nie posiada.
			if (porownanieKlientRezerwacje(k))
				throw new UsunException("Dany klient posiada rezerwacje. Nie mozna usunac.");
			klienci.Remove(k);

		}

		private bool porownanieKlientKlienci(Klient k)
		{
			for (int i = 0; i < this.klienci.Count; i++)
			{
				if (k.Equals(klienci[i]))
				{
					return true;
				}
			}
			return false;
		}

		private bool porownanieKlientRezerwacje(Klient k)
		{
			for (int i = 0; i < this.loty.Count; i++)
			{
				List<Rezerwacja> rezerwacje = loty[i].getRezerwacje();
				for (int j = 0; j < rezerwacje.Count; j++)
				{
					if (k.Equals(rezerwacje[j].getKlient()))
					{
						return true;
					}
				}
			}
			return false;
		}
		//Usuwanie lotow
		public void usunLot(Lot l)
		{
			//Metoda porownanieLotLoty sprawdza czy dany loy wystepuje na liscie lotow.
			//Metoda zwraca true jezeli lot wystepuje oraz false gdy nie wystepuje.
			if (!porownanieLotLoty(l))
				throw new UsunException("Dany lot nie wystepuje na liscie.");
			if (l.getRezerwacje().Count == 0)
				loty.Remove(l);
			else
				throw new UsunException("Dany lot posiada przypisane rezerwacje. Nie mozna usunac.");
		}
		private bool porownanieLotLoty(Lot l)
		{
			for (int i = 0; i < this.loty.Count; i++)
			{
				if (l.Equals(loty[i]))
				{
					return true;
				}
			}
			return false;
		}

		//Usuwanie lotnisk
		public void usunLotnisko(Lotnisko l)
		{
			//Metoda porownanieLotniskoLotniska sprawdza czy dane lotnisko wystepuje na liscie lotnisk.
			//Metoda zwraca true jezeli lotnisko wystepuje oraz false gdy nie wystepuje.
			if (!porownanieLotniskoLotniska(l))
				throw new UsunException("Dane lotnisko nie wystepuje na liscie.");

			//Metoda porownanieLotniskoTrasy sprawdza czy dane lotnisko posiada przypisane pewne trasy.
			//Metoda zwraca true jezeli lotnisko posiada przypisane trasy.
			if (porownanieLotniskoTrasy(l))
				throw new UsunException("Dane lotnisko ma przypisane trasy. Nie mozna usunac.");
			lotniska.Remove(l);
		}
		private bool porownanieLotniskoLotniska(Lotnisko l)
		{
			for (int i = 0; i < this.lotniska.Count; i++)
			{
				if (l.Equals(lotniska[i]))
				{
					return true;
				}
			}
			return false;
		}

		private bool porownanieLotniskoTrasy(Lotnisko l)
		{
			for (int i = 0; i < this.trasy.Count; i++)
			{
				List<Lotnisko> lotniska = trasy[i].getLotniska();
				for (int j = 0; j < lotniska.Count; j++)
				{
					if (l.Equals(lotniska[j]))
					{
						return true;
					}
				}
			}
			return false;
		}

		//Generowanie lotow.
		public void generujLot(Trasa trasa, DateTime DataP, DateTime DataK, string Id)
		{
			//Sprawdzenie czy lot o podanym id wystepuje na liscie lotow.
			if (sprawdzId(Id))
				throw new GenerujLotException("Lot o podanym Id wystapil juz na liscie. Nie mozna dodac.");

			//Sprawdzenie dat.
			if (DataK < DataP)
				throw new GenerujLotException("Data zakonczenia lotu nie moze wystapic przed data rozpoczecia.");

			//Nastepnie wykonywany jest dobor samolotu
			double Dystans = trasa.getDystans();
			if(Dystans <= 1000)
            {
				Regionalny r = (Regionalny)znajdzSamolot(DataP, DataK,Dystans);
				if (r == null)
					throw new GenerujLotException("Na liscie nie wystepuje samolot Regionalny, ktory moglby oblusyzc wybrana trase.");
				dodajLot(new Lot(r, trasa, DataP, DataK,  Id));
            }
			else if(1000 < Dystans && Dystans <=5000)
            {
				Sredniodystansowy sr = (Sredniodystansowy)znajdzSamolot(DataP, DataK, Dystans);
				if (sr == null)
					throw new GenerujLotException("Na liscie nie wystepuje samolot Sredniodystansowy, ktory moglby oblusyzc wybrana trase.");
				dodajLot(new Lot(sr, trasa, DataP,  DataK,  Id));
			}
			else if(5000<Dystans && Dystans<=20000)
            {
				Dlugodystansowy dl = (Dlugodystansowy)znajdzSamolot(DataP, DataK, Dystans);
				if (dl == null)
					throw new GenerujLotException("Na liscie nie wystepuje samolot Dlugodystansowy, ktory moglby oblusyzc wybrana trase.");
				dodajLot(new Lot(dl, trasa, DataP, DataK, Id));
			}
		}
		private bool sprawdzId(string Id)
        {
			for(int i=0;i<loty.Count;i++)
            {
				if (Id == loty[i].getId())
					return true;
            }
			return false;
		}
		private Samolot znajdzSamolot(DateTime DataP, DateTime DataK,double Dystans)
        {
			for(int i=0;i<samoloty.Count;i++)
            {
				if (Dystans > samoloty[i].getZasieg())
					continue;
				//Metoda dostepnosc Samolotu sprawdza czy dany samolot jest uzywany w danym przedziale czasowym.
				//Metoda zwraca true jezeli jest uzywany i false jezeli nie.
				if (!dostepnoscSamolotu(samoloty[i],DataP, DataK))
					return samoloty[i];
            }
			return null;
			
        }
		private bool dostepnoscSamolotu(Samolot s, DateTime DataP, DateTime DataK)
        {
			for(int i=0;i<loty.Count;i++)
            {
				if (s.Equals(loty[i].getSamolot()) && loty[i].getDataPocz()==DataP &&loty[i].getDataKon()==DataK)
					return true;
			}
			return false;
        }




	}



	public class LiniaLotniczaException : Exception
	{
		public LiniaLotniczaException(string msg) : base(msg) { }
	}
	public class ListaException : LiniaLotniczaException
	{
		public ListaException(string msg) : base(msg) { }
	}
	public class UsunException : LiniaLotniczaException
    {
		public UsunException(string msg) : base(msg) { }
	}
	public class GenerujLotException : LiniaLotniczaException
    {
		public GenerujLotException(string msg) : base(msg) { }
	}

}

