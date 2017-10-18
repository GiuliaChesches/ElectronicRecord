using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectronicRecord.Models
{
    public class Medic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required(ErrorMessage = "Numele este obligatoriu")]
        [StringLength(20)]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "Numele trebuie sa contina doar litere")]
        public string Nume { get; set; }
        [Required(ErrorMessage = "Prenumele este obligatoriu")]
        [StringLength(40)]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "Numele trebuie sa contina doar litere")]
        public string Prenume { get; set; }
        [Required(ErrorMessage = "Adresa este obligatorie")]
        public string Adresa { get; set; }
        [Required]
        public int OrasID { get; set; }
        [Required(ErrorMessage = "Numarul de telefon este obligatoriu")]
        [RegularExpression("^[0-9]{0,15}$", ErrorMessage = "Numarul de telefon trebuie sa contina doar cifre")]
        public string NrTelefon { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage="Adresa de email nu este valida")]
        public string Email { get; set; }
        [Required(ErrorMessage="Numele de utilizator este obligatoriu")]
        [MaxLength(15, ErrorMessage = "Numele de utilizator trebuie sa contina intre 5 si 15 caractere"), MinLength(5)]
        public string NumeUtilizator { get; set; }
        [Required(ErrorMessage="Parola este obligatorie")]
        [MinLength(6,ErrorMessage="Parola trebuie sa contina cel putin 6 caractere")]
        public string Parola { get; set; }

        public virtual Oras Oras { get; set; }
        public virtual ICollection<Programare> Programare { get; set; }
    }
}