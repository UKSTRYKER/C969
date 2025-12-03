using System;

namespace Scheduling_App
{
    public class User
    {
        public int UserID { get; }
        public string UserName { get; }
        public string Password { get; }
        public byte Active { get; }
        public DateTime CreateDate { get; }
        public string CreatedBy { get; }
        public DateTime LastUpdate { get; }
        public string LastUpdateBy { get; }

        public User(int userID, string userName, string password, int active, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
        {
            UserID = userID;
            UserName = userName;
            Password = password;
            Active = (byte)active;
            CreateDate = createDate;
            CreatedBy = createdBy;
            LastUpdate = lastUpdate;
            LastUpdateBy = lastUpdateBy;
        }
    }
}
