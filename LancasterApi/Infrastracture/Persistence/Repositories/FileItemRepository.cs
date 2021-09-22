using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Mapster;

namespace Persistence.Repositories
{
    public class FileItemRepository : IFileItemRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public FileItemRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;


        public async Task<IEnumerable<FileItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            // サイズが大きくなることが多いため
            // File は除外する。
            var items = await _dbContext.FileItems
                     .Select(x => new FileItem
                     {
                         Id = x.Id,
                         Name = x.Name
                     }).ToListAsync(cancellationToken);
            return items;
//            return await _dbContext.FileItems.ToListAsync(cancellationToken);
        }        
            

        public async Task<FileItem> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _dbContext.FileItems.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<FileItem> AddAsync(FileItem fileItem, CancellationToken cancellationToken = default)
        {
            await _dbContext.FileItems.AddAsync(fileItem, cancellationToken);
            return fileItem;
        }

        public async Task RemoveAsync(FileItem fileItem, CancellationToken cancellationToken = default)
        {
            _dbContext.FileItems.Remove(fileItem);
            await Task.CompletedTask;
        }
    }
}
