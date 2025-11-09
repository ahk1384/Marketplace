namespace Marketplace.Domain
{
    public class Item
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public int Price { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public int Ram { get; private set; }
        public int Storage { get; private set; }

        private Item()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public Item(string name, string description, int price, int ram = 0, int storage = 0)
        {
            Name = name;
            Description = description;
            Price = price;
            Ram = ram;
            Storage = storage;
            CreatedAt = DateTime.UtcNow;
        }

        public bool IsValidForPurchase()
        {
            return Price > 0 && !string.IsNullOrEmpty(Name);
        }

        public void UpdateDetails(string name, string description, int price, int ram, int storage)
        {
            Name = name;
            Description = description;
            Price = price;
            Ram = ram;
            Storage = storage;
        }
    }
}