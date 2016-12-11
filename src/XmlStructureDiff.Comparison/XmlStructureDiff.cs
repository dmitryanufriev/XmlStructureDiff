using System.Collections.Generic;
using System.Linq;

namespace XmlStructureDiff.Comparison
{
    /// <summary>
    /// Класс объектов, представляющих различия в <see cref="XmlStructure"/>.
    /// </summary>
    public class XmlStructureDiff
    {
        private readonly XmlStructure actualXmlStructure;
        private readonly XmlStructure sourceXmlStructure;

        /// <summary>
        /// Конструктор объектов данного класса.
        /// </summary>
        /// <param name="sourceXmlStructure"><see cref="XmlStructure"/> исходного XML документа.</param>
        /// <param name="actualXmlStructure"><see cref="XmlStructure"/> актуального XML документа.</param>
        public XmlStructureDiff(XmlStructure sourceXmlStructure, XmlStructure actualXmlStructure)
        {
            this.sourceXmlStructure = sourceXmlStructure;
            this.actualXmlStructure = actualXmlStructure;
        }

        /// <summary>
        /// Найти абсолютные пути всех удалённых элементов. То есть, элементов, которые
        /// отсутствуют в структуре актуального документа, но есть в текущей структуре.
        /// </summary>
        /// <returns>Набор абсолютных путей в XML документе.</returns>
        public IEnumerable<string> FindDeletedElementsAbsolutePaths()
        {
            var sourcePaths = sourceXmlStructure.FindAbsoluteElementsPaths();
            var actualPaths = actualXmlStructure.FindAbsoluteElementsPaths();

            return sourcePaths.Except(actualPaths);
        }

        /// <summary>
        /// Найти абсолютные пути всех добавленных (новых) элементов. То есть, элементов, которые
        /// отсутствуют в структуре текущего документа, но есть в структуре актуального
        /// документа.
        /// </summary>
        /// <returns>Набор абсолютных путей в XML документе.</returns>
        public IEnumerable<string> FindAddedElementsAbsolutePaths()
        {
            var sourcePaths = sourceXmlStructure.FindAbsoluteElementsPaths();
            var actualPaths = actualXmlStructure.FindAbsoluteElementsPaths();

            return actualPaths.Except(sourcePaths);
        }

        /// <summary>
        /// Вернуть результат сравнения общих элементов двух документов. То есть,
        /// элементов, которые присутствуют как в структуре документа-источника, так и в
        /// структуре актуального документа.
        /// </summary>
        /// <returns>Набор <see cref="XmlElementAttributesDiff"/> для общих элементов двух документов.</returns>
        public IEnumerable<XmlElementAttributesDiff> GetAttributesDifferences()
        {
            var sourcePaths = sourceXmlStructure.FindAbsoluteElementsPaths();
            var actualPaths = actualXmlStructure.FindAbsoluteElementsPaths();
            var commonPaths = sourcePaths.Intersect(actualPaths);

            return commonPaths.Select(path => new XmlElementAttributesDiff(path, sourceXmlStructure, actualXmlStructure));
        }
    }
}
