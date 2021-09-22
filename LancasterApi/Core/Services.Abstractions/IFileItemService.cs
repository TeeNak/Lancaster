using Contracts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IFileItemService
    {
        Task<IEnumerable<FileItemWithoutFileDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<FileItemWithoutFileDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<FileItemWithoutFileDto> CreateAsync(FileItemForCreationDto fileItemDto, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
