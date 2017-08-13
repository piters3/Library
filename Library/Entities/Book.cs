using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library.Entities {
    public class Book {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Proszę podać tytuł książki")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Proszę podać autora")]
        [Display(Name = "Autor")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Proszę podać wydawnictwo")]
        [Display(Name = "Wydawnictwo")]
        public string Print { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [Display(Name = "Kategoria")]
        public virtual Category Category { get; set; }

        [Required(ErrorMessage = "Proszę datę wydania książki")]
        [Display(Name = "Data wydania")]
        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }

        [Display(Name = "Ilość stron")]
        public int Pages { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Proszę ilość dostępnych egzemplarzy")]
        [Display(Name = "Ilość dostępnych egzemplarzy")]
        public int Amount { get; set; }

        [Display(Name = "Okładka")]
        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        [Display(Name = "Średnia ocena")]
        public double AverageRatig { get; set; }
    }
}