using System;
using System.Collections.Generic;
using System.Threading;

namespace LiniaLotnicza
{
	class Program
	{
		static void Main(string[] args)
		{

			LiniaLotnicza LiniaL = new LiniaLotnicza("Lot");

			Console.WriteLine("");
			Console.WriteLine("		   Witamy w systemie kontroli lotow");
			Console.WriteLine("");
			Console.WriteLine("				Autorzy: ");
			Console.WriteLine("			- Michal Dzienisik ");
			Console.WriteLine("			- Kornel Golebiewski");
			Console.WriteLine("			- Krzysztof Goral");
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("		Przycisnij dowolny przycisk aby zaczac ...");
			Console.Write("			         ");
			Console.ReadKey();


			while (true)
			{
				Thread.Sleep(2000);
				Console.Clear();
				Console.WriteLine("");
				Console.WriteLine("			1. Panel Administracyjny");
				Console.WriteLine("			2. Panel uzytkownika/goscia");
				Console.WriteLine("");
				Console.WriteLine("			0. Powrot");

				Console.Write("			         ");
				int wyborPanelu = Convert.ToInt32(Console.ReadLine());


				switch (wyborPanelu)
				{
					case 1:
						Console.Clear();
						Console.WriteLine("");
						Console.WriteLine("			1. Zarzadzanie  Linia Lotnicza");
						Console.WriteLine("			2. Zarzadzanie  Lotniskami");
						Console.WriteLine("			3. Zarzadzanie  Samolotami");
						Console.WriteLine("			4. Zarzadzanie  Lotami");
						Console.WriteLine("			5. Zarzadzanie  Trasami");
						Console.WriteLine("			6. Zarzadzanie  Kientami");
						Console.WriteLine("");
						Console.WriteLine("			0. Powrot");

						Console.Write("			         ");
						int wyborA = Convert.ToInt32(Console.ReadLine());

						switch (wyborA)
						{
							case 1:
								OperacjeLiniaLotnicza(LiniaL);
								break;
							case 2:
								OperacjeLotniska(LiniaL);
								break;
							case 3:
								OperacjeSamolot(LiniaL);
								break;
							case 4:
								OperacjeLot(LiniaL);
								break;
							case 5:
								OperacjeTrasa(LiniaL);
								break;
							case 6:
								OperacjeKlient(LiniaL);
								break;
							case 0:
								break;
						}

						break;
					case 2:
						Console.Clear();
						Console.WriteLine("");
						Console.WriteLine("			1. Rezerwuj bilet");
						Console.WriteLine("			2. Usun rezerwacje");
						Console.WriteLine("			3. Wyswietl liste lotow");
						Console.WriteLine("			4. Wyswietl liste tras");
						Console.WriteLine("			5. Wyswietl liste lotnisk");
						Console.WriteLine("");
						Console.WriteLine("			0. Powrot");

						Console.Write("			         ");
						int wyborB = Convert.ToInt32(Console.ReadLine());

						switch (wyborB)
						{
							case 1:
								Console.WriteLine("");
								Console.WriteLine("			Witaj w system rezerwacji biletow");

								Klient K = TworzenieKlienta();
								Rezerwacja R = new Rezerwacja(K);
								try { LiniaL.dodajKlienta(K); } catch (ListaException Le) { };
								Console.WriteLine("");
								Console.WriteLine("			1. Nie znasz ID lotu");
								Console.WriteLine("			2. Znasz ID lotu");
								int wybor = Convert.ToInt32(Console.ReadLine());
								if (wybor == 1) WyswietLot(LiniaL);


								Console.WriteLine("");
								Console.WriteLine("			Wpisz ID wybranego lotu");


								string wyborID = Console.ReadLine();

								Lot L = ZnajdzLotID(LiniaL, wyborID);

								if (L == null)
								{
									Console.WriteLine("			Nie wystepuje Lot o podanym ID");
									break;
								}

								Console.WriteLine("			Podaj miasto lotniska docelowego wybranego lotu");
								string wyborMiasta = Console.ReadLine();


								Lotnisko LotniskoL = ZnajdzMiejsceLot(L, wyborMiasta);

								if (LotniskoL == null)
								{
									Console.WriteLine("			Podane miasto docelowe nie znajduje sie w locie");
									break;
								}

								Console.WriteLine("			Podaj liczbe biletow, ktore chcesz zarezerwowac");
								int liczbaB = Convert.ToInt32(Console.ReadLine());

								int liczbaMiejsc = LiczbaWolnychMiejsc(L);

								if (liczbaB > liczbaMiejsc)
								{
									Console.WriteLine("			Podane liczba biletow jest wieksza niz liczba dostpnych miejsc");
									break;
								}
								for (int i = 0; i < liczbaB; i++)
								{
									Bilet B = new Bilet(LotniskoL.getMiasto(), L.getDataPocz(), L.getDataKon());

									R.dodajBilet(B);
								}
								L.dodajRezerwacje(R);
								break;
							case 2:
								Console.WriteLine("");
								Console.WriteLine("			Witaj w systemie do usuwania rezerwacji biletow");

								Klient Kl = TworzenieKlienta();
								Rezerwacja Re = new Rezerwacja(Kl);
								try { LiniaL.dodajKlienta(Kl); } catch (ListaException Le) { };
								Console.WriteLine("");
								Console.WriteLine("			1. Nie znasz ID lotu");
								Console.WriteLine("			2. Znasz ID lotu");
								int wybor1 = Convert.ToInt32(Console.ReadLine());
								if (wybor1 == 1) WyswietLot(LiniaL);
								Console.WriteLine("");
								Console.WriteLine("			Wpisz ID wybranego lotu");


								string wyborID1 = Console.ReadLine();

								Lot Lo = ZnajdzLotID(LiniaL, wyborID1);

								if (Lo == null)
								{
									Console.WriteLine("			Nie wystepuje Lot o podanym ID");
									break;
								}
								Rezerwacja r = RezerwacjaZnajdz(Lo, Kl);
								if (r == null)
								{
									Console.WriteLine("			Podany klient nie zarezerwowal zadnych biletow w tym locie.");
									break;
								}
								Lo.usunRezerwacje(r);
								break;
							case 3:
								WyswietLot(LiniaL);
								break;
							case 4:
								WyswietTrasa(LiniaL);
								break;
							case 5:
								WyswietLotniska(LiniaL);
								break;
							case 0:
								break;
							default:
								break;
						}
						break;
					case 0:
						break;
					default:
						break;
				}
			}
		}
		public static Rezerwacja RezerwacjaZnajdz(Lot L, Klient K)
		{
			List<Rezerwacja> rez = L.getRezerwacje();
			foreach (Rezerwacja r in rez)
			{
				if (K.Equals(r.getKlient()))
					return r;
			}
			return null;
		}

