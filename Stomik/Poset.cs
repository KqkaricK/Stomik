using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stomik
{
    [Table("poset")]
    class Poset
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("FIO")]
        public string FIO { get; set; }
       
        [Column("Pas")]
        public long Pas { get; set; }
        [Column("Nomber")]
        public long Nomber { get; set; }
        [Column("Adress")]
        public string Adress { get; set; }

    }
}
