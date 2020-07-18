using Models.EFrame;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class AccountAdminDAO
    {
        public ComesticEntities dbContext = null;
        public AccountAdminDAO()
        {
            dbContext = new ComesticEntities();
        }
        public List<Quantri> Ad()
        {
            var ad = dbContext.Quantris.ToList();
            return ad;
        }
        public int FindLast()
        {
            var find = dbContext.Quantris.LastOrDefault();
            return find.adId;
        }
        public int AddNewAdmin(Quantri obj)
        {
            var rs = dbContext.Quantris.Add(obj);
            dbContext.SaveChanges();
            if (rs != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public bool UpdateAdmin(Quantri obj)
        {
            try
            {
                var user = dbContext.Quantris.SingleOrDefault(x => x.adId == obj.adId);
                user.password = obj.password;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public Quantri ViewDetail(int adId)
        {
            return dbContext.Quantris.SingleOrDefault(x => x.adId == adId);
        }
        public int DeleteAccount(int obj)
        {
            var ad = dbContext.Quantris.Find(obj);
            var del = dbContext.Quantris.Remove(ad);
            dbContext.SaveChanges();
            if (del != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public List<Quantri> Search(string username)
        {
            var search = dbContext.Quantris.Where(x => x.username.Contains(username)).ToList();
            return search;
        }
    }
}
