using System;
using System.Collections.Generic;

namespace WebApplication2
{
    public partial class Line
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
