namespace ModShop.Products
{
    public class Mug : IProduct
    {
        public string GetName() => "Mug";

        public double GetPrice() => 10;
    }
}