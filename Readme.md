Documentazione Progetto: ModShop
ModShop è un'applicazione console in C# progettata per la gestione di un catalogo prodotti e di ordini, integrando diversi design pattern per garantire flessibilità e scalabilità
.

--------------------------------------------------------------------------------
1. Design Pattern Implementati
Singleton: AppContext
Viene utilizzato per gestire le configurazioni globali dell'applicazione, assicurando che esista una sola istanza del contesto
.
Responsabilità: Gestione della valuta, dell'IVA (default 22-23%) e dello sconto base
.
Metodi principali: getInstance, NuovaIva(), NuovoSconto(), NuovaValuta()
.
Strategy: Calcolo del Prezzo
Il pattern Strategy permette di cambiare dinamicamente la logica di calcolo del prezzo finale tramite l'interfaccia IStrategia
.
Strategie disponibili:
StandardPricing: Calcola il prezzo base più l'IVA
.
PromoPricing: Applica sconti basati su una soglia di prezzo
.
WholesalePricing: Prezzi per l'ingrosso con sconti dedicati
.
DynamicPricing: Permette sconti variabili se la modalità dinamica è attiva
.
Decorator: Personalizzazione Prodotti
Utilizzato per aggiungere funzionalità o costi aggiuntivi ai prodotti esistenti in modo dinamico
.
Classe base: ProductDecorator (eredita da IProduct)
.
Decoratori concreti:
StampaFronte (+3.5)
.
StampaRetro (+3.2)
.
ConfezioneRegalo (+1.5)
.
EstensioneGaranzia (+5.5)
.
Incisione (+4.0)
.
Observer: Sistema di Notifica
Permette a diversi componenti (come log o interfacce utente) di reagire ai cambiamenti dei prodotti
.
Interfaccia: IProductObserver con metodo Aggiorna()
.
Osservatori: Log, UI, Mock
.
Soggetti: TShirt, Mug e Skin mantengono una lista di osservatori
.

--------------------------------------------------------------------------------
2. Catalogo Prodotti Base
I prodotti implementano l'interfaccia IProduct, che richiede i metodi GetName() e GetPrice()
.
Prodotto
Prezzo Base
TSHIRT
20
MUG
10
SKIN
5
(Fonte:
)

--------------------------------------------------------------------------------
3. Funzionalità dell'Applicazione
Il programma principale (Program.cs) offre le seguenti opzioni all'utente:
Mostra Catalogo: Visualizza i prodotti disponibili e i loro prezzi base
.
Aggiungi Prodotto: Permette di selezionare un prodotto e aggiungere osservatori
.
Applica Decoratore: Consente di personalizzare un prodotto aggiungendo opzioni (es. stampa o garanzia) che ne incrementano il prezzo
.
Rimuovi Prodotto: Permette di eliminare un elemento specifico dall'ordine corrente
.
Modifica IVA: Consente di aggiornare la percentuale IVA globale tramite il Singleton AppContext
.
Calcolo Totale: Cicla tra i prodotti nell'ordine, ne visualizza i dettagli e calcola la somma finale
.

--------------------------------------------------------------------------------
4. Struttura delle Interfacce Core
public interface IProduct {
    string GetName();
    double GetPrice();
}

public interface IStrategia {
    double Pricing(double prezzo);
}

public interface IProductObserver {
    void Aggiorna(IProduct product, string messaggio);
}