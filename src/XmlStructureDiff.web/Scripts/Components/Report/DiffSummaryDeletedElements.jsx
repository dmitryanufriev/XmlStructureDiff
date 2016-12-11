(function () {
    UI.Components.Report.DiffSummaryDeletedElements = React.createClass({
        render: function () {

            return (
                <table className="table table-bordered table-hover">
                    <caption>Удалённые элементы: {this.props.elements.length}</caption>
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
                                    <td>Элемент отсутствует в актуальном документе.</td>
                                </tr>
                            );
                        })}
                    </tbody>
                </table>
            );
        }
    });
})();