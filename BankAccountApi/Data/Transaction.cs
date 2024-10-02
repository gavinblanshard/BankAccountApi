namespace BankAccountApi.Data
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public uint AccountNumber { get; set; }

        public decimal Amount { get; set; }

        public Guid? RelatedTransactionId { get; set; }

        public DateTime Created { get; set; }
    }
}
