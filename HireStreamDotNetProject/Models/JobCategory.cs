using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireStreamDotNetProject.Models {
    public class JobCategory {
        public int Id {get; set;}
        [Required]
        public string? Name {get; set;}
        public int UserId;
        [ForeignKey("UserId")]
        public User User;
        public DateTime AddedOn {get; set;} = DateTime.Now;
    }
}