using Models.EFrame;
using PagedList;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Models.DAO
{
    public class DetailCartDAO
    {
        public ComesticEntities dbContext = null;
        public DetailCartDAO()
        {
            dbContext = new ComesticEntities();
        }
        public IEnumerable<DetailCart> ListDetail(int? currentPage, int pageSize = 5)
        {
            return dbContext.DetailCarts.OrderBy(x => x.id).ToPagedList(currentPage ?? 1, pageSize);
        }
        public DetailCart FindDetail(int customerId, int status)
        {
            return dbContext.DetailCarts.SingleOrDefault(x => x.customerId == customerId && x.status == status);
        }
        public List<DetailCart> FindList(int customerId,int status)
        {
            return dbContext.DetailCarts.Where(x => x.customerId == customerId && x.status == status).ToList();
        }
        public int AddDetailOrder(DetailCart orderDetail)
        {
            try
            {
                int res;
                object[] parameter =
                {
                    new SqlParameter("@PrId",orderDetail.prId),
                    new SqlParameter("@OrderId",orderDetail.customerId),
                    new SqlParameter("@Amounts",orderDetail.amounts),
                    new SqlParameter("@Price",orderDetail.price),
                    new SqlParameter("@Status",orderDetail.status)
                };
                res = dbContext.Database.ExecuteSqlCommand("AddDetail_Order @PrId,@OrderId,@Amounts,@Price,@Status", parameter);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public int DeleteCart(int id)
        {
            var find = dbContext.DetailCarts.SingleOrDefault(x => x.id == id);
            var del = dbContext.DetailCarts.Remove(find);
            dbContext.SaveChanges();
            if (del == null)
                return 0;
            else
                return 1;
        }
        public IEnumerable<DetailCart> Search(int status, int? currentPage, int pageSize = 5)
        {
            return dbContext.DetailCarts.OrderBy(x => x.id).Where(x => x.status == status).ToPagedList(currentPage ?? 1, pageSize);
        }
        public int ChangeDetailCart(string changeId,int id)
        {
            var change = dbContext.DetailCarts.SingleOrDefault(x => x.id == id);
            if(change != null)
            {
                if (changeId == "mua")
                {
                    change.status = 2;
                    dbContext.SaveChanges();
                    return 1;
                } 
                else if (changeId == "chot")
                {
                    change.status = 3;
                    dbContext.SaveChanges();
                    return 1;
                }
                else if (changeId == "nhan")
                {
                    change.status = 0;
                    dbContext.SaveChanges();
                    return 1;
                }
                else if(changeId == "huy")
                {
                    change.status = 1;
                    dbContext.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }
    }
}
