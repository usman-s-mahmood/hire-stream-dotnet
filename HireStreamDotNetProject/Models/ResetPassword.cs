using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireStreamDotNetProject.Models {
    public class ResetPassword {
        [Key]
        public int Id {get; set;}
        [Required]
        public Guid Token {get; set;}
        public DateTime GeneratedOn {get; set;} = DateTime.UtcNow;

        public DateTime Expiration {get; set;} = DateTime.UtcNow.AddHours(12);
        [Required]
        public int UserId {get; set;}
        [ForeignKey("UserId")]
        public User? User {get; set;}
        public bool IsUsed {get; set;} = false;
    }
}