		public static int LiczbaWolnychMiejsc(Lot L)
		{
			List<Rezerwacja> R = L.getRezerwacje();
			int liczba = 0;
			foreach (Rezerwacja Rez in R)
			{
				foreach (Bilet B in Rez.getBilety())
				{
					liczba++;

				}
			}
			return L.getSamolot().getLiczbaMiejsc() - liczba;
		}
		public static Lot ZnajdzLotID(LiniaLotnicza LiniaL, string wyborID)
		{
			foreach (Lot L in LiniaL.getLoty())
			{
				if (wyborID == L.getId())
				{
					return L;
				}
			}
			return null;
		}

		public static Lotnisko ZnajdzMiejsceLot(Lot lot, string miasto)
		{
			Trasa T = lot.getTrasa();
			foreach (Lotnisko L in T.getLotniska())
			{
				if (miasto == L.getMiasto())
				{
					return L;
				}
			}
			return null;
		}



		public static void WyswietLot(LiniaLotnicza LiniaL)
		{
			Console.WriteLine("");
			Console.WriteLine("			Loty: ");
			foreach (Lot L in LiniaL.getLoty())
			{
				Console.WriteLine("			 Id Lotu: " + L.getId());
				Console.WriteLine("			 Godziny Lotu: " + L.getDataPocz() + "  " + L.getDataKon() + "  ");
				WyswietTrasaJedna(L);

			}
			Console.ReadKey();
		}


