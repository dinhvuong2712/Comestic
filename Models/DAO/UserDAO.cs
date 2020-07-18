using Models.EFrame;
using System.Linq;
using Common;
using System.Collections.Generic;

namespace Models.DAO
{
    public class UserDAO
    {
        public ComesticEntities dbContext = null;
        public UserDAO()
        {
            dbContext = new ComesticEntities();
        }
        public Customer UserLogin(string username)
        {
            return dbContext.Customers.SingleOrDefault(x => x.username == username);
        }

        public List<string> GetListCredential(string username)
        {
            var user = dbContext.Customers.SingleOrDefault(x =>x.username == username);
            var credential = (from a in dbContext.Credentials
                             join b in dbContext.QuantriNhoms on a.GroupId equals b.GroupId
                             join c in dbContext.Roles on a.RoleId equals c.RoleId
                             where b.GroupId == user.GroupId
                             select new
                             {
                                 GroupId = a.GroupId,
                                 RoleId = a.RoleId
                             }).AsEnumerable().Select(x => new Credential()
                             {
                                 GroupId = x.GroupId,
                                 RoleId = x.RoleId
                             });
            return credential.Select(x => x.RoleId).ToList();
        }

        public bool FindUser(string username, string password)
        {
            Quantri login = dbContext.Quantris.SingleOrDefault(x => x.username == username && x.password == password);
            if (login != null)
            {
                return true;
            }
            return false;
        }
        public int LoginCheck(string username,string password, bool isLoginAdmin = false)
        {
            var login = dbContext.Customers.SingleOrDefault(x => x.username == username);
            var loginMod = dbContext.Quantris.SingleOrDefault(x => x.username == username);
            if (login != null)
            {
                if (isLoginAdmin == true)
                {
                    if (login.GroupId == CommonConstantsLibary.ADMIN_GROUP || login.GroupId == CommonConstantsLibary.MOD_GROUP)
                    {
                        if (login.status == 0)
                        {
                            return 0;
                        }
                        else
                        {
                            if (login.password == password)
                            {
                                return 1;
                            }
                            else
                            {
                                return -2;
                            }
                        }
                    }
                    return -3;
                }
                else
                {
                    if (login.status == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        if (login.password == password)
                        {
                            return 1;
                        }
                        else
                        {
                            return -2;
                        }
                    }
                }
            }
            else if (loginMod != null)
            {
                if (isLoginAdmin == true)
                {
                    if (loginMod.status == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        if (loginMod.password == password)
                        {
                            return 1;
                        }
                        else
                        {
                            return -2;
                        }
                    }
                }
            }
            return -1;
        }
    }
}
