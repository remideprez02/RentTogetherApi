﻿//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using RentTogether.Interfaces.Helpers;

namespace RentTogether.Common.Helpers
{
    public class CustomEncoder : ICustomEncoder
    {

        /// <summary>
        /// Base64s the encode.
        /// </summary>
        /// <returns>The encode.</returns>
        /// <param name="plainText">Plain text.</param>
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Base64s the decode.
        /// </summary>
        /// <returns>The decode.</returns>
        /// <param name="base64EncodedData">Base64 encoded data.</param>
        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// Decodes the basic auth.
        /// </summary>
        /// <returns>The basic auth.</returns>
        /// <param name="encodeBasicAuth">Encode basic auth.</param>
        public Tuple<string, string> DecodeBasicAuth(string encodeBasicAuth)
        {
            if (encodeBasicAuth.Substring(0, 5) == "Basic")
            {
                var encoded = encodeBasicAuth.Substring(5);
                var decoded = Base64Decode(encoded);

                var indexSeparator = decoded.IndexOf(":", StringComparison.OrdinalIgnoreCase);

                var decodeUserMail = decoded.Substring(0, indexSeparator);
                var decodeUserPassword = decoded.Substring(indexSeparator + 1);


                var data = new Tuple<string, string>(decodeUserMail, decodeUserPassword);
                return data;
            }
            return null;
        }

        /// <summary>
        /// Decodes the bearer auth.
        /// </summary>
        /// <returns>The bearer auth.</returns>
        /// <param name="encodeBearerAuth">Encode bearer auth.</param>
        public string DecodeBearerAuth(string encodeBearerAuth)
        {
            if (encodeBearerAuth.Substring(0, 6) == "Bearer")
            {
                return encodeBearerAuth.Substring(7);
            }
            return null;
        }

        /// <summary>
        /// Files to base64.
        /// </summary>
        /// <returns>The to base64.</returns>
        /// <param name="file">File.</param>
		public string FileToBase64(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var fileBytes = memoryStream.ToArray();
                return Convert.ToBase64String(fileBytes);
            }
        }
    }
}
