namespace ModShop.Products
{
    public class TShirt : IProduct
    {
        public string GetName() => "T-Shirt";

        public double GetPrice() => 20;
    }
}