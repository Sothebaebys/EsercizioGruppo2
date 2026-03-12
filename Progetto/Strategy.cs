/*
Strategy:
strategie di pricing intercambiabili 
es.
StandardPricing,
PromoPricing, 
WholesalePricing, 
DynamicPricing.

*/

public interface IStrategia
{
   double Pricing(double prezzo);

}

public class StandarndPricing : IStrategia
{
   //Proprietà di classe
   public double iva;
   //public double sogliaSconto;
   
   //Costruttore pubblico,
   // nel momento in cui viene chiamato 
   // passare gli argomenti (iva e sconto) dal singleton
   public StandarndPricing(double iva /*, double sogliaSconto*/)
   {
      this.iva = iva;
      //this.sogliaSconto = sogliaSconto;
   }
   
   public double Pricing(double prezzo)
   {
      //calcolo il prezzo ivato
      double prezzoTot = prezzo *((iva / 100)+1);
      return prezzoTot;
   }
}
//Con soglia
public class PromoPricing : IStrategia
{
   public double iva;
   public double sogliaSconto;
   public double scontoApplicabile;

   public PromoPricing (double iva, double sogliaSconto, double scontoApplicabile)
   {
      this.iva = iva;
      this.sogliaSconto = sogliaSconto;
      this.scontoApplicabile = scontoApplicabile;
   }
   public double Pricing(double prezzo)
   {
      //controlla se il prezzo ivato supera la soglia di sconto
      double prezzoTot = prezzo *((iva / 100)+1);
      
      if(prezzoTot >= sogliaSconto)
      {
         //si applica uno sconto fisso
         return prezzoTot - scontoApplicabile;
      }
      return prezzoTot;
   }
}

public class WholesalePricing : IStrategia
{

   public double iva;
   public double scontoApplicabile;

   public WholesalePricing(double iva, double scontoApplicabile)
   {
      this.iva = iva;
      this.scontoApplicabile = scontoApplicabile;
   }
   public double Pricing(double prezzo)
   {
      //Calcolo tot ivato
      double prezzoTot = prezzo *((iva / 100)+1);

      return prezzoTot-scontoApplicabile;
   }
}

//sconto percentuale
public class DynamicPricing : IStrategia
{
   public double iva;
   public double scontoApplicabile;
   public bool isDynamic= false;

   public DynamicPricing(double iva,double scontoApplicabile, bool isApplicabile)
   {
      this.iva = iva;
      this.scontoApplicabile = scontoApplicabile;
      this.isDynamic = isApplicabile;
   }

   public double Pricing(double prezzo)
   {
      //calcolo il prezzo ivato
      double prezzoTot = prezzo *((iva / 100)+1);
      if (isDynamic)
      {
         return prezzoTot - scontoApplicabile;
      }


      return prezzoTot;
   }
}

/*
public class Contesto
{
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

      decimal totale = _strategia.Pricing(prezzo);
      Console.WriteLine($"Totale: {totale}");
      
   }
}
*/