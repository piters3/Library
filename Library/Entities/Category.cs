using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities {
    public class Category {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę kategorii")]
        [Display(Name = "Nazwa kategorii")]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}