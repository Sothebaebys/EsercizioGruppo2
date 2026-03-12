/*Singleton:
AppContext
(configurazioni globali: valuta, IVA, sconti di base,logger/event bus).

DaFare:
   Logger / Event bus
    */
public sealed class AppContext
{
   //Proprietà private
   private string _valuta;
   private double _iva;
   private double _scontoBase;

   //getter e setter proprietà private
   public string valuta
   {
      get
      {
         return _valuta;
      }
      private set
      {
         if (!string.IsNullOrEmpty(value) && value.Length == 3)
         {
            _valuta = value.ToUpper();
         }

      }
   }
   
   public double iva
   {
      get{ return _iva;}
      private set
      {
         if (value > 0)
         {
            _iva = value;
         }
         else
         {
            _iva = 23.0d;
         }
      }
   }
   
   public double scontoBase
   {
      get
      {
         return _scontoBase;
      }
      private set
      {
         if (value > 0)
         {
            _scontoBase = value;
         }
         else
         {
            _scontoBase = 0;
         }
      }
   }

   //Proprietà statica di classe
   private static AppContext _instance;

   //Getter dell'istanza
   public static AppContext getInstance
   {
      get
      {
         if (_instance == null)
         {
            _instance = new AppContext();
         }

         return _instance;
      }
   }
   
   //Costruttore privato con valori default
   private AppContext()
   {

      _valuta = "EUR";
      _iva = 23.0d;
      _scontoBase = 0;

   }


   public void NuovoSconto(double sconto)
   {
      if (sconto< 100 && sconto > 0)
      {
         _scontoBase = sconto;
      }
   }

   public void NuovaIva(double iva)
   {
      if (iva < 100 && iva >= 0)
      {
         _iva = iva;
      }
   }

   public void NuovaValuta(string valuta)
   {
      if (!string.IsNullOrEmpty(valuta) && valuta.Length == 3)
      {
         _valuta = valuta.ToUpper();
      }
   }

   #region ContestoStrategia
   //Parte che gestisce la strategia
   private IStrategia _strategia;

   public void SetStrategia (IStrategia strategia)
   {
      _strategia = strategia;
   }

   public void EseguiStrat (double prezzo)
   {
      if (_strategia == null)
      {
         Console.WriteLine($"Imposta la tipologia di pricing");
         return;
      }

      double totale = _strategia.Pricing(prezzo);
      Console.WriteLine($"Totale: {totale}");
      
      
      
   }
   #endregion
   
   //Logger / Eventbus

}