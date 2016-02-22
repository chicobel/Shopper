using Shopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopper
{
    public static class ShoppingList
    {
        static List<ShoppingItem> m_Items = new List<ShoppingItem>();
        public static List<ShoppingItem> Items { get { return m_Items; } set { m_Items = value; } }

        static ShoppingList()
        {
            m_Items.Add(new ShoppingItem(new Item() { Name="7up", Type="Drink", Id=1 }, 1));
            m_Items.Add(new ShoppingItem(new Item() { Name = "Coke", Type = "Drink",Id=2 }, 1));
            m_Items.Add(new ShoppingItem(new Item() { Name = "Fanta", Type = "Drink",Id=3 }, 1));
            m_Items.Add(new ShoppingItem(new Item() { Name = "DrPepper", Type = "Drink",Id=4 }, 1));
        }

        public static bool Add(Item itm, int q)
        {
            var len = Items.Count;
            if (ExistsInList(itm.Name))
            {
                var si = Items.FirstOrDefault(p => p.Item.Name == itm.Name);
                si.Quantity += q;
                return true;
            }
            else
            {
                itm.Id = len > 0 ? Items.Max(p => p.Item.Id) + 1 : 1;
                Items.Add(new ShoppingItem(itm, q));
                return Items.Count > len;
            }
        }

        public static bool Delete(string name)
        {
            var len = Items.Count;
            var sitem = Items.FirstOrDefault(p=>p.Item.Name == name);
            if(sitem == null)
                return false;
            var idx = Items.IndexOf(sitem);
            Items.Remove(sitem);
            return Items.Count < len;
        }

        public static bool UpdateQuantity(string itemName, int newq)
        {
            var len = Items.Count;
            var sitem = Items.FirstOrDefault(p => p.Item.Name == itemName);
            if (sitem == null)
                return false;
            sitem.Quantity = newq;

            return sitem.Quantity == newq;
        }


        public static bool ExistsInList(string itemName)
        {
            return Items.FirstOrDefault(p => p.Item.Name == itemName) != null;
        }
    }
}