using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppTaxi2020.Common.Helpers
{
     public interface IFilesHelper
    {
        byte[] ReadFully(Stream input);
    }
}
