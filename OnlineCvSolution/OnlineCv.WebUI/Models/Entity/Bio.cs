using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vizew.WebUI.Models.Entity;

namespace OnlineCv.WebUI.Models.Entity
{
    public class Bio:BaseEntity 
    {

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int SkillId { get; set; }
        public virtual Skill Skill { get; set; }

        public string Description { get; set; }
        public string SkillLevel { get; set; }
        public string SkillDescription { get; set; }
        public bool IsBar { get; set; }
        public bool IsTag { get; set; }
        
    }
}