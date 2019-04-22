using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MtomReader
{
    public class testController : Controller
    {

        [HttpPost]
        [Route("test")]
        public async Task<string> Iti41()
        {
            var boundary = Request.GetMultipartBoundary();

            var reader = new MultipartReader(boundary, Request.Body);
            bool done = false;
            while (!done)
            {
                var section = await reader.ReadNextSectionAsync();
                if (section != null)
                {
                    foreach (var header in section.Headers)
                    {
                        Console.WriteLine($"{header.Key}: {header.Value}");
                    }

                    XDocument.Load(section.Body);

                    //var body = await section.ReadAsStringAsync();
                    //Console.WriteLine(body);

                }
                else
                {
                    done = true;
                }

            }
            return "Hello World";
        }
    }
}
