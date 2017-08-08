using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library.Models;

namespace Library.Entities {
    public class Lending {
        [Key]
        public int LendId { get; set; }

        public int BookCopyId { get; set; }
        [ForeignKey("BookCopyId")]
        [Display(Name = "Pożyczana książka")]
        public virtual BookCopy BookCopy { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [Display(Name = "Użytkownik")]
        public virtual ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Proszę datę wypożyczenia książki")]
        [Display(Name = "Data wypożyczenia")]
        public DateTime BorrowDate { get; set; }

        [Required(ErrorMessage = "Proszę datę oddania książki")]
        [Display(Name = "Data oddania")]
        public DateTime ReturnDate { get; set; }

        [Display(Name = "Grzywna")]
        public int FinedAmount { get; set; }

        [Display(Name = "Uwagi")]
        public string Remarks { get; set; }
    }
}