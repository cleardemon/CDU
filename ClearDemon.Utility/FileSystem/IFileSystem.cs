using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClearDemon.Utility.FileSystem
{
    public interface IFileSystem
    {
        Task<bool> CheckExistsAsync(string path);
        Task<bool> CreateDirectoryAsync(string path, bool allowExisting = true);
        Task<bool> DeleteDirectoryAsync(string path, bool recursive = false);
        Task<bool> DeleteFileAsync(string path);
        Task<FileDetail> GetFileDetailAsync(string path);
        Task<IEnumerable<FileDetail>> GetFileListAsync(string path, bool recursive = false);
        Task<IEnumerable<string>> GetDirectoryListAsync(string path, bool recursive = false);
        Task<string> GetTemporaryFilePathAsync(bool createFile = false, string useExtension = null);
        Task<bool> RenameFileAsync(string oldPath, string newPath);
    }
}