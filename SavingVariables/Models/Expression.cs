using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables.Models
{
    public class Expression
    {
        [Key] //Primary key in table
        public int ExpressionId { get; set; }

        [Required] //Will not allow null entries for column
        public string Variable { get; set; }

        [Required] //Will not allow null entries
        public int Constant { get; set; }
    }
}
