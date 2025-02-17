using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireStreamDotNetProject.Models {
    public class Contact {
        [Key]
        public int Id {get; set;}
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Email {get; set;}
        [Required]
        public string? Message {get; set;}

        public DateTime SubmitTime {get; set;} = DateTime.Now;
        public bool IsSent {get; set;} = false;
    }
}