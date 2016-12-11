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
            var bus = this.props.bus;
            var sourceFilename = this.state.sourceFile.name;
            var actualFilename = this.state.actualFile.name;

            var form = new FormData();
            form.append("source", this.state.sourceFile);
            form.append("actual", this.state.actualFile);

            axios.post("/diff", form)
                .then(function (response) {
                    bus.dispatch("onDiffComplete", {
                        sourceFilename: sourceFilename,
                        actualFilename: actualFilename,
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
                            <div className="form-group">
                                <label htmlFor="source-file" className="control-label">Исходный файл</label>
                                <input id="source-file" type="file" className="form-control" accept="application/xml" onChange={this.setSourceFile} />
                                <span className="help-block">
                                    Исходный файл - это эталонный документ. Относительно структуры этого файла производится
                                    сравнение с актуальный документом.
                                </span>
                            </div>
                            <div className="form-group">
                                <label htmlFor="actual-file" className="control-label">Актуальный файл</label>
                                <input id="actual-file" type="file" className="form-control" accept="application/xml" onChange={this.setActualFile} />
                                <span className="help-block">
                                    Актуальный файл - это новый документ, в котором возможно есть изменения структуры.
                                </span>
                            </div>
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