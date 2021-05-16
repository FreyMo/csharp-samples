using Optional;
using System;
using System.Collections.Generic;

namespace optional_of_t
{
    public class ItemApi
    {
        private readonly ItemRepository repo;

        public ItemApi(ItemRepository repo) => this.repo = repo;

        public Option<Item> GetItem(long id) => id switch
        {
            > 0 and < 10000000 => repo.GetItem(id),
            { } => Option.None<Item>()
        };

        public IEnumerable<Item> GetItems(long? minimum = null, DateTime? createdAt = null)
        {
            var filters = new IItemFilter[]
            {
                new ValueFilter(minimum.ToOption()),
                new DateFilter(createdAt.ToOption()),
            };

            return repo.GetItems(filters);
        }
    }
}