		public static void WyswietTrasaJedna(Lot l)
		{
			Trasa t = l.getTrasa();
			Console.WriteLine("			Id: " + t.getId());
			WyswietLotniskaAll(t);
			Console.ReadKey();
		}
		public static void WyswietTrasa(LiniaLotnicza LiniaL)
		{
			Console.WriteLine("");
			Console.WriteLine("			Trasy: ");
			foreach (Trasa T in LiniaL.getTrasy())
			{
				Console.WriteLine("			Id: " + T.getId());
				Console.WriteLine("			Dystans:" + T.getDystans());
				WyswietLotniskaAll(T);
			}
			Console.ReadKey();
		}

		public static void WyswietLotniskaAll(Trasa T)
		{

			foreach (Lotnisko lotnisko in T.getLotniska())
			{
				Console.WriteLine("			 Panstwo: " + lotnisko.getKraj() + " Miasto: " + lotnisko.getMiasto() + " ID: " + lotnisko.getId());
			}


		}
		public static void WyswietLotniska(LiniaLotnicza LiniaL)
		{
			Console.WriteLine("");
			Console.WriteLine("			Lotniska: ");
			foreach (Lotnisko L in LiniaL.getLotniska())
			{
				Console.WriteLine("			" + L.getId() + " Miasto: " + L.getMiasto() + " Kraj: " + L.getKraj());
			}
			Console.ReadKey();
		}

		public static void WyswietSamolot(LiniaLotnicza LiniaL)
		{
			Console.WriteLine("");
			Console.WriteLine("			Samoloty: ");
			foreach (Samolot S in LiniaL.getSamoloty())
			{

				Console.WriteLine("			Id: " + S.getId());
				Console.WriteLine("			Zasieg: " + S.getZasieg());
				Console.WriteLine("			LiczbaMiejsc: " + S.getLiczbaMiejsc() + "\n");
			}
			Console.ReadKey();
		}
		public static void WyswietKlient(LiniaLotnicza LiniaL)
		{
			Console.WriteLine("");
			Console.WriteLine("			  Klienci: ");
			Console.WriteLine("");



			Console.WriteLine("			Typ Kientami:");
			Console.WriteLine("			1. Indywidualny");
			Console.WriteLine("			2. Firma");
			Console.WriteLine("");
			Console.WriteLine("			0. Powrot");

			Console.Write("			         ");
			int wybor = Convert.ToInt32(Console.ReadLine());

			switch (wybor)
			{
				case 1:
					foreach (Indywidualny I in LiniaL.getKlient())
					{
						Console.WriteLine("			Id:" + I.getId() + " Imie:" + I.getImie() + " Nazwisko:" + I.getNazwisko() + " Wiek:" + I.getWiek() + " Narodowosc:" + I.getNarodowosc());
					}
					break;
				case 2:

					foreach (PosrednikFirmy P in LiniaL.getKlient())
					{
						Console.WriteLine("			Id:" + P.getId() + " NazwaFirmy:" + P.getNazwaFirmy());
					}
					break;
				case 0:
					break;
				default:
					break;
			}
			Console.ReadKey();
		}

