(function () {
    UI.Components.Report.DiffSummary = React.createClass({

        render: function () {

            if (this.props.summary) {
                var diff = this.props.summary.diff;

                return (
                    <div className="row">
                        <div className="col-sm-12">
                            <UI.Components.Report.DiffSummaryHeader sourceFilename={this.props.summary.sourceFilename} actualFilename={this.props.summary.actualFilename} />
                            <UI.Components.Report.DiffSummaryAddedElements elements={this.props.summary.diff.elements.added} />
                            <UI.Components.Report.DiffSummaryChangedElements elements={this.props.summary.diff.elements.changed} />
                            <UI.Components.Report.DiffSummaryDeletedElements elements={this.props.summary.diff.elements.deleted} />
                        </div>
                    </div>
                );
            }
            return <span />;
        }
    });
})();