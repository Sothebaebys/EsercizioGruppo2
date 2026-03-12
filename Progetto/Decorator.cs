using System.Collections.Generic;

public abstract class ProductDecorator : IProduct
{
    protected IProduct _product;

    public ProductDecorator(IProduct product)
    {
        _product = product;
    }

    public abstract double GetPrice();
    public abstract string GetName();

    public void Registra(IProductObserver observer)
    {
        _product.Registra(observer);
    }
    public void Rimuovi(IProductObserver observer)
    {
        _product.Rimuovi(observer);
    }
    public void Notifica(string messaggio)
    {
        _product.Notifica(messaggio);
    }

}

public class StampaFronte : ProductDecorator
{
    public StampaFronte(IProduct product) : base(product) { }

    public override double GetPrice()
    {
        return _product.GetPrice() + 3.5;
    }

    public override string GetName()
    {
        return _product.GetName() + " + Stampa fronte";
    }
}

public class StampaRetro : ProductDecorator
{
    public StampaRetro(IProduct product) : base(product) { }

    public override double GetPrice()
    {
        return _product.GetPrice() + 3.2;
    }

    public override string GetName()
    {
        return _product.GetName() + " + Stampa retro";
    }
}

public class ConfezioneRegalo : ProductDecorator
{
    public ConfezioneRegalo(IProduct product) : base(product) { }

    public override double GetPrice()
    {
        return _product.GetPrice() + 1.5;
    }

    public override string GetName()
    {
        return _product.GetName() + " + Confezione regalo";
    }
}

public class EstensioneGaranzia : ProductDecorator
{
    public EstensioneGaranzia(IProduct product) : base(product) { }

    public override double GetPrice()
    {
        return _product.GetPrice() + 5.5;
    }

    public override string GetName()
    {
        return _product.GetName() + " + Estensione garanzia";
    }
}

public class Incisione : ProductDecorator
{
    public Incisione(IProduct product) : base(product) { }

    public override double GetPrice()
    {
        return _product.GetPrice() + 4.0;
    }

    public override string GetName()
    {
        return _product.GetName() + " + Incisione";
    }
}