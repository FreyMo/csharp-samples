using Optional;
using System.Linq;

namespace optional_of_t
{
    public class ValueFilter : IItemFilter
    {
        private readonly Option<long> minimum;

        public ValueFilter(Option<long> minimum) => this.minimum = minimum;

        public IQueryable<Item> Filter(IQueryable<Item> items)
        {
            return items.Where(i => i.Value >= minimum.ValueOr(long.MinValue));
        }
    }
}
