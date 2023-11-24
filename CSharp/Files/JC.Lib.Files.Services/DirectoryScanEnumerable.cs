using JC.Lib.Files.Abstractions;
using JC.Lib.Files.Models;
using System.Collections;

namespace JC.Lib.Files.Services
{
    public class DirectoryScanEnumerable<T_FileInfo> : IDirectoryScanEnumerable<T_FileInfo>
        where T_FileInfo : FileSystemInfo
    {
        protected readonly List<Func<DirectoryInfo, bool>> IgnoredDirectoriesPredicates = new();
        protected readonly List<RootInfo> RootInfos = new();







        public IDirectoryScanEnumerable<T_FileInfo> IgnoreDirectory(Func<DirectoryInfo, bool> predicate)
        {
            IgnoredDirectoriesPredicates.Add(predicate);
            return this;
        }

        public IDirectoryScanEnumerable<T_FileInfo> AddRootInfo(RootInfo rootInfo)
        {
            RootInfos.Add(rootInfo);
            return this;
        }

        public IEnumerable<T_FileInfo> Scan()
        {
            foreach (var rootInfo in RootInfos)
            {
                var values = scan(rootInfo.DirectoryInfo, rootInfo.MaxDepth, 0u);
                foreach (var value in values)
                    yield return value;
            }
        }

        private IEnumerable<T_FileInfo> scan
        (
            DirectoryInfo directoryInfo,
            uint? maxDepth,
            uint currentDepth
        )
        {
            FileSystemInfo[] inners;
            try
            {
                directoryInfo.Refresh();
                if (!directoryInfo.Exists)
                    yield break;

                inners = directoryInfo.GetFileSystemInfos();
            }

            catch (UnauthorizedAccessException)
            {
                inners = new FileSystemInfo[0];
            }

            var nextDepth = currentDepth + 1u;

            foreach (var inner in inners)
            {
                if (inner is T_FileInfo file)
                    yield return file;

                if ((maxDepth is null || nextDepth < maxDepth) && inner is DirectoryInfo innerDirectoryInfo)
                {
                    if (IgnoredDirectoriesPredicates.Any((predicate) => predicate(innerDirectoryInfo)))
                        continue;

                    var values = scan(innerDirectoryInfo, maxDepth, nextDepth);
                    foreach (var value in values)
                        yield return value;
                }
            }
        }

        public IEnumerator<T_FileInfo> GetEnumerator()
        {
            return Scan().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
