using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Rules.Tests
{
    public class RuleFactoryTests
    {
        [Fact]
        public void CountryToZoneBeforeCountryToCountry()
        {
            var ruleData = new List<RuleData>()
            {
                new RuleData() {Origin = "GB", Destination = "FR"},
                new RuleData() {Origin = "GB", DestinationZone = "EU"}
            };

            var ruleFactory = new RuleFactory(ruleData);

            var rules = ruleFactory.GetRules();

            Assert.Equal(2, rules.Count());
            Assert.Equal("EU", rules.First().To);
        }
    }

    internal class RuleFactory
    {
        private List<RuleData> ruleDatas;

        public RuleFactory(List<RuleData> ruleData)
        {
            this.ruleDatas = ruleData;
        }

        internal IEnumerable<Rule> GetRules()
        {
            var rules = new List<Rule>();

            foreach (var ruleData in ruleDatas)
            {
                if (ruleData.DestinationZone != null)
                {
                    rules.Add(new CountryToZoneRule(ruleData.Origin, ruleData.DestinationZone, ZoneRepo.Zones[ruleData.DestinationZone], ruleData.DocumentType));
                }
                else
                {
                    rules.Add(new CountryToCountryRule(ruleData.Origin, ruleData.Destination, ruleData.DocumentType));
                }
            }

            return rules.OrderByDescending(x => x.Specificity);
        }
    }
}