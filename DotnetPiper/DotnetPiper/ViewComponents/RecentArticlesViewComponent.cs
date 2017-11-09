using DotnetPiper.Models;
using DotnetPiper.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetPiper.ViewComponents
{
    [ViewComponent(Name = "RecentArticles")]
    public class RecentArticlesViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
             return View(await GetRecentArticlesAsync());
           //return View("~/Views/Product/Components/RecentArticles/Default.cshtml",await GetRecentArticlesAsync());
        }

        private Task<List<Articles>> GetRecentArticlesAsync()
        {
            ArticlesService articleService = new ArticlesService();
            return Task.FromResult(articleService.GetRecentArticles());
        }
    }
}
