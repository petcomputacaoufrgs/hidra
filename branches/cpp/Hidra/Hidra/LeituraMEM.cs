using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hidra
{
    public static class LeituraMEM
    {
        public static byte[] ler(FileStream f)
        {
            byte[] memory = new byte[256];
            
            f.Seek(4, SeekOrigin.Begin);
            for (int i = 0; i < 256; i++)
            {
                memory[i] = (byte)f.ReadByte();
                f.Seek(1, SeekOrigin.Current);
            }
            return memory;
        }
    }
}
