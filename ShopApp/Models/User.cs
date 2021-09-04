using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 2)]
        [Required(ErrorMessage = "Please enter your username.")]
        [RegularExpression("^[A-Z][a-zA-Z ]*$", ErrorMessage = "Username must contains only letters and start with one uppercase letter")]
        public string Username { get; set; }

        [StringLength(15, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter first name.")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter last name.")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter last name.")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone.")]
        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        public String PhoneNumber { get; set; }

        public UserType Type { get; set; } = UserType.Client;

        // one <-> one : Cart <-> User
        public Cart Cart { get; set; }
    }

    public enum UserType
    {
        Client,
        Admin
    }
}
