using Library.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models {

    public class AskViewModel {
        [Required(ErrorMessage = "Proszę podać nazwę tematu")]
        [Display(Name = "Temat")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proszę podać treść wiadomości")]
        [Display(Name = "Treść")]
        public string Content { get; set; }
    }
    public class ReplyViewModel {
        [Required(ErrorMessage = "Proszę podać treść wiadomości")]
        [Display(Name = "Treść")]
        public string Content { get; set; }
    }

    public class ConversationViewModel {
        public IEnumerable<Message> Messages { get; set; }
        public ReplyViewModel ReplyViewModel { get; set; }
    }
}