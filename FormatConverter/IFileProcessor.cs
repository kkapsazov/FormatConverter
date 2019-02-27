using System.IO;

namespace FormatConverter
{
    public interface IFileProcessor
    {
        string Process(Stream file, string fileName);
    }
}