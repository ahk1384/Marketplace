using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application
{

    public class ItemService : IItemService
    {
        private List<Item> items = new List<Item>();

        
        public bool AddItem(string name, int price, string description, DateTime createdAt)
        {
            Item item = new Item(name, description, price, createdAt);
            items.Add(item);
            return true;
        }

        public bool RemoveItem(int itemId)
        {
            var item = items.FirstOrDefault(i => i.id == itemId);
            if (item != null)
            {
                items.Remove(item);
                return true;
            }
            return false;
        }

        public bool BuyItem(User user, int itemId)
        {
            var item = items.FirstOrDefault(i => i.id == itemId);
            if (item != null && user.Balance >= item.price)
            {
                user.Balance -= item.price;
                items.Remove(item);
                return true;
            }
            return false;
        }
        public IReadOnlyList<Item> GetAllItems()
        {
            return items.AsReadOnly();
        }
    }
}