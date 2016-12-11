using NUnit.Framework;
using System.Linq;
using System.Xml.Linq;

namespace XmlStructureDiff.Comparison.Tests
{
    [TestFixture]
    public class XmlStructureDiffTests
    {
        [Test(Description = "Есть в исходном документе, но отсутствуют в актуальном документе")]
        public void FindDeletedElementsAbsolutePaths_Should_Return_Paths_From_Source()
        {
            var sourceDocument = XDocument.Parse("<root><child><element /></child></root>");
            var actualDocument = XDocument.Parse("<root />");
            var sourceXmlStructure = new XmlStructure(sourceDocument);
            var actualXmlStructure = new XmlStructure(actualDocument);
            var xmlStructureDiff = new XmlStructureDiff(sourceXmlStructure, actualXmlStructure);
            var paths = xmlStructureDiff.FindDeletedElementsAbsolutePaths().ToArray();

            Assert.That(paths, Is.EquivalentTo(new[] { "/root/child", "/root/child/element" }));
        }

        [Test(Description = "Есть в актуальном документе, но отстутствует в исходном документе")]
        public void FindAddedElementsAbsolutePaths_Should_Return_Paths_From_Actual()
        {
            var sourceDocument = XDocument.Parse("<root/>");
            var actualDocument = XDocument.Parse("<root><child><element/></child></root>");
            var sourceXmlStructure = new XmlStructure(sourceDocument);
            var actualXmlStructure = new XmlStructure(actualDocument);
            var xmlStructureDiff = new XmlStructureDiff(sourceXmlStructure, actualXmlStructure);
            var paths = xmlStructureDiff.FindAddedElementsAbsolutePaths().ToArray();

            Assert.That(paths, Is.EquivalentTo(new[] { "/root/child", "/root/child/element" }));
        }

        [Test]
        public void GetAttributesDifferences_Should_Return_Diffs_For_All_Common_Elements()
        {
            var sourceDocument = XDocument.Parse("<root><child><element/></child></root>");
            var actualDocument = XDocument.Parse("<root><child/></root>");
            var sourceXmlStructure = new XmlStructure(sourceDocument);
            var actualXmlStructure = new XmlStructure(actualDocument);
            var commonElementsDiffs = new[] {
                new XmlElementAttributesDiff("/root", sourceXmlStructure, actualXmlStructure),
                new XmlElementAttributesDiff("/root/child", sourceXmlStructure, actualXmlStructure),
            };

            var xmlStructureDiff = new XmlStructureDiff(sourceXmlStructure, actualXmlStructure);
            var paths = xmlStructureDiff.GetAttributesDifferences().ToArray();

            Assert.That(paths, Is.EquivalentTo(commonElementsDiffs));
        }
    }
}