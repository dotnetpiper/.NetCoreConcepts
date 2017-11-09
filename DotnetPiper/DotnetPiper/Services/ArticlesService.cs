using DotnetPiper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetPiper.Services
{
    public class ArticlesService : IDisposable
    {
        public List<Articles> GetRecentArticles()
        {
            var articleList = new List<Articles>() {
            new Articles {ArticleId = 1,Title="Developing Book My Seat Application In AngularJS And ASP.NET - Usage Of Routing And RouteParams",PublishedDate=Convert.ToDateTime("10/10/2016"),TotalViews=12000,Category="WebAPI" },
            new Articles {ArticleId = 2,Title="WebAPI: Restful CRUD Operations in WebAPI Using ADO.NET Objects and SQL Server",PublishedDate=Convert.ToDateTime("03/02/2016"),TotalViews=226743,Category="WebAPI" },
            new Articles {ArticleId = 3,Title="Life Cycle of TempData in MVC4",PublishedDate=Convert.ToDateTime("08/08/2016"),TotalViews=21000,Category="WebAPI" },
            new Articles {ArticleId = 4,Title="Getting Started With TypeScript Using Visual Studio Code",PublishedDate=Convert.ToDateTime("08/08/2016"),TotalViews=21000,Category="TypeScript" }
                
        };
            return articleList;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
