using System;
using System.Linq;
using Xunit;

namespace Rules.Tests
{
    public class CountryToZoneTest
    {
        [Fact]
        public void OneCountryToCountryRule()
        {
            var rule = new CountryToCountryRule(from: "GB", to: "FR", requiresDoc: DocumentType.CommercialInvoice);

            var rulesEngine = new RulesEngine(rule);

            var result = rulesEngine.Run(from: "GB", to: "FR", value: 100.10m);

            Assert.Single(result);
            Assert.Contains(DocumentType.CommercialInvoice, result);
        }

        [Fact]
        public void NoDocument()
        {
            var countryToCountry = new CountryToCountryRule(from: "GB", to: "FR", requiresDoc: DocumentType.NoDocument);

            var rulesEngine = new RulesEngine(countryToCountry);

            var result = rulesEngine.Run(from: "GB", to: "FR", value: 100.10m);

            Assert.Empty(result);
        }

        [Fact]
        public void NoDocumentAppliesOnlyToLessSpecificRules()
        {
            var commercialInvoice = new CountryToZoneRule(from: "GB", to: "EU", requiresDoc: DocumentType.CommercialInvoice);
            var noDocument = new CountryToZoneRule(from: "GB", to: "EU", requiresDoc: DocumentType.NoDocument);
            var cn22 = new CountryToCountryRule(from: "GB", to: "FR", requiresDoc: DocumentType.CN22);

            var rulesEngine = new RulesEngine(commercialInvoice, noDocument, cn22);

            var result = rulesEngine.Run(from: "GB", to: "FR", value: 100.10m);

            Assert.Single(result);
            Assert.Contains(DocumentType.CN22, result);
        }

        [Fact]
        public void Test()
        {
            var commercialInvoice = new CountryToZoneRule(from: "GB", to: "EU", requiresDoc: DocumentType.CommercialInvoice);
            var noDocument = new CountryToZoneRule(from: "GB", to: "EU", requiresDoc: DocumentType.NoDocument);
            var cn22 = new CountryToCountryRule(from: "GB", to: "FR", requiresDoc: DocumentType.CN22);

            var rulesEngine = new RulesEngine(commercialInvoice, noDocument, cn22);

            var result = rulesEngine.Run(from: "GB", to: "FR", value: 100.10m);

            Assert.Single(result);
            Assert.Contains(DocumentType.CN22, result);
        }

        [Fact]
        public void DestinationNotInZoneCoveredByRules()
        {
            var rule = new CountryToZoneRule(from: "GB", to: "EU", requiresDoc: DocumentType.CommercialInvoice);

            var rulesEngine = new RulesEngine(rule);

            var result = rulesEngine.Run(from: "GB", to: "US", value: 100.10m);

            Assert.Empty(result);
        }
        
        [Fact]
        public void OneCountryToZoneRule()
        {
            var rule = new CountryToZoneRule(from: "GB", to: "EU", requiresDoc: DocumentType.CommercialInvoice);

            var rulesEngine = new RulesEngine(rule);

            var result = rulesEngine.Run(from: "GB", to: "FR", value: 100.10m);

            Assert.Single(result);
            Assert.Contains(DocumentType.CommercialInvoice, result);
        }

        [Fact]
        public void TwoCountryToZoneRules()
        {
            var invoice = new CountryToZoneRule(from: "GB", to: "EU", requiresDoc: DocumentType.CommercialInvoice);

            var cn22 = new CountryToZoneRule(from: "GB", to: "EU", requiresDoc: DocumentType.CN22);

            var rulesEngine = new RulesEngine(invoice, cn22);

            var result = rulesEngine.Run(from: "GB", to: "FR", value: 100.10m);

            Assert.Equal(2, result.Count());
            Assert.Contains(DocumentType.CommercialInvoice, result);
            Assert.Contains(DocumentType.CN22, result);
        }

        [Fact]
        public void TestRuleValue()
        {
            var rule = new ValueTestRule("GB", "EU", DocumentType.CN22, 0m, 100m);

            var rulesEngine = new RulesEngine(rule);

            var result = rulesEngine.Run(from: "GB", to: "FR", value: 101m);

            Assert.Empty(result);
        }

        internal class ValueTestRule : Rule
        {
            public ValueTestRule(string from, string to, DocumentType requiresDoc, decimal min, decimal max)
            : base(from, to, requiresDoc, min, max)
            {
            }
        }
    }
}
