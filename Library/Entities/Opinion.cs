using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities {
    public class Opinion {
        [Key]
        public int OpinionId { get; set; }

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

        [Required(ErrorMessage = "Proszę podać opinię książki")]
        [Display(Name = "Opinia")]
        public string Content { get; set; }
    }
}