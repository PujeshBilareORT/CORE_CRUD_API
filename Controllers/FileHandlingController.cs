using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server;
using System.Diagnostics.CodeAnalysis;

namespace CORE_CRUD_API.Controllers
{
    [ApiController]
    public class FileHandlingController : ControllerBase
    {
		[Route("api/[controller]/v1/UploadFile")]
		[HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            /*  IFormFile file  */
            if (file != null && file.Length > 0)
            {
                string current = DateTime.Now.ToString("HH_mm_ss");
                //append current time hh:mm:ss to file name for uniqueness
				string full=(Path.GetFileNameWithoutExtension(file.FileName)+current+Path.GetExtension(file.FileName)).Replace(" ","_");
                // Get the physical path to save the file
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", full);
                // Open a FileStream to write the uploaded file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    // Copy the file to the FileStream
                    file.CopyToAsync(stream);
                }                
                return Ok("File uploaded successfully.");
            }
            return BadRequest("No file was submitted.");
            /*var file = Request.Form.Files.FirstOrDefault();
            byte[] fileBytes = null;

			if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                // File uploaded successfully
                return Ok("OK File Received");
            }

            // No file or empty file was submitted
            return BadRequest("No file was Received.");*/
        }
    }
}
