using Optional;
using System;
using System.Linq;

namespace optional_of_t
{
    public class DateFilter : IItemFilter
    {
        private readonly Option<DateTime> createdAt;

        public DateFilter(Option<DateTime> createdAt) => this.createdAt = createdAt;

        public IQueryable<Item> Filter(IQueryable<Item> items)
        {
            return createdAt.Match(
                some => items.Where(i => i.CreatedAt > some),
                () => items);
        }
    }
}
