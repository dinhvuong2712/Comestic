using Models.EFrame;
using System.Data.SqlClient;

namespace Models.DAO
{
    public class ContactDAO
    {
        public ComesticEntities dbContext = null;
        public ContactDAO()
        {
            dbContext = new ComesticEntities();
        }
        public int AddContact(string firstname,string lastname,int phone,string email,string address)
        {
            object[] parameter =
            {
                new SqlParameter("@FirstName",firstname),
                new SqlParameter("@LastName",lastname),
                new SqlParameter("@Phone",phone),
                new SqlParameter("@Email",email),
                new SqlParameter("@Address",address)
            };
            int res = dbContext.Database.ExecuteSqlCommand("AddContact @FirstName,@LastName,@Phone,@Email,@Address", parameter);
            return res;
        }
    }
}
