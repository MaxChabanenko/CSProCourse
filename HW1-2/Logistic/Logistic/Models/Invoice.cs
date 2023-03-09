namespace Logistic.ConsoleClient.Classes
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public string RecipientAddress { get; set; }
        public string SenderAddress { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public string SenderPhoneNumber { get; set; }
        public Invoice(Guid id, string recipientAddress, string senderAddress, string recipientPhoneNumber, string senderPhoneNumber)
        {
            Id = id;
            RecipientAddress = recipientAddress;
            SenderAddress = senderAddress;
            RecipientPhoneNumber = recipientPhoneNumber;
            SenderPhoneNumber = senderPhoneNumber;
        }
        public Invoice() { }
    }
}