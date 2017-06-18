using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace ReckonMe.Api.Models
{
    public class Expense
    {
        public Expense()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string Payer { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<string> Members { get; set; }
    }
}