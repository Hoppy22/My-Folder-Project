using MyFolderProject.Data;
using MyFolderProject.Data.Repository;
using MyFolderProject.Interface;
using MyFolderProject.Models;

namespace MyFolderProject.Services
{
    public class MyFoldersService : Repo<MyFolders>, IMyFoldersService 
    {
        public MyFoldersService(FoldersAndFilesAPIDbContext foldersAndFilesAPIDbContext) : base(foldersAndFilesAPIDbContext)
        {

        }
    }
}
