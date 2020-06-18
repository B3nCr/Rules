namespace Rules.Tests
{
    internal class CountryToZoneRule : Rule
    {
        public CountryToZoneRule(string from, string to, DocumentType requiresDoc)
        : base(from, to, requiresDoc, 0m, decimal.MaxValue)
        {
        }
    }
}
