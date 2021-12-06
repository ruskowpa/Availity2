using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace Availity.Models.BusinessModels
{
    public class BusinessDataContext : DbContext
    {
        public BusinessDataContext()
    : base("name=BusinessDataContext")
        {
        }

        public DbSet<CSVFile> CSVFiles { get; set; }
        public DbSet<Enrollee> Enrollees { get; set; }
        public DbSet<InsuranceFile> InsuranceFiles { get; set; }
        public DbSet<AspNetUsers> AspNetUsers { get; set; }
    }
}