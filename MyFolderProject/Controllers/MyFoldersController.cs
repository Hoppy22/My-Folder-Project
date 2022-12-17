using MyFolderProject.Data;
using MyFolderProject.Interface;
using MyFolderProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace MyFolderProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MyFoldersController : ControllerBase
    {
        private readonly IMyFoldersService _service;

        public MyFoldersController(IMyFoldersService service)
        {
            _service = service;
        }


        [HttpPost("createfolder")]
        public async Task<IActionResult> CreateFolder(string? folderName, string? subFolder)
        {
          
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);


            if (!Directory.Exists(folderPath))
            { 
                Directory.CreateDirectory(folderPath);
            }

            var pathString = Path.Combine(Directory.GetCurrentDirectory(), folderName, subFolder);


            if (!Directory.Exists(pathString))
            {
                Directory.CreateDirectory(pathString);
            }

            // Add to database
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
             
            MyFolders myFolder = new MyFolders();

            myFolder.Name = folderName;
            myFolder.CreatedDate = DateTime.Now;
            myFolder.LastModifiedDate = DateTime.Now;
            myFolder.TypeOfFolder = "File Folder";
            myFolder.Size = 0;
            myFolder.NumberOfSubFolders = 1;


            await _service.AddAsync(myFolder);
            return Ok();
        }


        [HttpPut()]
        public async Task<IActionResult> UpdateFolders(string? folderName, string newFolderName, int id)
        {

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var newFolderPath = Path.Combine(Directory.GetCurrentDirectory(), newFolderName);


            if (Directory.Exists(folderPath))
            {

                if (newFolderName != String.Empty)
                {

                    Directory.Move(folderPath, newFolderPath);   // to rename directory


                    if (Directory.Exists(newFolderPath))         // checking if directory has been renamed or not

                    {
                        Console.WriteLine("The directory was renamed to " + newFolderName);
                    }
                    
                    var result = await _service.GetByIdAsync(id);
                    result.Name = newFolderName;
                    await _service.UpdateAsync(result);

                    return Ok();
                }
            }
            return Ok("Updated sucessfully");
        }


        [HttpGet("getallfolders")]
        public async Task<IActionResult> GetFolders()
        {
            var folders = await _service.GetAllAsync();
            return Ok(folders);
        }



        [HttpDelete()]

        public async Task<IActionResult> DeleteFolders(string folderName, int id)
        {

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (System.IO.Directory.Exists(folderPath))
            {
                try
                {
                    System.IO.Directory.Delete(folderPath, true);

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
