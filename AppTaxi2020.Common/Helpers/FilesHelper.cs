using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppTaxi2020.Common.Helpers
{
    public class FilesHelper : IFilesHelper
    {
        public byte[] ReadFully(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
