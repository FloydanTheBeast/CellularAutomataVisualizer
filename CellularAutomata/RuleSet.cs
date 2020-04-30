using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    public class RuleSet
    {
        // List of all rules
        Rule[] _ruleSet;
        
        private readonly Cell _defaultCellProperties;

        public RuleSet(Rule[] ruleSet, Cell defaultCellProperties)
        {
            _ruleSet = ruleSet;
            _defaultCellProperties = defaultCellProperties;
        }
        
        /// <summary>
        /// Finds a proper rule for a passed neighborhood
        /// </summary>
        /// <param name="neighborhood"></param>
        /// <returns></returns>
        public Rule FindProperRule(Cell[] neighborhood)
        {
            foreach (Rule rule in _ruleSet)
                if (rule.CheckSuitability(neighborhood))
                    return rule;

            return new EmptyRule();
        }

    }
}
