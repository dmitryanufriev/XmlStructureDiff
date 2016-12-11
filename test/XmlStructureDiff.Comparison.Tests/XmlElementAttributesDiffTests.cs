using NUnit.Framework;
using System.Xml.Linq;

namespace XmlStructureDiff.Comparison.Tests
{
    [TestFixture]
    public class XmlElementAttributesDiffTests
    {
        [Test]
        public void FindDeletedXmlAttributesNames_Should_Return_Names_From_Source()
        {
            var source = XDocument.Parse("<root attr=\"\" />");
            var actual = XDocument.Parse("<root />");
            var diff = new XmlElementAttributesDiff("/root", new XmlStructure(source), new XmlStructure(actual));

            var deletedAttributesNames = diff.FindDeletedXmlAttributesNames();

            Assert.That(deletedAttributesNames, Is.EquivalentTo(new[] { "attr" }));
        }

        [Test]
        public void FindDeletedXmlAttributesNames_Should_Process_Attributes_From_All_Elements()
        {
            var source = XDocument.Parse("<root><child attr=\"\" /><child attr=\"\" optionalAttr=\"\" /></root>");
            var actual = XDocument.Parse("<root><child attr=\"\" /></root>");
            var diff = new XmlElementAttributesDiff("/root/child", new XmlStructure(source), new XmlStructure(actual));

            var deletedAttributesNames = diff.FindDeletedXmlAttributesNames();

            Assert.That(deletedAttributesNames, Is.EquivalentTo(new[] { "optionalAttr" }));
        }

        [Test]
        public void FindAddedXmlAttributesNames_Should_Return_Names_From_Actual()
        {
            var source = XDocument.Parse("<root />");
            var actual = XDocument.Parse("<root attr=\"\" />");
            var diff = new XmlElementAttributesDiff("/root", new XmlStructure(source), new XmlStructure(actual));

            var addedAttributesNames = diff.FindAddedXmlAttributesNames();

            Assert.That(addedAttributesNames, Is.EquivalentTo(new[] { "attr" }));
        }

        [Test]
        public void FindAddedXmlAttributesNames_Should_Process_Attributes_From_All_Elements()
        {
            var source = XDocument.Parse("<root><child attr=\"\" /></root>");
            var actual = XDocument.Parse("<root><child attr=\"\" /><child attr=\"\" optionalAttr=\"\" /></root>");
            var diff = new XmlElementAttributesDiff("/root/child", new XmlStructure(source), new XmlStructure(actual));

            var addedAttributesNames = diff.FindAddedXmlAttributesNames();

            Assert.That(addedAttributesNames, Is.EquivalentTo(new[] { "optionalAttr" }));
        }
    }
}
