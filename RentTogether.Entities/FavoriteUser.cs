using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class FavoriteUser
    {
        public FavoriteUser()
        {
        }

        [Key]
		public int FavoriteUserId { get; set; }
		public User VoteUser { get; set; }
		public List<User> TargetUsers { get; set; }
    }
}
