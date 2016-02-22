using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopper.Models
{
    public class PagedShoppingList
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Count { get; set; }
        public IList<ShoppingItem> Items { get; set; }
    }

    public class MyPagedList<T>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Count { get; set; }
        public IList<T> Items { get; set; }
    }
}