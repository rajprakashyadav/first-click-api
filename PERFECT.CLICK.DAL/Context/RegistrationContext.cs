using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PERFECT.CLICK.DAL.Context
{
    public class RegistrationContext : DbContext
    {
        public RegistrationContext(DbContextOptions<RegistrationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
