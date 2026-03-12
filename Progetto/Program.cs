using System;
using System.Collections.Generic;

class Program
{
   static void Main()
   {
      Console.WriteLine("=== Benvenuto in ModShop ===\n");

        // Lista prodotti aggiunti all'ordine
      List<IProduct> ordine = new List<IProduct>();

        // Lista observer globali
      List<IProductObserver> globalObservers = new List<IProductObserver>()
      {
         new Log(),
         new UI(),
         new Mock()
      };

      bool running = true;
      while (running)
      {
         Console.WriteLine("\nMenu:");
         Console.WriteLine("1. Mostra catalogo prodotti");
         Console.WriteLine("2. Aggiungi prodotto all'ordine");
         Console.WriteLine("3. Applica decoratore all'ultimo prodotto");
         Console.WriteLine("4. Mostra totale ordine (con IVA)");
         Console.WriteLine("5. Rimuovi prodotto dall'ordine");
         Console.WriteLine("6. Modifica IVA");
         Console.WriteLine("7. Esci");
         Console.Write("Seleziona un'opzione: ");

         string scelta = Console.ReadLine();
         switch (scelta)
         {
               case "1":
                  MostraCatalogo();
                  break;

               case "2":
                  IProduct prodotto = AggiungiProdotto(globalObservers);
                  if (prodotto != null)
                     ordine.Add(prodotto);
                  break;

               case "3":
                  if (ordine.Count == 0)
                     Console.WriteLine("Nessun prodotto nell'ordine!");
                  else
                     ordine[ordine.Count - 1] = ApplicaDecoratore(ordine[ordine.Count - 1]);
                  break;

               case "4":
                  MostraTotale(ordine);
                  break;

               case "5":
                  RimuoviProdotto(ordine);
                  break;

               case "6":
                  ModificaIVA();
                  break;

               case "7":
                  running = false;
                  break;

               default:
                  Console.WriteLine("Opzione non valida.");
                  break;
         }
      }
   }

   static void MostraCatalogo()
   {
      Console.WriteLine("\nCatalogo Prodotti:");
      Console.WriteLine("TSHIRT - Prezzo base 20");
      Console.WriteLine("MUG - Prezzo base 10");
      Console.WriteLine("SKIN - Prezzo base 5");
   }

   static IProduct AggiungiProdotto(List<IProductObserver> observers)
   {
      Console.Write("Inserisci codice prodotto (TSHIRT, MUG, SKIN): ");
      string codice = Console.ReadLine().ToUpper();

      try
      {
         IProduct prodotto = ProductFactory.CreateProduct(codice);

         foreach (var obs in observers)
         prodotto.Registra(obs);

         prodotto.Notifica("Prodotto aggiunto all'ordine");
         Console.WriteLine($"{prodotto.GetName()} aggiunto con prezzo base {prodotto.GetPrice()}");
         return prodotto;
      }
      catch
      {
         Console.WriteLine("Codice prodotto non valido.");
         return null;
      }
   }

   static IProduct ApplicaDecoratore(IProduct prodotto)
   {
      Console.WriteLine("\nDecoratori disponibili:");
      Console.WriteLine("1. Stampa Fronte (+3.5)");
      Console.WriteLine("2. Stampa Retro (+3.2)");
      Console.WriteLine("3. Confezione Regalo (+1.5)");
      Console.WriteLine("4. Estensione Garanzia (+5.5)");
      Console.WriteLine("5. Incisione (+4.0)");
      Console.Write("Seleziona decoratore: ");
      string scelta = Console.ReadLine();

      IProduct decorato = scelta switch
      {
         "1" => new StampaFronte(prodotto),
         "2" => new StampaRetro(prodotto),
         "3" => new ConfezioneRegalo(prodotto),
         "4" => new EstensioneGaranzia(prodotto),
         "5" => new Incisione(prodotto),
         _ => null
      };

      if (decorato == null)
      {
         Console.WriteLine("Decoratore non valido.");
         return prodotto;
      }

      decorato.Notifica("Decoratore applicato");
      Console.WriteLine($"{decorato.GetName()} - Prezzo ora {decorato.GetPrice()}");
      return decorato;
   }

   static void MostraTotale(List<IProduct> ordine)
   {
      if (ordine.Count == 0)
      {
         Console.WriteLine("Ordine vuoto.");
         return;
      }

      double totale = 0;
      double iva = AppContext.getInstance.iva / 100;

      Console.WriteLine("\nProdotti nell'ordine:");
      foreach (var prod in ordine)
      {
         double prezzo = prod.GetPrice();
         Console.WriteLine($"{prod.GetName()} - Prezzo base {prezzo} - Prezzo con IVA {prezzo * (1 + iva)}");
         totale += prezzo;
      }

      Console.WriteLine($"Totale senza IVA: {totale}");
      Console.WriteLine($"Totale con IVA {AppContext.getInstance.iva}%: {totale * (1 + iva)}");
   }

static void RimuoviProdotto(List<IProduct> ordine)
{
   if (ordine.Count == 0)
   {
      Console.WriteLine("L'ordine è vuoto.");
      return;
   }

   Console.WriteLine("\nProdotti nell'ordine:");

   for (int i = 0; i < ordine.Count; i++)
   {
      Console.WriteLine($"{i + 1}. {ordine[i].GetName()} - {ordine[i].GetPrice()}");
   }

   Console.Write("Seleziona il numero del prodotto da rimuovere: ");
   string input = Console.ReadLine();

   if (int.TryParse(input, out int scelta))
   {
      if (scelta > 0 && scelta <= ordine.Count)
      {
         IProduct prodotto = ordine[scelta - 1];

         prodotto.Notifica("Prodotto rimosso dall'ordine");

         ordine.RemoveAt(scelta - 1);

         Console.WriteLine("Prodotto rimosso con successo.");
      }
      else
      {
         Console.WriteLine("Indice non valido.");
      }
   }
   else
   {
      Console.WriteLine("Input non valido.");
   }
}

static void ModificaIVA()
   {
      Console.Write("Inserisci nuova IVA (%) [es. 22]: ");
      string input = Console.ReadLine();
      if (double.TryParse(input, out double nuovaIva))
      {
         AppContext.getInstance.NuovaIva(nuovaIva);
         Console.WriteLine($"IVA aggiornata a {AppContext.getInstance.iva}%");
      }
      else
      {
         Console.WriteLine("Valore non valido.");
      }
   }
}