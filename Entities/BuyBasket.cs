using ToytoyStoreBackend.Entities.Base;

namespace ToytoyStoreBackend.Entities
{
    public class BuyBasket:Thing
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // هر کاربر یک سبد خرید فعال داره
        public int UserId { get; set; }
        public Member Member { get; set; } = null!;

        public ICollection<BuyBasketItem> Items { get; set; } = new List<BuyBasketItem>();
    }
}