
namespace HireStreamDotNetProject.Models {
    public class User {
        public int Id {get; set;}
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public bool IsActive {get; set;} = true;
        public bool IsStaff {get; set;} = false;
        public bool IsAdmin {get; set;} = false;
    }
}