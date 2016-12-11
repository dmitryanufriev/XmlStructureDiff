using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace XmlStructureDiff.Comparison
{
    public class XmlElementAttributesDiff
    {
        private readonly string xpath;
        private readonly XmlStructure source;
        private readonly XmlStructure actual;

        public XmlElementAttributesDiff(string xpath, XmlStructure source, XmlStructure actual)
        {
            #region Проверка аргументов на null

            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentNullException("xpath", "Путь к элементу обязательно должен быть указан.");
            }

            if (source == null)
            {
                throw new ArgumentNullException("source", "Структура документа-источника обязательно должна быть указана.");
            }

            if (actual == null)
            {
                throw new ArgumentNullException("actual", "Структура актуального документа обязательно должна быть указана.");
            }

            #endregion

            this.xpath = xpath;
            this.source = source;
            this.actual = actual;
        }

        public string XmlElementPath => xpath;

        /// <summary>
        /// Найти имена добавленных атрибутов - атрибутов, которые присутствуют в структуре актуального
        /// документа, но отсутствуют в структуре исходного документа.
        /// </summary>
        /// <returns>Коллекция имён атрибутов</returns>
        public IEnumerable<string> FindAddedXmlAttributesNames()
        {
            var sourceElementAttributesNames = GetElementsAttributesNames(xpath, source);
            var actualElementAttributesNames = GetElementsAttributesNames(xpath, actual);

            return actualElementAttributesNames.Except(sourceElementAttributesNames);
        }

        /// <summary>
        /// Найти имена удалённых атрибутов - атрибутов, которые присутствуют в структуре исходного документа,
        /// но отсутствуют в структуре актуального документа.
        /// </summary>
        /// <returns>Коллекция имён атрибутов</returns>
        public IEnumerable<string> FindDeletedXmlAttributesNames()
        {
            var sourceElementAttributesNames = GetElementsAttributesNames(xpath, source);
            var actualElementAttributesNames = GetElementsAttributesNames(xpath, actual);

            return sourceElementAttributesNames.Except(actualElementAttributesNames);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherDiff = (XmlElementAttributesDiff)obj;

            return xpath.Equals(otherDiff.xpath) &&
                   actual.Equals(otherDiff.actual) &&
                   source.Equals(otherDiff.source);
        }

        public override int GetHashCode()
        {
            // Чтобы не ругаться на переполнение из-за большого числа
            unchecked
            {
                int hash = (int)2166136261;

                hash = (hash * 16777619) ^ xpath.GetHashCode();
                hash = (hash * 16777619) ^ source.GetHashCode();
                hash = (hash * 16777619) ^ actual.GetHashCode();

                return hash;
            }
        }

        private static IEnumerable<string> GetElementsAttributesNames(string xpath, XmlStructure structure)
        {
            var elements = structure.FindElements(xpath);

            return elements.SelectMany(element => GetAttributesNames(element)).Distinct();
        }

        private static IEnumerable<string> GetAttributesNames(XElement element)
        {
            return element.Attributes().Select(attr => attr.Name.LocalName);
        }
    }
}
