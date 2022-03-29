using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stomik
{
    [Table("vrach")]
    class Vrach
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        
        [Column("FIO")]
        public string FIO { get; set; }
        [Column("cabinet")]
        public int cabinet { get; set; }
        [Column("dol")]
        public string dol { get; set; }
    }
}
