namespace Rules.Tests
{
    internal class CountryToCountryRule : Rule
    {
        public CountryToCountryRule(
            string from,
            string to,
            DocumentType requiresDoc) 
            : base(from, to, requiresDoc, 0m, decimal.MaxValue)
        {
        }

         public CountryToCountryRule(
            string from,
            string to,
            DocumentType requiresDoc,
            decimal min, 
            decimal max) 
            : base(from, to, requiresDoc, min, max)
        {
        }
    }
}