using System.ComponentModel.DataAnnotations;
using Vizew.WebUI.Models.Entity;

namespace OnlineCv.WebUI.Models.Entity
{
    public class Category:BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}