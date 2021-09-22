using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Presentation.Utility;

namespace LancasterApi.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class FileItemController : ControllerBase
    {
        private const Int64 MaxUploadSize = 10L * 1024L * 1024L * 1024L; // 10GB 


        private readonly IFileItemService _service;

        public FileItemController(IFileItemService service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<IActionResult> List(CancellationToken cancellationToken)
        {
            var fileItemDtos = await _service.GetAllAsync(cancellationToken);
            return Ok(fileItemDtos);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFileItemById(Guid id, CancellationToken cancellationToken)
        {
            var fileItemDto = await _service.GetByIdAsync(id, cancellationToken);

            if(fileItemDto?.Id == null)
            {
                return NotFound();
            }

            return Ok(fileItemDto);
        }


        /// <summary>
        /// multipartのNameが以下の物を使用する
        /// json: メタデータペイロード
        /// file: アップロードファイル
        /// 上記以外のpartは無視される。また、上記2partが存在しない場合はエラーを返す
        /// 
        /// なお、IIS上で動作させる場合は以下の設定が必要
        /// Web.config
        ///  <security>
        ///    <requestFiltering>   
        ///        <!-- Configures IIS to accept files up to 500MB -->
        ///        <requestLimits maxAllowedContentLength = "524288000" />
        ///    </ requestFiltering >
        ///  </ security >
        /// 
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [RequestSizeLimit(MaxUploadSize)]
        [RequestFormLimits(MultipartBodyLengthLimit = MaxUploadSize)]
        public async Task<IActionResult> CreateAndUploadFileItemAsync(
            IFormCollection data, IFormFile file, CancellationToken cancellationToken)
        {
            // IFormFile.FileName は使うのであればvalidationが必要

            string json = data["json"];

            if (json == null)
            {
                return BadRequest("multipart part 'json' is required");
            }

            if(file == null)
            {
                return BadRequest("multipart part 'file' is required");
            }


            long length = file.Length;
            if (length < 0)
                return BadRequest("multipart part 'file' length is less than 0");

            if (length == 0)
                return BadRequest("multipart part 'file' length is 0");

            using var fileStream = file.OpenReadStream();
            byte[] bytes = new byte[length];
            fileStream.Read(bytes, 0, (int)file.Length);

            var itemDto = Serializer.Deserialize<FileItemForCreationDto>(json);
            itemDto.File = bytes;

            var result = await _service.CreateAsync(itemDto, cancellationToken);

            return CreatedAtAction(nameof(GetFileItemById), new { id = result.Id }, result);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAccount(Guid id, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(id, cancellationToken);

            return NoContent();
        }

    }
}
