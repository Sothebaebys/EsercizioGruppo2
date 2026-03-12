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
   decimal Pricing(decimal prezzo);

}

public class StandarndPricing : IStrategia
{
   //Proprietà di classe
   public decimal iva;
   //public decimal sogliaSconto;
   
   //Costruttore pubblico,
   // nel momento in cui viene chiamato 
   // passare gli argomenti (iva e sconto) dal singleton
   public StandarndPricing(decimal iva /*, decimal sogliaSconto*/)
   {
      this.iva = iva;
      //this.sogliaSconto = sogliaSconto;
   }
   
   public decimal Pricing(decimal prezzo)
   {
      //calcolo il prezzo ivato
      decimal prezzo = prezzo *((iva / 100)+1);
      return prezzo;
   }
}
//Con soglia
public class PromoPricing : IStrategia
{
   public decimal iva;
   public decimal sogliaSconto;
   public decimal scontoApplicabile;

   public PromoPricing (decimal iva, decimal sogliaSconto, decimal scontoApplicabile)
   {
      this.iva = iva;
      this.sogliaSconto = sogliaSconto;
      this.scontoApplicabile = scontoApplicabile;
   }
   public decimal Pricing(decimal prezzo)
   {
      //controlla se il prezzo ivato supera la soglia di sconto
      decimal prezzo = prezzo *((iva / 100)+1);
      
      if(prezzo >= sogliaSconto)
      {
         //si applica uno sconto fisso
         return prezzo - scontoApplicabile;
      }
      return prezzo;
   }
}


public class WholesalePricing : IStrategia
{
   public decimal Pricing(decimal prezzo)
   {
      return ;
   }
}

//sconto percentuale
public class DynamicPricing : IStrategia
{
   public decimal iva;
   public decimal scontoApplicabile;
   public bool isDynamic= false;

   public DynamicPricing(decimal iva,decimal scontoApplicabile, bool isApplicabile)
   {
      this.iva = iva;
      this.scontoApplicabile = scontoApplicabile;
      this.isDynamic = isApplicabile;
   }

   public decimal Pricing(decimal prezzo)
   {
      //calcolo il prezzo ivato
      decimal prezzo = prezzo *((iva / 100)+1);
      if (isDynamic)
      {
         return prezzo - scontoApplicabile;
      }


      return prezzo;
   }
}


public class Contesto
{
   private IStrategia _strategia;

   public void SetStrategia (IStrategia strategia)
   {
      _strategia = strategia;
   }

   public void EseguiStrat (decimal prezzo)
   {
      if (_strategia == null)
      {
         Console.WriteLine($"Imposta la tipologia di pricing");
         return;
      }

      double totale = _strategia.Pricing(prezzo);
      Console.WriteLine($"Totale: {totale}");
      
   }
}