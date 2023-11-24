namespace JC.Lib.Files.Extensions
{
    public static class PathExtensions
    {
        public static string GetRelativePath(this string path, string root)
        {
            var result = path[root.Length..];

            return result.TrimStart(Path.DirectorySeparatorChar);
        }

        public static FileSystemInfo? CreateFileSystemInfoBasingOnExistance(this string path)
        {
            if (File.Exists(path))
                return new FileInfo(path);

            else if (Directory.Exists(path))
                return new DirectoryInfo(path);

            else
                return null;
        }
    }
}
