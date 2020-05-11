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
        public Rule[] Rules { get; set; }
        
        public Cell DefaultCellProperties { get; set; }

        // If true then cell will update to default
        // if no proper rule was found (if EmptyRule was returned) 
        public bool ShouldCellUpdateToDefault { get; set; }
        
        public RuleSet(Rule[] ruleSet, Cell defaultCellProperties, bool shouldCellUpdateToDefault = false)
        {
            Rules = ruleSet;
            DefaultCellProperties = defaultCellProperties;
            ShouldCellUpdateToDefault = shouldCellUpdateToDefault;
        }
        
        /// <summary>
        /// Finds a proper rule for a passed neighborhood
        /// </summary>
        /// <param name="neighborhood"></param>
        /// <returns></returns>
        public Rule FindProperRule(Cell[] neighborhood)
        {
            foreach (Rule rule in Rules)
                if (rule.CheckSuitability(neighborhood))
                    return rule;

            return new EmptyRule(ShouldCellUpdateToDefault);
        }

    }
}
