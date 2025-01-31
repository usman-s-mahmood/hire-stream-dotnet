
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireStreamDotNetProject.Models {
    public class JobApplication {
        [Key]
        public int Id { get; set; }
        public int JobPostId { get; set; }
        [ForeignKey("JobPostId")]
        public JobPost JobPost { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [MaxLength(255)]
        public string? CoverLetter { get; set; }

        public string? ResumeUrl { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending"; 

        public DateTime AppliedOn { get; set; } = DateTime.Now; 
    }
}