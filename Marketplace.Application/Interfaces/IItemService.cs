using System;
using Marketplace.Domain;

namespace Marketplace.Application
{
    public interface IItemService
    {
        public bool AddItem(string name, int price, string description, DateTime createdAt);
        public bool RemoveItem(int itemId);
        public bool BuyItem(User user, int itemId);
        public IReadOnlyList<Item> GetAllItems();
    }
}