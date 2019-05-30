using NLog;
using OnlineCv.WebUI.Models;
using OnlineCv.WebUI.Models.Entity;
using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;

namespace OnlineCv
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class OnlineExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        //Logger logger;
        //public OnlineExceptionFilterAttribute(Logger logger)
        //{
        //    this.logger = logger;
        //}
        public void OnException(ExceptionContext filterContext)
        {
            var area = "";//todo....

            var controller = (filterContext.RouteData.Values["controller"] ?? "").ToString();
            var action = (filterContext.RouteData.Values["action"] ?? "").ToString();

            //səbəbkar erroru tapmaq
            while (filterContext.Exception.InnerException != null)
                filterContext.Exception = filterContext.Exception.InnerException;

            var model = new HandleErrorInfo(filterContext.Exception, controller, action);

            try
            {
                using (var db = new OnlineCvDbContext())
                {
                    var entity = new ErrorHistory();
                    if (!string.IsNullOrWhiteSpace(area))
                        entity.AreaName = area;
                    if (!string.IsNullOrWhiteSpace(controller))
                        entity.ControllerName = controller;
                    if (!string.IsNullOrWhiteSpace(action))
                        entity.ActionName = action;

                    if (filterContext.Exception is HttpException)
                        entity.ErrorCode = (filterContext.Exception as HttpException).GetHttpCode();
                    else if (filterContext.Exception is SqlException)
                    {
                        entity.ErrorCode = (filterContext.Exception as SqlException).Number;
                        entity.ErrorMessage = filterContext.Exception.Message;
                    }
                    else
                        entity.ErrorMessage = filterContext.Exception.Message;

                    logger.Fatal(entity.ErrorMessage);
                    entity.CreationDate = DateTime.UtcNow;
                    db.ErrorHistory.Add(entity);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }

            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml",
                //MasterName = "~/Areas/Admin/Views/Shared/_Layout.cshtml",
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                TempData = filterContext.Controller.TempData
            };

            filterContext.ExceptionHandled = true;
        }
    }
}