using System;
namespace LiniaLotnicza
{
	public abstract class Klient
	{
		protected string Id;
		public Klient() { }
		public Klient(string id)
		{
			this.Id = id;
		}
		public string getId() { return this.Id; }
		public override bool Equals(Object obj)
		{
			if ((obj == null) || !this.GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				Klient k = (Klient)obj;
				return (this.Id == k.getId());
			}
		}
	}
	public class Indywidualny : Klient
	{
		private string Imie;
		private string Nazwisko;
		private string Narodowosc;
		private int Wiek;
		public Indywidualny() { }
		public Indywidualny(string id,string imie, string nazwisko, string narodowosc, int wiek) : base(id)
		{
			if (wiek < 0)
				throw new WiekException("Wiek nie może być ujemny.");
			this.Id = id;
			this.Imie = imie;
			this.Nazwisko = nazwisko;
			this.Narodowosc = narodowosc;
			this.Wiek = wiek;
		}
		public string getImie() { return this.Imie; }
		public string getNazwisko() { return this.Nazwisko; }
		public int getWiek() { return this.Wiek; }
		public string getNarodowosc() { return this.Narodowosc; }
		public override bool Equals(Object obj)
		{
			if ((obj == null) || !this.GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				Indywidualny i = (Indywidualny)obj;
				return (this.Id == i.getId() && this.Imie == i.getImie() && this.Nazwisko == i.getNazwisko() && this.Wiek == i.getWiek()); 
			}
		}
	}

	
	public class PosrednikFirmy : Klient
	{
		private string NazwaFirmy;
		public PosrednikFirmy() { }
		public PosrednikFirmy(string id,string nazwafirmy) : base(id)
		{
			this.Id = id;
			this.NazwaFirmy = nazwafirmy;
		}
        public string getNazwaFirmy() { return this.NazwaFirmy; }
		public override bool Equals(Object obj)
		{
			if ((obj == null) || !this.GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				PosrednikFirmy pf = (PosrednikFirmy)obj;
				return (this.Id == pf.getId() && this.NazwaFirmy == pf.getNazwaFirmy());  

			}
		}
	}
	public class KlientException : Exception
	{
		public KlientException(string msg) : base(msg) { }
	}
    public class WiekException : KlientException
	{
		public WiekException(string msg) : base(msg) { }
	}
}