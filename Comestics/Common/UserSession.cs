using System;

namespace Comestics.Common
{
    [Serializable]
    public class UserSession
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string GroupId { get; set; }
    }
}