using System;

namespace Data.Models
{
    public class MessageDataModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ContactId { get; set; }

        public DateTime SendTime { get; set; }

        public DateTime DeliveryTime { get; set; }

        public string Content { get; set; }
    }
}