using System;
using System.Collections.Generic;

namespace WebApplication2
{
    public partial class RelaxRum
    {
        public RelaxRum()
        {
            Microwave = new HashSet<Microwave>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Microwave> Microwave { get; set; }
    }
}
