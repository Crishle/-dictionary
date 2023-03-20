using Exercise1.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Exercise1.Controllers
{
    [ApiController]
    public class FileController : ControllerBase
    {
        private FileService fileService = new();

        [HttpPost("upload-audio")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(IFormFile file, string url, CancellationToken cancellationToken)
        {
            var result = await fileService.uploadFile(file, url);
            return Ok(result);
        }
    }
}
