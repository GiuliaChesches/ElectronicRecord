using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectronicRecord.Models
{
    public class Pacient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required(ErrorMessage="Numele este obligatoriu")]
        [StringLength(20)]
        [RegularExpression("[a-zA-Z ]*$",ErrorMessage="Numele trebuie sa contina doar litere")]
        public string Nume { get; set;}
        [Required(ErrorMessage = "Prenumele este obligatoriu")]
        [StringLength(40)]
        [RegularExpression("[a-zA-Z ]*$",ErrorMessage = "Numele trebuie sa contina doar litere")]
        public string Prenume { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Adresa este obligatorie")]
        public string Adresa { get; set; }
        [Required]
        public int OrasId {get; set; }
        [Required(ErrorMessage = "Data nasterii este obligatorie")]
        [DataType(DataType.Date, ErrorMessage="")]
 		public DateTime DataNasterii { get; set;}
        [Required(ErrorMessage = "Numarul de telefon este obligatoriu")]
        [RegularExpression("^[0-9]{0,15}$", ErrorMessage = "Numarul de telefon trebuie sa contina doar cifre")]
        public string NrTelefon { get; set; }

        public virtual Oras Oras { get; set; }
        public virtual ICollection<Programare> Programare { get; set; }
    }
}