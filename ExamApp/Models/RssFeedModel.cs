using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamApp.Models
{
    public class FeedModel
    {
        public string id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class FeedDetailModel
    {
        public string Title { get; set; }
        public string HtmlStr { get; set; }
      
    }
}
