using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class PageModel
    {
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public int firstPage { get; set; }
        public int lastPage { get; set; }
        public int numberPage { get; set; }
    }
}
