using JC.Lib.Exceptions;
using JC.Lib.Files.Models;

namespace JC.Lib.Files.Extensions
{
    public static class FileSystemInfoExtensions
    {
        public static string GetRelativePath(this FileSystemInfo original, DirectoryInfo parent)
        {
            return original.FullName.GetRelativePath(parent.FullName);
        }

        public static FileSystemType GetFileSystemType(this FileSystemInfo info)
        {
            switch (info)
            {
                case FileInfo:
                    return FileSystemType.FILE;

                case DirectoryInfo:
                    return FileSystemType.DIRECTORY;

                default:
                {
                    throw new CausedException.Builder()
                        .SetMessage("Unexpected info type")
                        .AddCauseValue(nameof(info), info)
                        .Build();
                }
            }
        }

        public static void ForceDelete(this FileSystemInfo info)
        {
            switch (info)
            {
                case FileInfo fileInfo:
                {
                    fileInfo.Delete();
                    break;
                }
                case DirectoryInfo directoryInfo:
                {
                    directoryInfo.Delete(true);
                    break;
                }
                default:
                {
                    throw new CausedException.Builder()
                        .SetMessage("Unexpected info type")
                        .AddCauseValue(nameof(info), info)
                        .Build();
                }
            }
        }
    }
}
