using System.Collections.Generic;
using System.Linq;

namespace Rules.Tests
{
    internal class RulesEngine
    {
        private readonly List<Rule> rules = new List<Rule>();

        public RulesEngine(params Rule[] rule)
        {
            this.rules.AddRange(rule);
        }

        internal IEnumerable<DocumentType> Run(string from, string to, decimal value)
        {
            List<DocumentType> result = new List<DocumentType>();

            foreach (var rule in rules)
            {
                if (rule.RequiresDoc == DocumentType.NoDocument)
                {
                    result.Clear();
                }
                else if (rule.Applies(value))
                {
                    result.Add(rule.RequiresDoc);
                }
            }

            return result;
        }
    }
}
