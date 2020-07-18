using Models.EFrame;
using PagedList;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class ProducerDAO
    {
        public ComesticEntities dbContext = null;
        public ProducerDAO()
        {
            dbContext = new ComesticEntities();
        }
        public List<Producer> ListAll()
        {
            return dbContext.Producers.ToList();
        }
        public Producer FindProducer(long prodId)
        {
            return dbContext.Producers.Find(prodId);
        }
        public IEnumerable<Producer> PageList(int? currentPage, int pageSize)/*PageModel pageModel*/
        {
            return dbContext.Producers.OrderByDescending(x => x.prodId).ToPagedList(currentPage ?? 1, pageSize);
        }
        public int Update1(Producer producer)
        {
            var update = dbContext.Producers.SingleOrDefault(x => x.prodId == producer.prodId);
            if (update != null)
            {
                update.prodName = producer.prodName;
                update.prodAddress = producer.prodAddress;
                dbContext.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public int Delete(int id)
        {
            var del = dbContext.Producers.SingleOrDefault(x => x.prodId == id);
            if (del.status == 1)
            {
                del.status = 0;
            }
            else
            {
                del.status = 1;
            }
            dbContext.SaveChanges();
            return 1;
        }
    }
}
