using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentitySample.Models {

    public class ChangePasswordViewModel {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Aktualne hasło")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi mieć przynajmniej {2} znaków.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("NewPassword", ErrorMessage = "Hasła nie pasują do siebie.")]
        public string ConfirmPassword { get; set; }
    }

    public class EditAccountViewModel {
        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }

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
        public string PostalCode { get; set; }

        [Display(Name = "Avatar")]
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

    }

}