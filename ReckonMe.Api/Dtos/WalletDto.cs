using System.Collections.Generic;

namespace ReckonMe.Api.Dtos
{
    public class WalletDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public IEnumerable<string> Members { get; set; }
        public IEnumerable<ExpenseDto> Expenses { get; set; }
    }
}