		public static void OperacjeLiniaLotnicza(LiniaLotnicza LiniaL)
		{
			Console.Clear();
			Console.WriteLine("");
			Console.WriteLine("			Zarzadzanie Linia Lotnicza:");
			Console.WriteLine("			1. Wyswietl nazwe");
			Console.WriteLine("			2. Generuj Lot");
			Console.WriteLine("");
			Console.WriteLine("			0. Powrot");
			Console.Write("			         ");
			int wyborLinia = Convert.ToInt32(Console.ReadLine());

			switch (wyborLinia)
			{
				case 1:
					Console.Write("			         ");
					Console.WriteLine(LiniaL.getNazwaLinii());
					Console.ReadKey();
					break;
				case 2:
					Samolot s = TworzenieSamolotu();
					try { LiniaL.dodajSamolot(s); } catch (ListaException Le) { Console.WriteLine("			" + Le.Message); };


					Trasa t = DodawanieTrasa(LiniaL);
					try { LiniaL.dodajTrase(t); } catch (ListaException Le) { Console.WriteLine("			" + Le.Message); };

					DateTime DataP = DataOdlotu();
					DateTime DataK = DataPrzylotu();


					Console.WriteLine("			Podaj ID lotu");
					Console.Write("			         ");
					string id = Console.ReadLine();
					LiniaL.generujLot(t, DataP, DataK, id);
					break;
				case 0:
					break;
				default:
					break;

			}

		}
		public static void OperacjeLotniska(LiniaLotnicza LiniaL)
		{
			Console.Clear();
			Console.WriteLine("");
			Console.WriteLine("			Zarzadzanie  Lotniskami:");
			Console.WriteLine("			1. Wyswietl nazwe");
			Console.WriteLine("			2. Dodaj Lotnisko");
			Console.WriteLine("			3. Usun Lotnisko");
			Console.WriteLine("");
			Console.WriteLine("			0. Powrot");
			Console.Write("			         ");
			int wyborLotnisko = Convert.ToInt32(Console.ReadLine());


			switch (wyborLotnisko)
			{
				case 1:
					WyswietLotniska(LiniaL);
					break;
				case 2:
					Lotnisko lAdd = TworzenieLotniska();
					try { LiniaL.dodajLotnisko(lAdd); } catch (ListaException Le) { Console.WriteLine("			" + Le.Message); };
					break;
				case 3:
					Lotnisko lDelete = TworzenieLotniska();
					try { LiniaL.usunLotnisko(lDelete); } catch (UsunException Ue) { Console.WriteLine("			" + Ue.Message); };
					break;
				case 0:
					break;
				default:
					break;

			}


		}
		public static Lotnisko TworzenieLotniska()
		{
			Console.Clear();
			Console.WriteLine("");
			Console.WriteLine("			Dodawanie Lotniska");
			Console.WriteLine(" ");
			Console.WriteLine("			Podaj Panstwo Lotniska");
			Console.Write("			         ");
			string kraj = Console.ReadLine();
			Console.WriteLine("			Podaj Miasto Lotniska");
			Console.Write("			         ");
			string miasto = Console.ReadLine();
			Console.WriteLine("			Podaj ID Lotniska");
			Console.Write("			         ");
			string id = Console.ReadLine();
			return new Lotnisko(kraj, miasto, id);

		}

		public static void OperacjeSamolot(LiniaLotnicza LiniaL)
		{
			Console.Clear();
			Console.WriteLine("");
			Console.WriteLine("			Zarzadzanie Samolotami:");
			Console.WriteLine("			1. Wyswietl nazwe");
			Console.WriteLine("			2. Dodaj Samolot");
			Console.WriteLine("			3. Usun Samolot");
			Console.WriteLine("");
			Console.WriteLine("			0. Powrot");
			Console.Write("			         ");
			int wyborSamolot = Convert.ToInt32(Console.ReadLine());
			switch (wyborSamolot)
			{
				case 1:
					WyswietSamolot(LiniaL);
					break;
				case 2:
					Samolot sAdd = TworzenieSamolotu();
					try { LiniaL.dodajSamolot(sAdd); } catch (ListaException Le) { Console.WriteLine("			" + Le.Message); };
					break;
				case 3:
					Samolot sDelete = TworzenieSamolotu();
					try { LiniaL.usunSamolot(sDelete); } catch (UsunException Ue) { Console.WriteLine("			" + Ue.Message); };
					break;
				case 0:
					break;
				default:
					break;
			}
		}

