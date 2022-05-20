using Microsoft.AspNetCore.WebUtilities;
using System;

namespace DirectorySearchTool.Core.Helpers
{
    public static class ShortLinkHelper
    {
        public static string Encode(int id)
        {
            return WebEncoders.Base64UrlEncode(BitConverter.GetBytes(id));
        }

        public static int Decode(string urlChunk)
        {
            return BitConverter.ToInt32(WebEncoders.Base64UrlDecode(urlChunk));
        }
    }
}
