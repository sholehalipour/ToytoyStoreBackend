using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.practice.Entities.Base;
using backend.practice.Enums;

namespace backend.practice.Entities
{
    public class Member : Thing
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public Gender Gender { get; set; }
    }
}