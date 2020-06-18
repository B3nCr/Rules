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

    public static class ZoneRepo
    {
        static ZoneRepo()
        {
            Zones = new Dictionary<string, HashSet<string>>();
            Zones.Add("EU", new HashSet<string>() { "DK", "FR", "GE" });
            Zones.Add("Non-Matching-Zone", new HashSet<string>());
        }

        public static Dictionary<string, HashSet<string>> Zones { get; }
    }
}
