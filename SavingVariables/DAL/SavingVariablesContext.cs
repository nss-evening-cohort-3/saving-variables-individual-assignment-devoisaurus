using SavingVariables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables.DAL
{
    public class SavingVariablesContext : DbContext
    {
        public DbSet<Expression> Expressions { get; set; }
    }
}
