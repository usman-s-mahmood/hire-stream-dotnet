using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireStreamDotNetProject.Models {
    public class JobPost {
        public int Id {get; set;}
        [Required]
        [MaxLength(255)]
        public string? Title {get; set;}
        [Required]
        public string? Content {get; set;}
        public DateTime PostDate {get; set;} = DateTime.Now;
        public int UserId {get; set;}
        [ForeignKey("UserId")]
        public User User {get; set;}
        [Required]
        public string? Location;
        [Required]
        public string? JobType;

    }
}