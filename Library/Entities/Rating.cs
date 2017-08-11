using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library.Entities {
    public class Rating {
        [Key]
        public int RatingId { get; set; }

        [Required]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        [Display(Name = "Oceniana książka")]
        public virtual Book Book { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [Display(Name = "Użytkownik")]
        public virtual ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Proszę podać ocenę książki")]
        [Display(Name = "Ocena")]
        public int Rate { get; set; }

        [Display(Name = "Opinia")]
        public string Opinion { get; set; }
    }
}