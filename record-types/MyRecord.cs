namespace record_types
{
    public record MyRecord
    {
        public int MyInt { get; init; }

        public string MyString { get; init; }

        public void Deconstruct(out int myInt, out string myString) => (myInt, myString) = (MyInt, MyString);
    }
}
