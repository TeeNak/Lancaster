using Domain.Entities;
using System;

namespace Domain.Exceptions
{
    public sealed class FileItemNotFoundException : NotFoundException
    {
        public FileItemNotFoundException(Guid id)
            : base($"The file item with the identifier {id} was not found.")    
        {
        }
    }
}
