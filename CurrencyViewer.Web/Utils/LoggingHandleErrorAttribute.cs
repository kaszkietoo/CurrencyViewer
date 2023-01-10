using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CurrencyViewer.Web.Utils
{
    public class LoggingHandleErrorAttribute : HandleErrorAttribute
    {
        private readonly string _dateFormat = "yyyy-MM-dd HH:mm:ss";
        public override void OnException(ExceptionContext filterContext)
        {
            var path = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)}\exceptions.txt".Replace("file:\\", "");
            
            //consider thread safety before publishing production
            File.AppendAllText(path, 
                $"{DateTime.Now.ToString(_dateFormat)} {filterContext.Exception.Message}{Environment.NewLine}{filterContext.Exception.StackTrace}{Environment.NewLine}");
            
            base.OnException(filterContext);
        }
    }
}