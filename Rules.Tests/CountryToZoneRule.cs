using System.Collections.Generic;

namespace Rules.Tests
{
    internal class CountryToZoneRule : Rule
    {
        private Zones _zones = new Zones();

        public CountryToZoneRule(string from, string to, DocumentType requiresDoc)
            : base(from, to, requiresDoc, 0m, decimal.MaxValue)
        {
        }

        internal override bool Applies(string from, string to, decimal shipmentValue)
        {
            if (!from.Equals(From))
            {
                return false;
            }

            if(!_zones.ZoneContainsCountry(To, to)) 
            {
                return false;
            }
            
            return base.Applies(from, to, shipmentValue);
        }
    }

    public class Zones : Dictionary<string, HashSet<string>>
    {
        public Zones ()
        {
            this.Add("EU", new HashSet<string>() { "DK", "FR", "GE" });
        }

        public bool ZoneContainsCountry(string zone, string country) 
        {
            if(!this.ContainsKey(zone))
            {
                return false;
            }
            
            return this[zone].Contains(country);
        }
    }
}
