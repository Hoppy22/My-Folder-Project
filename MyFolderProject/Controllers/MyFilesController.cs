using MyFolderProject.Data;
using MyFolderProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFolderProject.Interface;

using System.IO;
using System.Threading;
using Microsoft.AspNetCore.StaticFiles;

namespace MyFolderProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MyFilesController : ControllerBase
    {
        private readonly IMyFilesService _service;

        public MyFilesController(IMyFilesService service)
        {
            _service = service;
        }



        private async Task<bool> WriteFile(IFormFile file, string folderName, string subFolder)
        {
            bool success = false;
            string fileName;

            try
            {
                var extention = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + extention; // create a new name for file due to security

              
                string pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), folderName, subFolder);


                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }
              
                var path = Path.Combine(pathBuilt, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                success = true;
            }
            catch (Exception e)
            {
                //log error
            }
            return success;
        }




        [HttpPost]
        [Route("SaveFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(IFormFile file, string folderName, string subFolderName, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();

            }

            MyFiles myfiles = new MyFiles();
            myfiles.Name = file.FileName;
            myfiles.CreatedDate = DateTime.Now;
            myfiles.LastModifiedDate = DateTime.Now;
            myfiles.TypeOfFile = "File";
            myfiles.Size = 0;
           
            await _service.AddAsync(myfiles);
        
            await WriteFile(file, folderName, subFolderName);
            return Ok();
        }


        [HttpGet("getallfiles")]
        public async Task<IActionResult> GetFiles()
        {
            var folders = await _service.GetAllAsync();
            return Ok(folders);
        }



        [HttpGet("DownloadFile")]
        public async Task<ActionResult> DownloadFile(string NameFile, string folder, string subFolder)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), folder,subFolder, NameFile);
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }
        

        [HttpDelete()]

        public async Task<IActionResult> DeleteFile(string folderName, string subFolder, string fileName, int id)
        {

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName, subFolder, fileName); 

            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);

                    await _service.DeleteAsync(id);
                }


                
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();

                }

                return Ok();
            }

            return Ok("deleted sucessfully");
        }

       
























        



        
    }
}

