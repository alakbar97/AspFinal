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
        public string Qualification { get; set; }
        [Required]
        public string Degree { get; set; }
        public string UniversityName { get; set; }
        public string YearofObtention { get; set; }
        public string Details { get; set; }
        public string mediaUrl { get; set; }
    }
}