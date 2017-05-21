﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReckonMe.Api.Dtos
{
    public class EditWalletDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Owner { get; set; }
        public IEnumerable<string> Members { get; set; }
    }
}