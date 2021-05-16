using System.Linq;

namespace optional_of_t
{
    public interface IItemFilter
    {
        public IQueryable<Item> Filter(IQueryable<Item> items);
    }
}
