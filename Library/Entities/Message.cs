using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Library.Entities {
    public class Message {

        [Key]
        public int MessageId { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Proszę podać treść wiadomości")]
        [Display(Name = "Nazwa")]
        public string Content { get; set; }
   
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        [Display(Name = "Temat")]
        public virtual Subject Subject { get; set; }

        public DateTime Data { get; set; }


    }
}