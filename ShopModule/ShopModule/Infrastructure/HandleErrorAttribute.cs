﻿using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace ShopModule.Infrastructure
{
    /// <summary>
    /// Apply this attribute to controller actions to log the exception via Trace.
    /// </summary>
    /// <remarks>
    /// If ExceptionHandled is true in context then no action will be taken.
    /// Marks ExceptionHandled to true.
    /// </remarks>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Method,
        AllowMultiple = true,
        Inherited = true)]
    public class HandleErrorAttribute : System.Web.Mvc.HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                if (filterContext.Exception != null)
                {
                    Trace.TraceError(filterContext.Exception.ToString());
                    var val = filterContext.Exception.ToString();

                    filterContext.Result = new ViewResult
                    {
                        ViewName = "Error",
                        ViewData = new ViewDataDictionary<string>(val)
                    };
                }
                filterContext.ExceptionHandled = true;
            }
        }
    }
}