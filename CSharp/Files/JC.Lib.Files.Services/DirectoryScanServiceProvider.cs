using JC.Lib.Files.Abstractions;

namespace JC.Lib.Files.Services
{
    public class DirectoryScanServiceProvider : IDirectoryScanServiceProvider
    {
        public IDirectoryScanEnumerable<T_FileInfo> CreateScannerEnumerable<T_FileInfo>()
            where T_FileInfo : FileSystemInfo
        {
            return new DirectoryScanEnumerable<T_FileInfo>();
        }
    }
}
