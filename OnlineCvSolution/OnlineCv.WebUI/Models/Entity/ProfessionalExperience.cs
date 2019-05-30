using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vizew.WebUI.Models.Entity;

namespace OnlineCv.WebUI.Models.Entity
{
    public class ProfessionalExperience:BaseEntity
    {
        [Required]
        public string Duration { get; set; }
        [Required]
        [StringLength(50)]
        public string JobTitle { get; set; }
        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }
        public string MediaUrl { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
    }
}