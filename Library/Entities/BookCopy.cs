using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities {
    public class BookCopy {
        [Key]
        public int BookCopyId { get; set; }

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        [Display(Name = "Książka oryginalna")]
        public virtual Book Book { get; set; }

        [Required(ErrorMessage = "Proszę ilość dostępnych egzemplarzy")]
        [Display(Name = "Ilość dostępnych egzemplarzy")]
        public int Amount { get; set; }
    }
}