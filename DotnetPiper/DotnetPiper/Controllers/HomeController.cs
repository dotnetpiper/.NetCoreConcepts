using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotnetPiper.Services;

namespace DotnetPiper.Controllers
{
    public class HomeController : Controller
    {
        //[MiddlewareFilter(typeof(SecurityMiddleware))]
        public IActionResult Component()
        {
            //ArticlesService articlesService = new ArticlesService();
            //var result = articlesService.GetRecentArticles();
            //return View("~/Views/Home/Components/RecentArticles/Default.cshtml", result);
             return ViewComponent("RecentArticles");
            //return View("~/Views/Home/Components/RecentArticles/Default.cshtml");
        }
    }
}