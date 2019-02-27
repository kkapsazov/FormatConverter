using System.Collections.Generic;
using System.Xml;
using log4net;
using Newtonsoft.Json;

namespace FormatConverter.FileHandlers
{
    public class XmlHandler :  FileHandler
    {
        public override string Handle(string fileExtension, string fileContent)
        {
            if (fileExtension == ".xml")
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(fileContent);
                string result = JsonConvert.SerializeXmlNode(doc);
                
                logger.Info($"1 XML file handled");

                return result;
            }
            else
            {
                return base.Handle(fileExtension, fileContent);
            }
        }
    }
}