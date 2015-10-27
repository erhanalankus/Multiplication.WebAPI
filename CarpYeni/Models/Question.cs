using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpYeni.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? SolveDate { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }
        public int Max { get; set; }
        public int? SolveSeconds { get; set; }
        public bool IsChecked { get; set; }
    }
}
