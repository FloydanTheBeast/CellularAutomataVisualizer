using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{

    public class CellularAutomata
    {
        // RuleSet
        RuleSet _ruleSet;
        
        // GameField
        GameField _gameField;

        public CellularAutomata(RuleSet ruleSet, GameField gameField)
        {
            _ruleSet = ruleSet;
            _gameField = gameField;
        }
    }
}
