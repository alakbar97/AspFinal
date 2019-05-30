using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Vizew.WebUI.Models.Entity;

namespace OnlineCv.WebUI.Models.Entity
{
    public class Admin:BaseEntity
    {
        [Required]
        [StringLength(70)]
        public string Email { get; set; }
        [Required]
        [StringLength(20)]
        public string Password { get; set; }
    }
}