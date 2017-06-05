using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenMaster.Models
{
    public class TransactionResponse
    {
        public bool Success { get; set; }
        public Transaction Transaction { get; set; }
        public string Message { get; set; }
        public int EventTokensRemaining { get; set; }

        public TransactionResponse(bool success, Transaction transaction, string message, int tokensRemaining = 0)
        {
            Success = success;
            Transaction = transaction;
            Message = message;
            EventTokensRemaining = tokensRemaining;
        }
    }
}
