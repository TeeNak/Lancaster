using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class FileItemService : IFileItemService
    {
        private IFileItemRepository _fileItemRepository;
        private IUnitOfWork _unitOfWork;

        public FileItemService(IFileItemRepository fileItemRepository, IUnitOfWork unitOfWork) : base()
        {
            _fileItemRepository = fileItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<FileItemWithoutFileDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var fileItems = await _fileItemRepository.GetAllAsync(cancellationToken);
            var fileItemDtos = fileItems.Adapt<IEnumerable<FileItemWithoutFileDto>>();
            return fileItemDtos;
        }

        public async Task<FileItemWithoutFileDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var fileItem = await _fileItemRepository.GetByIdAsync(id, cancellationToken);
            if(fileItem is null)
            {
                throw new FileItemNotFoundException(id);
            }
            var fileItemDto = fileItem.Adapt<FileItemWithoutFileDto>();
            return fileItemDto;
        }

        public async Task<FileItemWithoutFileDto> CreateAsync(FileItemForCreationDto fileItemDto, CancellationToken cancellationToken = default)
        {
            var fileItem = fileItemDto.Adapt<FileItem>();
            var fileItemResult = await _fileItemRepository.AddAsync(fileItem, cancellationToken);

            // fileItem Id is expected to be filled here
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var fileItemDtoResult = fileItemResult.Adapt<FileItemWithoutFileDto>();
            return fileItemDtoResult;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var fileItem = await _fileItemRepository.GetByIdAsync(id, cancellationToken);

            if( fileItem is null)
            {
                throw new FileItemNotFoundException(id);
            }

            await _fileItemRepository.RemoveAsync(fileItem);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }
}
