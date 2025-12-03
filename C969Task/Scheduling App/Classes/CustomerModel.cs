using System;

namespace Scheduling_App
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int AddressId { get; set; }
        public byte Active { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        private static int count = 0;

        public CustomerModel(int customerId, string customerName, int addressId, int active, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
        {
            CustomerId = customerId;
            count = customerId;
            CustomerName = customerName;
            AddressId = addressId;
            Active = (byte)active;
            CreateDate = createDate;
            CreatedBy = createdBy;
            LastUpdate = lastUpdate;
            LastUpdateBy = lastUpdateBy;
        }

        public CustomerModel(string customerName, int addressId, int active, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
        {
            count++;
            CustomerId = count;
            CustomerName = customerName;
            AddressId = addressId;
            Active = (byte)active;
            CreateDate = createDate;
            CreatedBy = createdBy;
            LastUpdate = lastUpdate;
            LastUpdateBy = lastUpdateBy;
        }
    }
}
