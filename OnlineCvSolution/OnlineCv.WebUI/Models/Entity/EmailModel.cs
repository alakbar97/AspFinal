using System.ComponentModel.DataAnnotations;
using Vizew.WebUI.Models.Entity;

namespace OnlineCv.WebUI.Models.Entity
{
    public class EmailModel:BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        
        public string ToMails { get; set; }

        [Required]
        [StringLength(50)]
        public string UserEmail { get; set; }

        public string Subject { get; set; }

        [Required]
        [StringLength(500)]
        public string Body { get; set; }
        public string Answer { get; set; }
        public bool IsRead { get; set; }
    }
}