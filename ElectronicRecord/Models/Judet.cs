using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectronicRecord.Models
{
    public class Judet
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string Denumire { get; set; }

        public virtual ICollection<Oras> Oras { get; set; }
    }
}