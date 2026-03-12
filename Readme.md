Relazione di Progettazione: Architettura Modulare e Design Pattern nel Sistema ModShop
1. Visione Architetturale e Obiettivi del Sistema
Il progetto ModShop è stato concepito per rispondere alle sfide di un mercato e-commerce sempre più dinamico, dove la capacità di adattare l'offerta e personalizzare l'esperienza d'acquisto rappresenta un asset strategico imprescindibile. In qualità di Software Architect, l'obiettivo primario è stato lo sviluppo di un'architettura che non si limitasse a gestire transazioni, ma che fungesse da framework estensibile e resiliente.
Il sistema è fondato su tre pilastri fondamentali: manutenibilità, estensibilità e una rigorosa separazione delle responsabilità (Separation of Concerns). Questi obiettivi sono stati perseguiti per gestire efficacemente prodotti fisici che richiedono configurazioni granulari e logiche di pricing variabili. Attraverso l'applicazione dei Design Pattern GoF, abbiamo trasformato requisiti di business complessi in componenti software disaccoppiati. Di seguito viene presentata l'analisi tecnica dei pattern implementati e il loro impatto sulla robustezza del sistema.
2. Singleton Pattern: Centralizzazione dello Stato e Configurazione Globale
In un ecosistema e-commerce, l'integrità dei dati finanziari è prioritaria. La gestione di parametri critici come l'IVA, la valuta e gli sconti base richiede una "Single Source of Truth" per evitare inconsistenze tra i diversi moduli di calcolo.
Il sistema risolve questa necessità tramite la classe AppContext, implementata come Singleton. La classe è dichiarata sealed per impedirne l'ereditarietà e presenta un costruttore private, garantendo che l'istanza sia unica e controllata. L'accesso avviene esclusivamente tramite la proprietà statica getInstance, che utilizza un controllo di esistenza (if (_instance == null)) per l'inizializzazione lazy.
Le proprietà gestite includono vincoli di validazione stringenti per assicurare la qualità del dato:
Valuta: Accetta esclusivamente codici di 3 caratteri (es. "EUR", "USD"), validando la proprietà value.Length == 3.
IVA: Implementa una logica di fallback robusta; se il valore fornito non è superiore a zero, il sistema imposta automaticamente un valore di default pari a 23.0d.
ScontoBase: Gestisce la percentuale di sconto applicabile globalmente, soggetta a validazione per prevenire valori negativi.
Dal punto di vista architettonico, l'uso del Singleton assicura che ogni componente del sistema interroghi la medesima configurazione, facilitando aggiornamenti massivi delle politiche fiscali senza rifattorizzare i moduli periferici. Questa stabilità configurativa è il prerequisito per la definizione della gerarchia dei prodotti core.
3. Definizione dei Prodotti Core e Interfaccia IProduct
La struttura del catalogo ModShop poggia sull'interfaccia IProduct, che funge da contratto fondamentale per tutti i beni commerciabili. Questa astrazione definisce i metodi GetName() e GetPrice(), consentendo al sistema di trattare in modo polimorfico magliette, tazze o accessori durante le fasi di calcolo del carrello.
I prodotti base attualmente censiti nel sistema sono:
Codice Prodotto
Descrizione Prodotto
Prezzo Base
TSHIRT
Maglietta in cotone
20.0
MUG
Tazza in ceramica
10.0
SKIN
Pellicola protettiva
5.0
L'astrazione tramite interfaccia permette al motore di calcolo di iterare su una lista di oggetti IProduct e calcolare il totale senza conoscere le classi concrete (TShirt, Mug, Skin). Tuttavia, per superare la staticità dei prodotti base e permettere la personalizzazione richiesta dal business, il sistema evolve verso un modello dinamico.
4. Decorator Pattern: Estensibilità Dinamica e Personalizzazione
Per implementare opzioni aggiuntive (come stampe o incisioni) rispettando il principio Open/Closed, è stato adottato il pattern Decorator. Invece di creare una sottoclasse per ogni possibile combinazione di prodotto e opzione, abbiamo utilizzato la composizione rispetto all'ereditarietà.
La classe astratta ProductDecorator implementa IProduct e contiene al suo interno un riferimento protetto _product. Questo permette di "incapsulare" un prodotto esistente, aggiungendo comportamenti o costi in modo incrementale. I decoratori disponibili nel sistema includono:
StampaFronte: Costo addizionale di +3.5.
StampaRetro: Costo addizionale di +3.2.
ConfezioneRegalo: Costo addizionale di +1.5.
Incisione: Costo addizionale di +4.0.
EstensioneGaranzia: Costo addizionale di +5.5.
L'impatto tecnico è significativo: il sistema può generare una Mug con Incisione e ConfezioneRegalo semplicemente stratificando gli oggetti. Questo approccio evita l'esplosione combinatoria delle classi e permette di aggiungere nuove opzioni di personalizzazione semplicemente creando un nuovo decoratore, senza toccare il codice dei prodotti core. Una volta definito il prodotto "composto", la logica di calcolo finale viene delegata alle strategie di pricing.
5. Strategy Pattern: Flessibilità nelle Logiche di Pricing
La determinazione del prezzo finale è una logica suscettibile di frequenti cambiamenti (campagne marketing, vendite all'ingrosso, normative fiscali). Lo Strategy Pattern permette di isolare l'algoritmo di calcolo dalla struttura del prodotto.
Le strategie implementate, che ereditano dall'interfaccia IStrategia, vengono istanziate passando i parametri correnti dal Singleton AppContext. Le varianti principali sono:
StandarndPricing: Calcola il prezzo ivato utilizzando la formula prezzo * ((iva / 100) + 1). (Nota: il naming mantiene la compatibilità con il sistema legacy).
PromoPricing: Valuta se il prezzo ivato supera una determinata sogliaSconto per applicare una riduzione di prezzo.
WholesalePricing: Applica logiche di abbattimento costi dedicate ai grandi volumi d'acquisto.
DynamicPricing: Utilizza un flag booleano isDynamic per decidere a runtime se sottrarre uno scontoApplicabile dal totale ivato.
L'integrazione tra AppContext e IStrategia è gestita tramite il metodo SetStrategia. Questo consente al sistema di cambiare comportamento dinamico a runtime: ad esempio, attivando la PromoPricing durante un evento di vendita stagionale senza modificare una singola riga di codice nel catalogo prodotti.
6. Observer Pattern: Disaccoppiamento della Notifica e Logging
Un'architettura enterprise deve essere reattiva. Nel sistema ModShop, ogni variazione di stato di un prodotto (creazione, modifica, rimozione) deve essere comunicata a sottosistemi indipendenti come il logging di sistema o l'interfaccia utente.
L'Observer Pattern gestisce questa reattività tramite l'interfaccia IProductObserver, che definisce il metodo void Aggiorna(IProduct product, string messaggio). Le classi concrete che implementano questa interfaccia sono:
Log: Registra i dettagli tecnici dell'operazione.
UI: Aggiorna gli elementi visuali per l'utente finale.
Mock: Fornisce un endpoint per i test di integrazione.
Ogni classe prodotto (Mug, Skin, TShirt) mantiene una lista interna di osservatori (lstObs). Quando viene invocato un evento, il prodotto notifica tutti i soggetti registrati inviando un messaggio contestuale. Questo disaccoppiamento garantisce che il prodotto non debba conoscere i dettagli implementativi dei sistemi di log o delle interfacce, migliorando drasticamente la modularità e la facilità di test.
7. Analisi dei Vantaggi e Conclusioni Tecniche
L'adozione sinergica di questi quattro Design Pattern ha trasformato ModShop in una piattaforma professionale, pronta per la scalabilità orizzontale e verticale. L'architettura non è più una sequenza di istruzioni, ma un insieme di componenti intelligenti che collaborano tramite interfacce definite.
In sintesi, i benefici ottenuti sono:
Scalabilità: L'aggiunta di nuovi prodotti o decoratori avviene per estensione, non per modifica.
Flessibilità: Il sistema può cambiare logiche di pricing a runtime tramite l'iniezione di strategie nel contesto globale.
Robustezza: La centralizzazione tramite Singleton e la validazione dei dati (come il fallback dell'IVA al 23%) prevengono errori a catena nel calcolo finanziario.
Manutenibilità: La netta separazione delle responsabilità permette di intervenire su un modulo (es. le notifiche) senza il rischio di regressioni sul calcolo dei prezzi o sulle personalizzazioni.
In conclusione, la solidità dell'architettura ModShop fornisce una base tecnica d'eccellenza, capace di supportare le future evoluzioni del business con un debito tecnico ridotto al minimo e un'altissima manutenibilità del codice.