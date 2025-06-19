using System.Data.Entity;

namespace InsuranceQuoteCalculator.Models
{
    public class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext() : base("name=InsuranceDB")
        {
        }

        public DbSet<Insuree> Insurees { get; set; }
    }
}