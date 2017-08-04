using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Library.Models {
    public class RoleViewModel {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Rola")]
        public string Name { get; set; }
    }

    public class EditUserViewModel {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^\d{2}-\d{3}?$", ErrorMessage = "Nieprawidłowy kod pocztowy")]
        public string PostalCode { get; set; }

        [Display(Name = "Avatar")]
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        [Display(Name = "Aktywacja mailem")]
        public bool EmailConfirmed{ get; set; }

        [Display(Name = "Aktywność")]
        public bool Enabled { get; set; }

        [Display(Name = "Rola")]
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}