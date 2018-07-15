//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using Microsoft.AspNetCore.Http;

namespace RentTogether.Interfaces.Helpers
{
    public interface ICustomEncoder
    {
        string Base64Encode(string plainText);
        string Base64Decode(string base64EncodedData);
        Tuple<string, string> DecodeBasicAuth(string encodeBasicAuth);
        string DecodeBearerAuth(string encodeBearerAuth);
		string FileToBase64(IFormFile file);
    }
}
