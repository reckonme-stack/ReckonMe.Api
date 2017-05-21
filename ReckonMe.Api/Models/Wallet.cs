using System.Collections.Generic;
using MongoDB.Bson;

namespace ReckonMe.Api.Models
{
    public class Wallet
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public IEnumerable<string> Members { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }
    }
}