using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations
{
    internal sealed class FileItemConfiguration : IEntityTypeConfiguration<FileItem>
    {
        public void Configure(EntityTypeBuilder<FileItem> builder)
        {
            builder.ToTable(nameof(FileItem));

            builder.HasKey(fileItem => fileItem.Id);

            builder.Property(fileItem => fileItem.Id).ValueGeneratedOnAdd();

            builder.Property(fileItem => fileItem.Name).HasMaxLength(400);
        }
    }
}
