using ToytoyStoreBackend.Entities.Base;

namespace ToytoyStoreBackend.Entities
{
    public class BuyBasketItem : Thing
    {
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public  required BuyBasket BuyBasket { get; set; } 
        public int ProductId { get; set; }
        public  required Product Product { get; set; } 
    }
}