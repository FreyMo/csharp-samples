using Optional;

namespace optional_of_t
{
    public static class NullableTypeExtensions
    {
        public static Option<T> AsOption<T>(this T? nullable) where T : struct
        {
#pragma warning disable CS8629 // Nullable value type may be null.
            return nullable.SomeWhen(x => x.HasValue).Map(x => x.Value);
#pragma warning restore CS8629 // Nullable value type may be null.
        }

        public static Option<T> AsOption<T>(this T? nullable) where T : class
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return nullable.SomeNotNull<T>();
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}
