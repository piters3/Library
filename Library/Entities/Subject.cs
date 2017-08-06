using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Entities {
    public class Subject {
        [Key]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę tematu")]
        [Display(Name = "Temat")]
        public string Name { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}