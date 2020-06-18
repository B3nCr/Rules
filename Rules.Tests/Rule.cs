using System;

namespace Rules.Tests
{
    internal abstract class Rule
    {
        public Rule(string from, string to, DocumentType requiresDoc, decimal min, decimal max)
        {
            this.From = from;
            this.To = to;
            this.RequiresDoc = requiresDoc;
            Min = min;
            Max = max;
        }

        public string From { get; }
        public string To { get; }
        public DocumentType RequiresDoc { get; }
        public decimal Min { get; }
        public decimal Max { get; }

        internal virtual bool Applies(string from, string to, decimal shipmentValue)
        {
            return  shipmentValue > Min && shipmentValue < Max;
        }
    }
}
