using MyFolderProject.Data.Repository;


namespace MyFolderProject.Models
{
    public class MyFolders : IRepoId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeOfFolder { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int Size { get; set; }
        public int NumberOfSubFolders { get; set; }
    }
}
