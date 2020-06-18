using System.Collections.Generic;

namespace Rules.Tests
{
    internal class CountryToZoneRule : Rule
    {
        private HashSet<string> _countries;

        private Rule next;

        public CountryToZoneRule(string from, string to, HashSet<string> countries, DocumentType requiresDoc)
            : base(from, to, requiresDoc, 0m, decimal.MaxValue)
        {
            _countries = countries;
        }

        public override int Specificity => 200;

        internal override bool Applies(string from, string to, decimal shipmentValue)
        {
            if (!from.Equals(From))
            {
                return false;
            }

            if (!_countries.Contains(to))
            {
                return false;
            }

            return base.Applies(from, to, shipmentValue);
        }
    }
}
