using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HireStreamDotNetProject.Models {
    public class User {
        public int Id {get; set;}
        [Required]
        
        [MinLength(8, ErrorMessage = "Username must be >= 8")]
        public string? Username { get; set; }
        [MinLength(8, ErrorMessage = "First Name must be >= 8")]

        public string? FirstName { get; set; }
        [MinLength(8, ErrorMessage = "Last Name must be >= 8")]

        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public bool IsActive {get; set;} = true;
        public bool IsStaff {get; set;} = false;
        public bool IsAdmin {get; set;} = false;
    }
}