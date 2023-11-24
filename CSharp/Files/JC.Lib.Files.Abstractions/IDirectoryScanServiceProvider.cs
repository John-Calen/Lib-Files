namespace JC.Lib.Files.Abstractions
{
    public interface IDirectoryScanServiceProvider
    {
        IDirectoryScanEnumerable<T_FileInfo> CreateScannerEnumerable<T_FileInfo>()
            where T_FileInfo : FileSystemInfo;
    }
}
