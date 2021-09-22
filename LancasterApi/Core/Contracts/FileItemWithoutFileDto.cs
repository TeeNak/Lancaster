using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class FileItemWithoutFileDto
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 楽観的排他制御のためのタイムスタンプ
        /// </summary>
        [Timestamp]
        public byte[] Version { get; set; }

    }
}
