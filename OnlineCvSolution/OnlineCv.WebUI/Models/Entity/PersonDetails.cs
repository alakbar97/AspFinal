using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vizew.WebUI.Models.Entity;

namespace OnlineCv.WebUI.Models.Entity
{
    public class PersonDetails:BaseEntity
    {
        [Required]
        [StringLength(70)]
        public string Name { get; set; }

        public int Age { get; set; }

        [Required]
        [StringLength(70)]
        public string Location { get; set; }

        public string Experience { get; set; }

        [Required]
        [StringLength(70)]
        public string Degree { get; set; }

        public string MediaUrl { get; set; }

        public string CareerLevel { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

    }
}