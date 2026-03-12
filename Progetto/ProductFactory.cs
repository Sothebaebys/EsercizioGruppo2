namespace ModShop.Products
{
    public static class ProductFactory
    {
        public static Product CreateProduct(string code)
        {
            switch (code)
            {
                case "TSHIRT":
                    return new TShirt();

                case "MUG":
                    return new Mug();

                case "SKIN":
                    return new Skin();

                default:
                    throw new ArgumentException("Unknown product");
            }
        }
    }
}