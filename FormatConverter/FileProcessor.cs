using System.IO;
using FormatConverter.FileHandlers;

namespace FormatConverter
{
    public class FileProcessor : IFileProcessor
    {
        private FileHandler handlersChain;
        public FileProcessor()
        {
            FileHandler json = new JsonHandler();
            FileHandler xml = new XmlHandler();
            
            json.SetNext(xml);

            handlersChain = json;
        }

        public string Process(Stream file, string fileName)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                string fileExtension = Path.GetExtension(fileName);
                string fileContent = sr.ReadToEnd();

                return handlersChain.Handle(fileExtension, fileContent);
            }
        }
    }
}