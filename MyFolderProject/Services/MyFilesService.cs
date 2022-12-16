using MyFolderProject.Data;
using MyFolderProject.Data.Repository;
using MyFolderProject.Interface;
using MyFolderProject.Models;

namespace MyFolderProject.Services
{
    public class MyFilesService : Repo<MyFiles>, IMyFilesService
    {
        public MyFilesService(FoldersAndFilesAPIDbContext foldersAndFilesAPIDbContext) : base(foldersAndFilesAPIDbContext)
        {

        }
    }
}
