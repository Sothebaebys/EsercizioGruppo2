namespace ModShop.Products
{
    public class Skin : IProduct
    {
        public string GetName() => "Game Skin";

        public double GetPrice() => 5;
    }
}