using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vizew.WebUI.Models.Entity;

namespace OnlineCv.WebUI.Models.Entity
{
    public class SocialProfiles:BaseEntity
    {
        public string Facebook { get; set; }
        public string Google { get; set; }
        public string Twitter { get; set; }
        public string Linked { get; set; }
        public string Skype { get; set; }
    }
}