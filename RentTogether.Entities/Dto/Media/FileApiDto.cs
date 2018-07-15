//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using Microsoft.AspNetCore.Http;

namespace RentTogether.Entities.Dto.Media
{
    public class FileApiDto
    {
		public int UserId { get; set; }
		public int FileId { get; set; }
		public string FileToBase64 { get; set; }
    }
}
