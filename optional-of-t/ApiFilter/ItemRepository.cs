using Optional;
using System;
using System.Collections.Generic;
using System.Linq;

namespace optional_of_t
{
    public class ItemRepository
    {
        private readonly Item[] items =
        {
            new(1, 55, DateTime.Today),
            new(10000, -5000, DateTime.Today),
            new(999, 5000, DateTime.MinValue)
        };

        public Option<Item> GetItem(long id) => items.FirstOrDefault(i => i.Id == id).AsOption();

        public IEnumerable<Item> GetItems(IItemFilter[] filters)
        {
            return filters.Aggregate(items.AsQueryable(), (i, f) => f.Filter(i));
        }
    }
}
