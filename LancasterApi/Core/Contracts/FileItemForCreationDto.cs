using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public class FileItemForCreationDto
    {
        public string Name { get; set; }

        public string FileName { get; set; }

        public byte[] File { get; set; }

    }
}
