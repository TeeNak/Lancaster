using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IFileItemRepository
    {
        Task<IEnumerable<FileItem>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<FileItem> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<FileItem> AddAsync(FileItem fileItem, CancellationToken cancellationToken = default);

        Task RemoveAsync(FileItem fileItem, CancellationToken cancellationToken = default);
    }
}
