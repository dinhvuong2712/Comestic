using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comestics.Areas.Admin.Data
{
    public class EditAccModel
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int status { get; set; }
        public string email { get; set; }
    }
}