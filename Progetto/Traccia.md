Motore ordini “ModShop”: creazione prodotti, personalizzazioni, strategie di
prezzo e notifiche in tempo reale

Obiettivo

    Progettare e implementare la gestione di ordini di un negozio modulare (es.
    gadget/skin/oggetti digitali) che:

    crei prodotti da un catalogo,

    applichi personalizzazioni opzionali,

    calcoli il prezzo con strategie intercambiabili,

    notifichi in tempo reale i cambiamenti d’ordine agli osservatori,

    centralizzi configurazioni/log tramite un’unica istanza condivisa.

    Pattern obbligatori e mappatura

    Singleton: AppContext (configurazioni globali: valuta, IVA, sconti di base,
    logger/event bus).

    Factory (Factory Method o Abstract Factory): ProductFactory per istanziare
    prodotti concreti da un “product code” (es. "TSHIRT", "MUG", "SKIN").

    Decorator: catena di “addon” opzionali che arricchiscono il prodotto (stampa
    fronte/retro, confezione regalo, estensione garanzia digitale, incisione).

    Strategy: strategie di pricing intercambiabili (es. StandardPricing,
    PromoPricing, WholesalePricing, DynamicPricing).

    Observer: sistema di notifiche per cambi di stato dell’ordine
    (aggiunta/rimozione item, cambio strategia, checkout), con più subscriber (es.
    UI, log, email/sms mock).