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
        public readonly Rule[] _ruleSet;
        
        public readonly Cell _defaultCellProperties;

        // If true then cell will update to default
        // if no proper rule was found (if EmptyRule was returned) 
        public readonly bool _shouldCellUpdateToDefault;
        
        public RuleSet(Rule[] ruleSet, Cell defaultCellProperties, bool shouldCellUpdateToDefault = false)
        {
            _ruleSet = ruleSet;
            _defaultCellProperties = defaultCellProperties;
            _shouldCellUpdateToDefault = shouldCellUpdateToDefault;
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

            return new EmptyRule(_shouldCellUpdateToDefault);
        }

    }
}
