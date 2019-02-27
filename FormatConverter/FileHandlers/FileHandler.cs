using System.Collections.Generic;
using System.IO;
using FormatConverter.Exceptions;
using log4net;

namespace FormatConverter.FileHandlers
{
    public abstract class FileHandler
    {
        private FileHandler nextHandler;
        protected ILog logger;

        public FileHandler()
        {                
            this.logger = LogManager.GetLogger(typeof(FileHandler));
        }

        public FileHandler SetNext(FileHandler handler)
        {
            this.nextHandler = handler;
            
            return handler;
        }
        
        public virtual string Handle(string fileExtension, string fileContent)
        {
            if (this.nextHandler != null)
            {
                return this.nextHandler.Handle(fileExtension, fileContent);
            }
            else
            {
                throw new UnsupportedFileFormatException();
            }
        }
    }
}