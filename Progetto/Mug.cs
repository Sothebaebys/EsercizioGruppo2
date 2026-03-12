public class Mug : IProduct
{
      //LISTA PER OBSERVER
    private List<IProductObserver> lstObs = new();

    public void Registra(IProductObserver observer)
    {
        lstObs.Add(observer);
    }
    public void Rimuovi(IProductObserver observer)
    {
        lstObs.Remove(observer);
    }
    public void Notifica(string messaggio)
    {
        foreach(var ob in lstObs)
        {
            ob.Aggiorna(this, messaggio);
        }
    }
    public string GetName() => "Mug";

    public double GetPrice() => 10;
}
