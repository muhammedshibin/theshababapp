using System;
using Core.Entities;

namespace API.Dtos
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public string Name { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsExpense { get; set; }
        public int PaidPartyId { get; set; }
        public string PaidParty { get; set; }
        public double Amount { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
    }
}