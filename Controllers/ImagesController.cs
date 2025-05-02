using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace StudentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpPost("Upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            // Save the file to a specific location
            var UploadsDirectory = @"C:\MyUploads";


            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(UploadsDirectory, file.FileName);
            if (!Directory.Exists(UploadsDirectory))
            {
                Directory.CreateDirectory(UploadsDirectory);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(new { FilePath = filePath });
        }


        [HttpGet("GetImage/{FileName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult GetImage(string FileName)
        {
            // Validate the file name
            var UploadsDirectory = @"C:\MyUploads";
            var filePath = Path.Combine(UploadsDirectory, FileName);
            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
           //var fileBytes = System.IO.File.ReadAllBytes(filePath);
           var fileBytes = System.IO.File.OpenRead(filePath);
           var mimeType = GetMimeType(filePath);
            //return File(fileBytes, "image/jpeg");
            return File(fileBytes, mimeType);

        }
        private string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                default:
                    return "application/octet-stream"; // Default for unknown types
            }
        }
    }
}
