using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contracts
{
    public class FileItemWithFileDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string FileName { get; set; }

        public byte[] File { get; set; }

        /// <summary>
        /// 楽観的排他制御のためのタイムスタンプ
        /// </summary>
        [Timestamp]
        public byte[] Version { get; set; }

    }
}