		public static Samolot TworzenieSamolotu()
		{
			Console.Clear();
			Console.WriteLine("");
			Console.WriteLine("			Dodawanie Samolotu");
			Console.WriteLine(" ");
			Console.WriteLine("			Podaj Zasieg Samolotu");
			Console.Write("			         ");
			double zasieg = Convert.ToDouble(Console.ReadLine());
			Console.WriteLine("			Podaj ID Samolotu");
			Console.Write("			         ");
			string id = Console.ReadLine();
			Console.WriteLine("			Podaj Liczbe miejsc Samolotu");
			Console.Write("			         ");
			int miejsca = Convert.ToInt32(Console.ReadLine());
			if (zasieg <= 1000)
			{
				Regionalny samolotR = new Regionalny(zasieg, id, miejsca);
				return samolotR;
			}
			else if (zasieg <= 5000)
			{
				Sredniodystansowy samolotS = new Sredniodystansowy(zasieg, id, miejsca);
				return samolotS;
			}
			else
			{
				Dlugodystansowy samolotD = new Dlugodystansowy(zasieg, id, miejsca);
				return samolotD;
			}

		}

		public static void OperacjeLot(LiniaLotnicza LiniaL)
		{
			Console.Clear();
			Console.WriteLine("");
			Console.WriteLine("			Zarzadzanie Lotami:");
			Console.WriteLine("			1. Wyswietl nazwe");
			Console.WriteLine("			2. Dodaj Lot");
			Console.WriteLine("			3. Usun Lot");
			Console.WriteLine("");
			Console.WriteLine("			0. Powrot");
			Console.Write("			         ");
			int wyborLot = Convert.ToInt32(Console.ReadLine());
			switch (wyborLot)
			{
				case 1:
					WyswietLot(LiniaL);
					break;
				case 2:
					Lot lAdd = TworzenieLotu(LiniaL);
					if (lAdd != null)
						try { LiniaL.dodajLot(lAdd); } catch (ListaException Le) { Console.WriteLine("			" + Le.Message); };
					break;
				case 3:
					Lot l = TworzenieLotu(LiniaL);
					if (l != null)
						try { LiniaL.usunLot(l); } catch (UsunException Ue) { Console.WriteLine("			" + Ue.Message); };

					break;
				case 0:
					break;
				default:
					break;

			}
		}

		public static DateTime DataOdlotu()
		{

			Console.WriteLine("");
			Console.WriteLine("			Podaj Date odlotu: ");
			Console.WriteLine("");
			Console.Write("			Rok odlotu: ");
			int rokOdlotu = Convert.ToInt32(Console.ReadLine());
			Console.Write("			Miesiac odlotu: ");
			int miesieacOdlotu = Convert.ToInt32(Console.ReadLine());
			Console.Write("			Dzien odlotu: ");
			int dzienOdlotu = Convert.ToInt32(Console.ReadLine());
			Console.Write("			Godzine odlotu: ");
			int godzinaOdlotu = Convert.ToInt32(Console.ReadLine());
			Console.Write("			Minute odlotu: ");
			int minutaOdlotu = Convert.ToInt32(Console.ReadLine());

			return new DateTime(rokOdlotu, miesieacOdlotu, dzienOdlotu, godzinaOdlotu, minutaOdlotu, 0);

		}
		public static DateTime DataPrzylotu()
		{
			Console.WriteLine("");
			Console.WriteLine("			Podaj Date przylotu: ");
			Console.WriteLine("");
			Console.Write("			Rok przylotu: ");
			int rokPrzylotu = Convert.ToInt32(Console.ReadLine());
			Console.Write("			Miesiac przylotu: ");
			int miesieacPrzylotu = Convert.ToInt32(Console.ReadLine());
			Console.Write("			Dzien przylotu: ");
			int dzienPrzylotu = Convert.ToInt32(Console.ReadLine());
			Console.Write("			Godzine przylotu: ");
			int godzinaPrzylotu = Convert.ToInt32(Console.ReadLine());
			Console.Write("			Minuta przylotu: ");
			int minutaPrzylotu = Convert.ToInt32(Console.ReadLine());

			return new DateTime(rokPrzylotu, miesieacPrzylotu, dzienPrzylotu, godzinaPrzylotu, minutaPrzylotu, 0);

		}

