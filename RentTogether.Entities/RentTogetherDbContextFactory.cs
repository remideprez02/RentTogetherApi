using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RentTogether.Entities
{

	public class RentTogetherDbContextFactory : IDesignTimeDbContextFactory<RentTogetherDbContext>
        {
		RentTogetherDbContext IDesignTimeDbContextFactory<RentTogetherDbContext>.CreateDbContext(string[] args)
            {
			var optionsBuilder = new DbContextOptionsBuilder<RentTogetherDbContext>();
			optionsBuilder.UseSqlServer("Server=tcp:renttogether.database.windows.net,1433;Initial Catalog=RentTogetherBdd;Persist Security Info=False;User ID=adminrenttogether;Password=MPjka07qf62-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");

			return new RentTogetherDbContext(optionsBuilder.Options);
            }
        }
}
