using Optional;

namespace optional_of_t
{
    public static class NullableExtensions
    {
        public static Option<T> ToOption<T>(this T? nullable) where T : class
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return nullable.SomeNotNull<T>();
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}
