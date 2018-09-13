using System;
using System.Collections.Generic;

namespace WebApplication2
{
    public partial class Microwave
    {
        public int Id { get; set; }
        public bool Busy { get; set; }
        public int? UserId { get; set; }
        public int RelaxRumId { get; set; }

        public RelaxRum RelaxRum { get; set; }
        public User User { get; set; }
    }
}
