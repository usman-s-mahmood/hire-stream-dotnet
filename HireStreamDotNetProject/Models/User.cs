using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireStreamDotNetProject.Models {
    public class User {
        public int Id {get; set;}
        [Required]
        
        [MinLength(8, ErrorMessage = "Username must be >= 8")]
        public string? Username { get; set; }
        [Required]

        public string? FirstName { get; set; }
        [Required]

        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Password { get; set; }
        public bool IsActive {get; set;} = true;
        public bool IsStaff {get; set;} = false;
        public bool IsAdmin {get; set;} = false;
        public DateTime RegisterDate {get; set;} = DateTime.Now;
        public string ProfilePic {get; set;} = "";
        public string? AboutUser {get; set;} = "";
        public string? SocialLink {get; set;} = "";
        public string UserRole {get; set;} = "applicant"; // Either Recruiter or Applicant
    }
}