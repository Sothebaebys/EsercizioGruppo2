public interface IProduct
{
    string GetName();
    double GetPrice();

    //METODI PER OBSERVER
    public void Registra(IProductObserver observer);
    public void Rimuovi(IProductObserver observer);
    public void Notifica(string messaggio);
}

public interface IProductObserver
{
    public void Aggiorna(IProduct product, string messaggio);
}

public class Log : IProductObserver
{
    public void Aggiorna(IProduct product, string messaggio)
    {
        Console.WriteLine($"Log - {product.GetName()} : {messaggio}");
    }
}

public class UI : IProductObserver
{
    public void Aggiorna(IProduct product, string messaggio)
    {
        Console.WriteLine($"UI - {product.GetName()} : {messaggio}");
    }
}

public class Mock : IProductObserver
{
    public void Aggiorna(IProduct product, string messaggio)
    {
        Console.WriteLine($"Mock - {product.GetName()} : {messaggio}");
    }
}