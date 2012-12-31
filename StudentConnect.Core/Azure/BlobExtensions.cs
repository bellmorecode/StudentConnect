using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace StudentConnect.Azure
{
    public static class BlobExtensions
    {
        public static string DownloadText(this ICloudBlob blob)
        {
            string content = string.Empty;
            using (var ms = new MemoryStream())
            {
                blob.DownloadToStream(ms);
                var data = ms.ToArray();
                content = new String(Encoding.UTF8.GetChars(data));
            }
            return content;
        }
    }
}
