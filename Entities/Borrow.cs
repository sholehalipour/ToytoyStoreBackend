using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.practice.Entities
{
    public class Borrow:Thing
    {
        public required Book Book { get; set; }
        public required Member Member { get; set; }
        public DataTime BorrowTime { get; set; }
        public DataTime? ReturnTime { get; set; }
        
    }
}