(function () {
    UI.Application = React.createClass({
        componentWillMount: function() {
            this.props.bus.subscribe("onDiffComplete", this.showDiff);
        },

        getInitialState: function() {
            return {};
        },

        showDiff: function (summary) {
            this.setState({ summary: summary });
        },

        render: function () {
            return (
                <div className="row">
                    <div className="col-sm-12">
                        <div className="page-header">
                            <h1>Контур.РПН <small>XML</small></h1>
                        </div>
                        <UI.Components.ApplicationActions bus={this.props.bus} />
                        <br />
                        <UI.Components.Report.DiffSummary summary={this.state.summary} />
                    </div>
                </div>
            );
        }
    });

    ReactDOM.render(<UI.Application bus={ Services.EventEmitter } />, document.getElementById("application-container"));
})();