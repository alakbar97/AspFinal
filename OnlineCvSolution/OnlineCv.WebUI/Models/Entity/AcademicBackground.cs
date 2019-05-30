using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vizew.WebUI.Models.Entity;

namespace OnlineCv.WebUI.Models.Entity
{
    public class AcademicBackground:BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string Qualification { get; set; }
        [Required]
        [StringLength(20)]
        public string Degree { get; set; }
        [Required]
        [StringLength(50)]
        public string UniversityName { get; set; }
        public string YearofObtention { get; set; }
        public string Details { get; set; }
        public string mediaUrl { get; set; }
    }
}