		public static Lot TworzenieLotu(LiniaLotnicza LL)
		{
			Trasa t = DodawanieTrasa(LL);
			try { LL.dodajTrase(t); } catch (ListaException Le) { };

			Samolot s = TworzenieSamolotu();
			try { LL.dodajSamolot(s); } catch (ListaException Le) { };

			if (s.getZasieg() < t.getDystans())
			{
				Console.WriteLine("			Zasieg wybranego samolotu jest mniejszy od dystansu trasy. Lot nie zostanie utworzony. ");
				return null;
			}

			DateTime dataOdlotuDelete = DataOdlotu();
			DateTime dataPrzylotuDelete = DataPrzylotu();


			Console.WriteLine("			Podaj ID lotu");
			Console.Write("			         ");
			string id = Console.ReadLine();
			return new Lot(s, t, dataOdlotuDelete, dataPrzylotuDelete, id);

		}
		public static void OperacjeTrasa(LiniaLotnicza LiniaL)
		{
			Console.Clear();
			Console.WriteLine("			Zarzadzanie Trasami:");
			Console.WriteLine("			1. Wyswietl nazwe");
			Console.WriteLine("			2. Dodaj Trase");
			Console.WriteLine("			3. Usun Trase");
			Console.WriteLine("");
			Console.WriteLine("			0. Powrot");
			Console.Write("			         ");
			int wyborTrasa = Convert.ToInt32(Console.ReadLine());
			switch (wyborTrasa)
			{
				case 1:
					WyswietTrasa(LiniaL);
					break;
				case 2:
					DodawanieTrasa(LiniaL);
					break;
				case 3:
					UsuwanieTrasa(LiniaL);
					break;
				case 0:
					break;
				default:
					break;
			}

		}

