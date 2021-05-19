using System;
using Core.Entities;

namespace Core.CoreDtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsExpense { get; set; }
        public int PaidPartyId { get; set; }
        public string PaidParty { get; set; }
        public int PaidToId { get; set; }
        public string PaidToParty { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
    }
}