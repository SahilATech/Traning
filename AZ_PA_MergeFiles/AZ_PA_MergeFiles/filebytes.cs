using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AZ_PA_MergeFiles
{
    public class filebytes
    {
        public static byte[][] convertbyte(IFormFileCollection files)
        {

            byte[][] bytesarray = new byte[files.Count][];
            int j = 0;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        bytesarray[j] = fileBytes;
                        j++;

                    }
                }
            }
            return bytesarray;
        }
    } 
}
