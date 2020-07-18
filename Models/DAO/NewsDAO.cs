using Models.EFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class NewsDAO
    {
        public ComesticEntities dbContext = null;
        public NewsDAO()
        {
            dbContext = new ComesticEntities();
        }
        public List<News> ListAll()
        {
            return dbContext.News.ToList();
        }
        public News ListId(long id)
        {
            return dbContext.News.Find(id);
        }
    }
}
