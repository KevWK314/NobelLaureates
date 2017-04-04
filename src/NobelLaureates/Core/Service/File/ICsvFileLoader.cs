namespace NobelLaureates.Core.Service.File
{
    public interface ICsvFileLoader
    {
        T[] LoadFile<T>(string filename) where T : class;
    }
}
