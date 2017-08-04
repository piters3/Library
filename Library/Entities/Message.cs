using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Library.Entities {
    public class Message {
        public int MessageId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Proszę podać treść wiadomości")]
        [Display(Name = "Nazwa")]
        public string Content { get; set; }

        public int SubjectId { get; set; }

        public string DateTime { get; set; }
    }
}