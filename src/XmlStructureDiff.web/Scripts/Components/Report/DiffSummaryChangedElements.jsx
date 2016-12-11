(function () {
    UI.Components.Report.DiffSummaryChangedElements = React.createClass({
        render: function () {

            return (
                <table className="table table-bordered table-hover">
                    <caption>Изменения в атрибутах: {this.props.elements.length}</caption>
                    <thead>
                        <tr>
                            <th>Элемент</th>
                            <th>Путь</th>
                            <th>Комментарий</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.elements.map(function (element, i) {
                            return (
                                <tr key={i}>
                                    <td>{element.name}</td>
                                    <td>{element.path}</td>
                                    <td>
                                        <p>{element.attributes.added.length > 0 ? "Добавлены атрибуты: " + element.attributes.added.join(", ") : null}</p>
                                        <p>{element.attributes.deleted.length > 0 ? "Удалены атрибуты: " + element.attributes.deleted.join(", ") : null}</p>
                                    </td>
                                </tr>
                            );
                        })}
                    </tbody>
                </table>
            );
        }
    });
})();