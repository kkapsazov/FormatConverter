using System.Collections.Generic;
using log4net;
using Newtonsoft.Json;

namespace FormatConverter.FileHandlers
{
    public class JsonHandler : FileHandler
    {
        public override string Handle(string fileExtension, string fileContent)
        {
            if (fileExtension == ".json")
            {
                string result = JsonConvert.DeserializeXmlNode(fileContent).OuterXml;
                
                logger.Info($"1 JSON file handled");

                return result;
            }
            else
            {
                return base.Handle(fileExtension, fileContent);
            }
        }
    }
}