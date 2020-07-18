using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comestics.Areas.Admin.Data
{
    public class AddNewAccModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }

        public string email { get; set; }
    }
}