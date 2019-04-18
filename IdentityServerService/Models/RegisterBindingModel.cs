using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace IdentityServerService.Models
{
    public class RegisterBindingModel
    {
        [Required]
        public string UserName { get; set; }
        
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(150)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}