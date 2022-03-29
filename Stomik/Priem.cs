using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stomik
{
    [Table("priem")]
    class Priem
    {
        [Key]
        [Column("kod_priem")]
        public int kod_priem { get; set; }
        [Column("kod_pos")]
        public int kod_pos { get; set; }
        [ForeignKey("kod_pos")]

        public Poset Poset { get; set; }

        [Column("kod_vrach")]
        public int kod_vrach { get; set; }
        [ForeignKey("kod_vrach")]

        public Vrach Vrach { get; set; }

        [Column("kod_yslugi")]
        public int kod_yslugi { get; set; }
        [ForeignKey("kod_yslugi")]

        public Yslygi Yslygi { get; set; }

        [Column("date")]
        public string date { get; set; }
        [Column("Otmetka")]
        public bool Otmetka { get; set; }
        [Column("diagnos")]
        public string diagnos { get; set; }
        
    }
}
