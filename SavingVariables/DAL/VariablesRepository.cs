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

        public VariablesRepository(VariablesContext _context) //dependency injection, like bringing in the paint for a paint job, the shop only has to do the work with the materials provided
        {
            Context = _context; //sets Context as _context to pass in VariablesContext
        }

        public List<Variable> GetVariables()
        {
            return Context.Variables.ToList();
        }
    }
}
