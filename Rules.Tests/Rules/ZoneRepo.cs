using System.Collections.Generic;

namespace Rules.Tests
{
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
