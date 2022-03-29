using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Stomik
{
    class StomContext : DbContext
    {
        public DbSet<Poset> Posets { get; set; }
        public DbSet<Vrach> Vraches { get; set; }
        public DbSet<Yslygi> Yslygis { get; set; }
        public DbSet<Priem> Priems { get; set; }
        public DbSet<LoginDate> LoginDate { get; set; }

        public StomContext() : base(nameOrConnectionString: "PGConnectionString") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}
