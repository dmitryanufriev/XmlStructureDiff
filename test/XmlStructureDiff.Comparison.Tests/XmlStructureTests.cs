using NUnit.Framework;
using System.Linq;
using System.Xml.Linq;

namespace XmlStructureDiff.Comparison.Tests
{
    [TestFixture]
    public class XmlStructureTests
    {
        private XmlStructure xmlStructure;

        [SetUp]
        public void Setup()
        {
            var document = XDocument.Parse("<root><child><element /><element /></child></root>");
            xmlStructure = new XmlStructure(document);
        }

        [Test]
        public void FindAbsoluteElementsPaths_Should_Return_Paths_Without_Duplicates()
        {
            Assert.That(xmlStructure.FindAbsoluteElementsPaths(), Is.EquivalentTo(new[] { "/root", "/root/child", "/root/child/element" }));
        }

        [Test]
        public void FindElements_Should_Return_Elements()
        {
            Assert.That(xmlStructure.FindElements("/root").Count(), Is.EqualTo(1));
            Assert.That(xmlStructure.FindElements("/root/child/element").Count(), Is.EqualTo(2));
        }

        [Test]
        public void FindElements_Should_Return_Empty_When_Elements_Not_Found()
        {
            Assert.That(xmlStructure.FindElements("/root/element"), Is.Empty);
        }
    }
}
