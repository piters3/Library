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

        [Required]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        [Display(Name = "Pożyczana książka")]
        public virtual Book Book { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [Display(Name = "Użytkownik")]
        public virtual ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Proszę datę wypożyczenia książki")]
        [Display(Name = "Data wypożyczenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BorrowDate { get; set; }

        [Display(Name = "Data oddania")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReturnDate { get; set; }

        [Display(Name = "Dni do oddania")]
        public int? DaysLeft { get; set; }

        [Display(Name = "Grzywna")]
        public int? FinedAmount { get; set; }

        [Display(Name = "Uwagi")]
        public string Remarks { get; set; }
    }
}