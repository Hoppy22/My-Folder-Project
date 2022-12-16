using MyFolderProject.Data.Repository;


namespace MyFolderProject.Models
{
    public class MyFiles : IRepoId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeOfFile { get; set; }
        public int Size { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
