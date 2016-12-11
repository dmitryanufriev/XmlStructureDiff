(function () {
    UI.Components.XmlDocumentsForm = React.createClass({

        getInitialState: function () {
            return {
                sourceFile: undefined,
                actualFile: undefined
            };
        },

        setSourceFile: function (e) {
            this.setState({
                sourceFile: e.target.files[0],
                actualFile: this.state.actualFile
            });
        },

        setActualFile: function (e) {
            this.setState({
                sourceFile: this.state.sourceFile,
                actualFile: e.target.files[0]
            });
        },

        sendFiles: function () {
            var sourceFile = this.refs.sourceFile;
            var actualFile = this.refs.actualFile;

            if (!sourceFile.validate() | !actualFile.validate()) {
                return;
            }

            var bus = this.props.bus;
            var form = new FormData();
            form.append("source", sourceFile.file());
            form.append("actual", actualFile.file());

            axios.post("/diff", form)
                .then(function (response) {
                    bus.dispatch("onDiffComplete", {
                        sourceFilename: sourceFile.file().name,
                        actualFilename: actualFile.file().name,
                        diff: response.data
                    });
                });

            $(ReactDOM.findDOMNode(this)).modal('hide');
        },

        render: function () {
            return (
                <div className="modal fade" tabIndex="-1" role="dialog">
                  <div className="modal-dialog" role="document">
                    <div className="modal-content">
                      <div className="modal-header">
                        <button type="button" className="close" data-dismiss="modal" aria-label="Закрыть"><span aria-hidden="true">&times;</span></button>
                        <h4 className="modal-title">Выберите файлы документов</h4>
                      </div>
                      <div className="modal-body">
                        <form>
                            <UI.Components.Forms.InputXmlFile ref="sourceFile" required="true" label="Исходный файл" comment="Исходный файл - это эталонный документ. Относительно структуры этого файла производится сравнение с актуальный документом." />
                            <UI.Components.Forms.InputXmlFile ref="actualFile" required="true" label="Актуальный файл" comment="Актуальный файл - это новый документ, в котором возможно есть изменения структуры." />
                        </form>
                      </div>
                      <div className="modal-footer">
                        <button type="button" className="btn btn-default" data-dismiss="modal">Закрыть</button>
                        <button type="button" className="btn btn-primary" onClick={this.sendFiles}>Сравнить</button>
                      </div>
                    </div>
                  </div>
                </div>
            );
        }
    });
})();