using OnlineCv.WebUI.Models.Entity;
using System.Collections.Generic;

namespace OnlineCv.WebUI.Models.ViewModel
{
    public class OnlineCvViewModel
    {
        public PersonDetails PersonDetails { get; set; }
        public EmailModel EmailModel { get; set; }
        public Bio Bio { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
    }
}