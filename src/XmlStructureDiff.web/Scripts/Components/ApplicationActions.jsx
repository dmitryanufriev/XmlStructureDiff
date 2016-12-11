(function () {
    UI.Components.ApplicationActions = React.createClass({
        showForm: function () {
            $(ReactDOM.findDOMNode(this.refs.xmlDocumentsForm)).modal('show');
        },

        render: function () {
            return (
                <div className="row">
                    <div className="col-sm-12">
                        <button type="button" className="btn btn-success btn-lg pull-right" onClick={this.showForm}>
                            Сравнить XML документы
                        </button>
                        <UI.Components.XmlDocumentsForm ref="xmlDocumentsForm" bus={this.props.bus}/>
                    </div>
                </div>
            );
        }
    });
})();