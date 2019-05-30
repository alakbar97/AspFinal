using OnlineCv.WebUI.AppCode.Constant;
using System.Web.Mvc;

namespace OnlineCv.WebUI.Areas.Admin.Controllers
{
    public class MainController:Controller
    {
        public Models.Entity.Admin currentUser
        {
            get
            {
                return Session[SessionKey.User] as Models.Entity.Admin;
            }
        }
    }
}