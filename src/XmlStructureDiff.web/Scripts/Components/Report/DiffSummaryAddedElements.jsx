(function () {
    UI.Components.Report.DiffSummaryAddedElements = React.createClass({
        render: function () {

            return (
                <table className="table table-bordered table-hover">
                    <caption>Добавленные элементы: {this.props.elements.length}</caption>
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
                                    <td>Элемент отсутствует в исходном документе.</td>
                                </tr>
                            );
                        })}
                    </tbody>
                </table>
            );
        }
    });
})();