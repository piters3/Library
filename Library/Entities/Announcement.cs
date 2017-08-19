using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Entities {
    public class Announcement {
        [Key]
        public int AnnouncementId { get; set; }

        [Required(ErrorMessage = "Proszę podać tytuł ogłoszenia")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Proszę treść ogłoszenia")]
        [Display(Name = "Treść")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Proszę podać datę ogłoszenia")]
        [Display(Name = "Data ogłoszenia")]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }
    }
}