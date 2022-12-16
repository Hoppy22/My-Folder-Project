using MyFolderProject.Models;
using Microsoft.EntityFrameworkCore;

namespace MyFolderProject.Data
{
    public class FoldersAndFilesAPIDbContext: DbContext
    {
        public FoldersAndFilesAPIDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<MyFolders> Folders { get; set; }
        public DbSet<MyFiles> Files { get; set; }
    }
}
