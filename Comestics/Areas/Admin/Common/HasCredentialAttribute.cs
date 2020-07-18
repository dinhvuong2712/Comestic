using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Common;
namespace Comestics.Common
{
    public class HasCredentialAttribute:AuthorizeAttribute
    {
        public string RoleId { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = (UserSession)HttpContext.Current.Session[CommonConstants.User_SESSION];
            if (session == null)
            {
                return false;
            }
            List<string> privilegeLevels = GetCredentialByLoggedInUser(session.UserName);
            if (session.GroupId == CommonConstantsLibary.ADMIN_GROUP || session.GroupId == CommonConstantsLibary.MOD_GROUP)
            {
                if(privilegeLevels.Contains(RoleId))
                    return true;
                return false;
            }
            else
            {
                return false;
            }
        }
        private List<string> GetCredentialByLoggedInUser(string username)
        {
            return (List<string>)HttpContext.Current.Session[CommonConstants.Credential_User];
        }
    }
}