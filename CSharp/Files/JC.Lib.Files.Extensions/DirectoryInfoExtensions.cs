using JC.Lib.Files.Abstractions;
using JC.Lib.Files.Models;
using JC.Lib.Files.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JC.Lib.Files.Extensions
{
    public static class DirectoryInfoExtensions
    {
        public static IDirectoryScanEnumerable<T_FileInfo> Scan<T_FileInfo>(this DirectoryInfo directoryInfo, uint? maxDepth)
            where T_FileInfo: FileSystemInfo
        {
            var scanEnumerable = new DirectoryScanEnumerable<T_FileInfo>();
            return scanEnumerable.AddRootInfo(new RootInfo(directoryInfo, maxDepth));
        }

        public static IDirectoryScanEnumerable<T_FileInfo> Scan<T_FileInfo>(this DirectoryInfo directoryInfo, uint? maxDepth, IServiceProvider serviceProvider)
            where T_FileInfo : FileSystemInfo
        {
            var scanProvider = serviceProvider.GetRequiredService<IDirectoryScanServiceProvider>();
            var scanEnumerable = scanProvider.CreateScannerEnumerable<T_FileInfo>();
            
            return scanEnumerable.AddRootInfo(new RootInfo(directoryInfo, maxDepth));
        }
    }
}
