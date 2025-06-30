using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.practice.Entities
{
    public class Member:Thing
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public Gender Gender { get; set; }
    }
}