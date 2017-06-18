using System;
using System.Collections.Generic;

namespace ReckonMe.Api.Dtos
{
    public class ExpenseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string Payer { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<string> Members { get; set; }
    }
}