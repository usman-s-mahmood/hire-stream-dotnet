using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireStreamDotNetProject.Models {
    public class Newsletter {
        [Key]
        public int Id {get; set;}

        [Required]
        public string? email {get; set;}

        public DateTime RegisteredOn{get; set;} = DateTime.UtcNow;
    }
}