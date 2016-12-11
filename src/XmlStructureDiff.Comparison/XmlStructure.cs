using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XmlStructureDiff.Comparison
{
    /// <summary>
    /// Класс объектов, представляющих структуру XML документа.
    /// </summary>
    public class XmlStructure
    {
        private readonly XDocument source;

        public XmlStructure(XDocument document)
        {
            source = document;
        }

        /// <summary>
        /// Найти абсолютные пути для всех элементов в XML.
        /// </summary>
        /// <remarks>
        /// Найденные пути уникальны, то есть для двух элементов, находящихся на
        /// одном уровне, вернётся один путь. Например, для <child/><child/>
        /// вернётся только /child.
        /// </remarks>
        /// <returns>Набор путей в XML документе.</returns>
        public IEnumerable<string> FindAbsoluteElementsPaths()
        {
            var paths = new HashSet<string>();
            foreach (XElement element in source.Descendants())
            {
                var branch = element.AncestorsAndSelf().Reverse();
                var xpath = $"/{string.Join("/", branch.Select(e => e.Name.LocalName))}";
                paths.Add(xpath);
            }
            return paths;
        }

        /// <summary>
        /// Найти все элементы, которые соответствуют переданному <paramref name="xpath"/>.
        /// </summary>
        /// <param name="xpath">Путь к элементам (выражение XPath)</param>
        /// <returns>
        /// Набор элементов, которые соответствуют переданному <paramref name="xpath"/>
        /// или пустой набор, если элементы по переданному <paramref name="xpath"/>
        /// не найдены.
        /// </returns>
        public IEnumerable<XElement> FindElements(string xpath)
        {
            return source.XPathSelectElements(xpath);
        }

        /// <summary>
        /// Вернуть различия между структурами документов.
        /// </summary>
        /// <param name="actual">Структура документа, с которой сравнивается текущая структура.</param>
        /// <returns><see cref="XmlStructureDiff"/></returns>
        public XmlStructureDiff GetDifferences(XmlStructure actual)
        {
            return new XmlStructureDiff(this, actual);
        }
    }
}
