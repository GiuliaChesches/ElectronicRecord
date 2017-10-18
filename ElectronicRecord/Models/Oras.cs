using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectronicRecord.Models
{
    public class Oras
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string JudetID { get; set; }
        [Required]
        [StringLength(30)]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "Trebuie sa contina doar litere")]
        public string Denumire { get; set; }

        public virtual Judet Judet { get; set; }
        public virtual ICollection<Pacient> Pacient { get; set; }
        public virtual ICollection<Medic> Medic { get; set; }
    }
}