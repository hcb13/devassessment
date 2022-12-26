using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SubtitleTimeshift
{
    internal class Convert
    {
        public static byte[] StreamToByte(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
