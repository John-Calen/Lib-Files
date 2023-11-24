using JC.Lib.Files.Models;

namespace JC.Lib.Files.Abstractions
{
    public interface IDirectoryScanEnumerable<T_FileInfo> : IEnumerable<T_FileInfo>
        where T_FileInfo : FileSystemInfo
    {
        IDirectoryScanEnumerable<T_FileInfo> IgnoreDirectory(Func<DirectoryInfo, bool> predicate);
        IDirectoryScanEnumerable<T_FileInfo> AddRootInfo(RootInfo rootInfo);
    }
}
