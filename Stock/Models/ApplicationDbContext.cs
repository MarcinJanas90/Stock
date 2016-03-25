using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Stock.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Share> Shares { get; set; }
    }
}