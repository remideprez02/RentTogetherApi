//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using Microsoft.AspNetCore.Http;

namespace RentTogether.Entities.Dto.Media
{
    public class FileDto
    {
		public int UserId { get; set; }

		public IFormFile File { get; set; }
		public string FileToBase64 { get; set; }
    }
}
