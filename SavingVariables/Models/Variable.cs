using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables.Models
{
    public class Variable
    {
        [Key] //Primary key in table
        public int VariableId { get; set; }

        [Required] //Will not allow null entries for column
        public string Name { get; set; }

        [Required] //Will not allow null entries
        public int Value { get; set; }
    }
}
