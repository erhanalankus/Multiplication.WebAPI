using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpYeni.Models
{
    public class ScoreSetting
    {
        [Key]
        public int Id { get; set; }
        public int Wood { get; set; }
        public int Copper { get; set; }
        public int Silver { get; set; }
        public int Gold { get; set; }
    }
}
