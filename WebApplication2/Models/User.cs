using System;
using System.Collections.Generic;

namespace WebApplication2
{
    public partial class User
    {
        public User()
        {
            Microwave = new HashSet<Microwave>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Line Line { get; set; }
        public ICollection<Microwave> Microwave { get; set; }
    }
}
