using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.practice.Entities.Base;

namespace backend.practice.Entities
{
    public class Book :Thing
    {
        public required string title { get; set; }
        public string? writer { get; set; }
        public string? publisher { get; set; }
        public double price { get; set; }
    }
}