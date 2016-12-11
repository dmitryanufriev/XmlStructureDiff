using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using XmlStructureDiff.Comparison;
using XmlStructureDiff.Reporting;

namespace XmlStructureDiff.web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CompareXmlStructure(IFormFile source, IFormFile actual)
        {
            var sourceXml = GetXDocument(source);
            var actualXml = GetXDocument(actual);

            var diff = new XmlStructure(sourceXml).GetDifferences(new XmlStructure(actualXml));

            return base.Content(new XmlStructureDiffSummary(diff).ToJson(), "application/json");
        }

        private XDocument GetXDocument(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                return XDocument.Load(stream);
            }
        }
    }
}
