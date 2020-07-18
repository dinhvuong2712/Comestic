using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comestics.Models
{
    public class SearchModel
    {
        public string keyword { get; set; }
        public int currentPage { get; set; }
        public int pageSize { get; set; }
    }
}