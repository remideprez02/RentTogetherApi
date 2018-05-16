using System;
namespace RentTogether.Entities.Filters.Users
{
    public class UserFilters
    {
        public UserFilters()
        {
        }
		public string CityFilter { get; set; }
		public string DateSort { get; set; }
		public string DateFilter { get; set; }
    }
}
