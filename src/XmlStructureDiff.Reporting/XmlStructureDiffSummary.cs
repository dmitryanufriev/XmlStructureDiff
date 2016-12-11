using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using XmlStructureDiff;

namespace XmlStructureDiff.Reporting
{
    public class XmlStructureDiffSummary
    {
        private readonly Comparison.XmlStructureDiff diff;

        public XmlStructureDiffSummary(Comparison.XmlStructureDiff xmlStructureDiff)
        {
            diff = xmlStructureDiff;
        }

        public string ToJson()
        {
            var addedElementsPaths = diff.FindAddedElementsAbsolutePaths();
            var addedElementsSection = GetSummarySection(addedElementsPaths);

            var changedElementsSection = new List<object>();
            // TODO: Сделать возвращение diff'а только для тех элементов у которых есть
            // изменения в атрибутах
            var elementsAttributesDiffs = diff.GetAttributesDifferences();
            foreach (var elementAttributesDiff in elementsAttributesDiffs)
            {
                var addedAttributesNames = elementAttributesDiff.FindAddedXmlAttributesNames();
                var deletedAttributesNames = elementAttributesDiff.FindDeletedXmlAttributesNames();

                if (addedAttributesNames.Count() == 0 && deletedAttributesNames.Count() == 0)
                {
                    continue;
                }

                var changedElement = new
                {
                    name = GetElementName(elementAttributesDiff.XmlElementPath),
                    attributes = new
                    {
                        added = elementAttributesDiff.FindAddedXmlAttributesNames(),
                        deleted = elementAttributesDiff.FindDeletedXmlAttributesNames()
                    },
                    path = elementAttributesDiff.XmlElementPath
                };

                changedElementsSection.Add(changedElement);
            }

            var deletedElementsPaths = diff.FindDeletedElementsAbsolutePaths();
            var deletedElementsSection = GetSummarySection(deletedElementsPaths);

            var summary = new
            {
                elements = new
                {
                    added = addedElementsSection,
                    changed = changedElementsSection,
                    deleted = deletedElementsSection
                }
            };

            return JsonConvert.SerializeObject(summary);
        }

        private static List<object> GetSummarySection(IEnumerable<string> elementsPaths)
        {
            var section = new List<object>();
            foreach (var path in elementsPaths)
            {
                section.Add(new
                {
                    name = GetElementName(path),
                    path = path
                });
            }
            return section;
        }

        private static string GetElementName(string path)
        {
            var matchedName = Regex.Match(path, @"\b\w*$").Value;
            if (string.IsNullOrEmpty(matchedName))
            {
                return path;
            }
            return matchedName;
        }
    }
}