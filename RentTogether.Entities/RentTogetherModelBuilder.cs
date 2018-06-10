using System;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace RentTogether.Entities
{
    public class RentTogetherModelBuilder
    {
		public IEdmModel GetEdmModel(IServiceProvider serviceProvider)
		{
			var builder = new ODataConventionModelBuilder(serviceProvider);


            builder.EntitySet<User>(nameof(User))
                   .EntityType
                   .Filter() // Allow for the $filter Command
                   .Count() // Allow for the $count Command
                   .Expand() // Allow for the $expand Command
                   .OrderBy() // Allow for the $orderby Command
                   .Page() // Allow for the $top and $skip Commands
                   .Select() // Allow for the $select Command
			       .HasMany(x => x.Messages)
                   .Expand();

            return builder.GetEdmModel();
		}
    }
}
