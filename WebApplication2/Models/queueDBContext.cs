using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication2
{
    public partial class queueDBContext : DbContext
    {
        public queueDBContext()
        {
        }

        public queueDBContext(DbContextOptions<queueDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Line> Line { get; set; }
        public virtual DbSet<Microwave> Microwave { get; set; }
        public virtual DbSet<RelaxRum> RelaxRum { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
