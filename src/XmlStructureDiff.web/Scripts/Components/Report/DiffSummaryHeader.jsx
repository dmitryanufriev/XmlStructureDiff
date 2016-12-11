(function () {
    UI.Components.Report.DiffSummaryHeader = React.createClass({
        render: function () {
            return (
                <table className="table">
                    <tbody>
                        <tr>
                            <td>Исходный файл</td>
                            <td>{this.props.sourceFilename}</td>
                        </tr>
                        <tr>
                            <td>Актуальный файл</td>
                            <td>{this.props.actualFilename}</td>
                        </tr>
                    </tbody>
                </table>
            );
        }
    });
})();