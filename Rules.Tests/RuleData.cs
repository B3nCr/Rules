namespace Rules.Tests
{
    internal class RuleData
    {
        public RuleData()
        {
        }

        public string Origin { get; set; }
        public string Destination { get; set; }

        public string OriginZone { get; set; }
        public string DestinationZone { get; set; }

        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }

        public virtual DocumentType DocumentType { get; set; }
    }
}