using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables.Models;

namespace SavingVariables.DAL
{
    public class VariablesRepository
    {
        public VariablesContext Context { get; set; } //creating property of "context" with a getter and setter

        public VariablesRepository()
        {
            Context = new VariablesContext();
        }

        public List<Variable> GetVariables()
        {
            throw new NotImplementedException();
        }
    }
}
