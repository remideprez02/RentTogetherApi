//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class UserPicture
    {
        public UserPicture()
        {
        }
        [Key]
		public int UserPictureId { get; set; }
		public string FileToBase64 { get; set; }
		public User User { get; set; }
    }
}
