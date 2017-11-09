using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetPiper.Models
{
    public class Articles
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public int TotalViews { get; set; }

        public string Category { get; set; }
    }
}
