using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using FormatConverter.Exceptions;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormatConverter.Controllers
{
    [Authorize]
    public class FormatConverterController : Controller
    {
        private IFileProcessor fileProcessor;
        private ILog logger;
        public FormatConverterController(IFileProcessor fileProcessor)
        {
            this.fileProcessor = fileProcessor;
            this.logger = LogManager.GetLogger(typeof(FormatConverterController));
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("FormatConverter")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            Dictionary<string, string> downloads = new Dictionary<string, string>();
            
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<Task> tasks = new List<Task>();
            
            foreach (var file in files)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    using (var stream = file.OpenReadStream())
                    {
                        try
                        {
                            string result = fileProcessor.Process(stream, file.FileName);
                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            downloads.Add($"{fileName}.txt", result);
                        }
                        catch (UnsupportedFileFormatException e)
                        {
                            logger.Error($"Unsupported file {file.FileName}");
                        }
                        catch (Exception e)
                        {
                            logger.Error($"Error processing file {file.FileName}: {e.Message}");
                        }
                    }
                }));
            }

            await Task.WhenAll(tasks);
            sw.Stop();
            
            logger.Info($"{downloads.Count} files processed for {sw.ElapsedMilliseconds} ms");
            
            return View("Downloads", downloads);
        } 
    }
}