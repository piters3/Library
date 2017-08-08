using Library.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Models {
    public class ApplicationUser : IdentityUser {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager) {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        [Display(Name = "Login")]
        public override string UserName { get; set; }

        [Display(Name = "Aktywacja mailem")]
        public override bool EmailConfirmed { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }

        [Display(Name = "Avatar")]
        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }

        [Display(Name = "Aktywność")]
        public bool Enabled { get; set; }

        [Display(Name = "Data rejestracji")]
        public DateTime RegisterDate { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext()
            : base("LibraryDB", throwIfV1Schema: false) {
        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookCopy> BookCopies { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Lending> Lendings { get; set; }


        /*static ApplicationDbContext() {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }*/

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }
    }
}