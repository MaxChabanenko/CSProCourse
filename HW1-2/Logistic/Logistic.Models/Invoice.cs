﻿
namespace Logistic.Models
{
    public class Invoice :  IEntity<Guid>
    {
        public Invoice() { }

        public new Guid Id { get; set; }
        public string RecipientAddress { get; set; }
        public string SenderAddress { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public string SenderPhoneNumber { get; set; }

    }
}