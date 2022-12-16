using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MyFolderProject.Data.Repository
{
    public class Repo<T> : IRepo<T> where T : class, IRepoId, new()
    {
        private readonly FoldersAndFilesAPIDbContext _foldersAndFilesAPIDbContext;

        public Repo(FoldersAndFilesAPIDbContext foldersAndFilesAPIDbContext)
        {
            _foldersAndFilesAPIDbContext = foldersAndFilesAPIDbContext;
        }


        public async Task AddAsync(T entity)
        {
            await _foldersAndFilesAPIDbContext.Set<T>().AddAsync(entity);
            await _foldersAndFilesAPIDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _foldersAndFilesAPIDbContext.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityEntry = _foldersAndFilesAPIDbContext.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await _foldersAndFilesAPIDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _foldersAndFilesAPIDbContext.Set<T>().ToListAsync();
            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _foldersAndFilesAPIDbContext.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task UpdateAsync(T entity)
        {
            // EntityEntry entityEntry = _foldersAndFilesAPIDbContext.Entry<T>(entity);
            //entityEntry.State = EntityState.Modified;

            var result = _foldersAndFilesAPIDbContext.Set<T>().Update(entity);
            await _foldersAndFilesAPIDbContext.SaveChangesAsync();
        }
    }
}