		public static Trasa DodawanieTrasa(LiniaLotnicza LL)
		{
			Console.Clear();
			Console.WriteLine("");
			Console.WriteLine("			Dodawanie Trasy");
			Console.WriteLine(" ");
			Console.WriteLine("			Podaj Id Trasy");
			Console.Write("			         ");
			string id = Console.ReadLine();
			Console.WriteLine("			Podaj dystans Trasy");
			Console.Write("			         ");
			double dystan = Convert.ToDouble(Console.ReadLine());
			Console.WriteLine("			Dodawanie Lotnisk do trasy, by rozpoczac wcisnij dowolny przycisk procz 0");
			Trasa Add = new Trasa(dystan, id);
			while (true)
			{
				Console.Write("			         ");
				if (Console.ReadLine() == "0")
					break;
				Lotnisko l = TworzenieLotniska();
				try { LL.dodajLotnisko(l); } catch (ListaException Le) { }
				Add.dodajLotnisko(l);
				Console.WriteLine("			By kontynowac dodawanie kliknij dowolny przycisk procz 0");
				Console.WriteLine("			By zakonczyc dodawanie wcisnij 0");
			}
			try { LL.dodajTrase(Add); } catch (ListaException Le) { };
			return Add;

		}
		public static void UsuwanieTrasa(LiniaLotnicza LL)
		{
			Console.Clear();
			Console.WriteLine("");
			Console.WriteLine("			Usuwanie Trasy");
			Console.WriteLine(" ");
			Console.WriteLine("			Podaj Id Trasy");
			Console.Write("			         ");
			string id = Console.ReadLine();
			Console.WriteLine("			Podaj dystans Trasy");
			Console.Write("			         ");
			double dystan = Convert.ToDouble(Console.ReadLine());
			Trasa Delete = new Trasa(dystan, id);
			try { LL.usunTrase(Delete); } catch (UsunException Ue) { Console.WriteLine("			" + Ue.Message); };
		}
		public static void OperacjeKlient(LiniaLotnicza LiniaL)
		{
			Console.Clear();
			Console.WriteLine("");
			Console.WriteLine("			Zarzadzanie Kientami:");
			Console.WriteLine("			1. Wyswietl dane");
			Console.WriteLine("			2. Dodaj Klienta");
			Console.WriteLine("			3. Usun Klienta");
			Console.WriteLine("");
			Console.WriteLine("			0. Powrot");
			Console.Write("			         ");
			int wyborKlienta = Convert.ToInt32(Console.ReadLine());
			switch (wyborKlienta)
			{
				case 1:
					WyswietKlient(LiniaL);
					break;
				case 2:
					Klient kAdd = TworzenieKlienta();
					try { LiniaL.dodajKlienta(kAdd); } catch (ListaException Le) { Console.WriteLine("			" + Le.Message); };
					break;
				case 3:
					Klient kDelete = TworzenieKlienta();
					try { LiniaL.usunKlienta(kDelete); } catch (UsunException Ue) { Console.WriteLine("			" + Ue.Message); };
					break;
				case 0:
					break;
				default:
					break;

			}
		}
		public static Klient TworzenieKlienta()
		{
			Console.Clear();
			Console.WriteLine("");
			Console.WriteLine("			Dodawanie Klienta :");
			Console.WriteLine("			Podaj ID");
			Console.Write("			         ");
			string id = Console.ReadLine();

			Console.WriteLine("			Typ Kientami:");
			Console.WriteLine("			1. Indywidualny");
			Console.WriteLine("			2. Firma");
			Console.WriteLine("");
			Console.WriteLine("			0. Powrot");

			Console.Write("			         ");
			int wyborTypu = Convert.ToInt32(Console.ReadLine());

			switch (wyborTypu)
			{
				case 1:
					Indywidualny I = KlientIndywidualy(id);
					return I;
					break;
				case 2:
					PosrednikFirmy P = Posrednikfirmy(id);
					return P;
					break;
				case 0:
					break;
				default:
					break;
			}
			return null;
		}

		public static Indywidualny KlientIndywidualy(string id)
		{
			Console.Clear();
			Console.WriteLine("");
			Console.WriteLine("			Klient Indywidualy:");
			Console.WriteLine(" ");

			Console.WriteLine("			Podaj imie");
			Console.Write("			         ");
			string imie = Console.ReadLine();
			Console.WriteLine("			Podaj nazwisko");
			Console.Write("			         ");
			string nazwisko = Console.ReadLine();
			Console.WriteLine("			Podaj narodowosc");
			Console.Write("			         ");
			string narodowosc = Console.ReadLine();
			Console.WriteLine("			Podaj swoj wiek");
			Console.Write("			         ");
			int wiek = Convert.ToInt32(Console.ReadLine());

			Indywidualny KlientI = new Indywidualny(id, imie, nazwisko, narodowosc, wiek);
			return KlientI;

		}

		public static PosrednikFirmy Posrednikfirmy(string id)
		{
			Console.Clear();
			Console.WriteLine("");
			Console.WriteLine("			Dodawanie Posrednik:");
			Console.WriteLine(" ");
			Console.WriteLine("			Podaj nazwe reprezentowanej firmy:");
			Console.Write("			         ");
			string nazwa = Console.ReadLine();

			PosrednikFirmy KlientF = new PosrednikFirmy(id, nazwa);
			return KlientF;
		}
	}
}