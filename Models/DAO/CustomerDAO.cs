using Models.EFrame;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;

namespace Models.DAO
{
    public class CustomerDAO
    {
        public ComesticEntities dbContext = null;
        public CustomerDAO()
        {
            dbContext = new ComesticEntities();
        }
        public int CheckOrder(string name, string email)
        {
            var checkId = from customerid in dbContext.Customers
                          where customerid.username == name && customerid.email == email
                          select customerid.customerId;
            return checkId.First();
        }
        public bool CheckMail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public List<Customer> Search(string username)
        {
            var search = dbContext.Customers.Where(x => x.username.Contains(username)).ToList();
            return search;
        }

        public List<Customer> Acc()
        {
            var acc = dbContext.Customers.ToList();
            return acc;
        }
        public int FindLast()
        {
            var find = dbContext.Customers.LastOrDefault();
            return find.customerId;
        }
        public int AddNewCustomer(Customer obj)
        {
            var rs = dbContext.Customers.Add(obj);
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
        public bool UpdateCustomer(Customer obj)
        {
            try
            {
                var user = dbContext.Customers.SingleOrDefault(x => x.customerId == obj.customerId);
                user.email = obj.email;
                user.status = obj.status;
                dbContext.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }
        public bool ChangeCustomer(int id)
        {
            try
            {
                var ac = dbContext.Customers.SingleOrDefault(x => x.customerId == id);
                if (ac.status == 1)
                {
                    ac.status = 0;
                }
                else
                {
                    ac.status = 1;
                }
                dbContext.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public Customer ViewDetail(int customerId)
        {
            return dbContext.Customers.SingleOrDefault(x => x.customerId == customerId);
        }
        public int DeleteCustomer(int obj)
        {
            var ad = dbContext.Customers.Find(obj);
            var del = dbContext.Customers.Remove(ad);
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
    }
}
