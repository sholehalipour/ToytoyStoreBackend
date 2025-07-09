<<<<<<< HEAD
using Microsoft.AspNetCore.Identity;
using toytoy_store_backend.Entities.Base;

namespace toytoy_store_backend.Entities
{
    public class Member : Thing
    {
        public required string Name { get; set; }
        public required string Family { get; set; }
        public required string UserName { get; set; }
        // public string? Email { get; set; }
        // public int PhoneNumber { get; set; }
        public required String Password { get; set; }

=======
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
>>>>>>> 1b60adb338816233eb6de69ddf1caf2c1dd5a311
    }
}