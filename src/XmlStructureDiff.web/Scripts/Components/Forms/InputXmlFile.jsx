(function () {

    UI.Components.Forms.InputXmlFile = React.createClass({
        componentWillMount: function() {
            this.id = Services.UniqueId.generate("InputXmlFile_");
        },

        getInitialState: function () {
            return {
                filename: "Файл не выбран",
                valid: true
            };
        },

        validate: function () {
            var markup = $(ReactDOM.findDOMNode(this));
            if (this.props.required && !this.state.file) {
                markup.addClass("has-error");
                this.setState({
                    valid: false
                });
                return false;
            } else if (this.state.file) {
                markup.removeClass("has-error");
                this.setState({
                    valid: true
                });
                return true;
            }
        },

        file: function () {
            return this.state.file;
        },

        setFile: function (e) {
            var file = e.target.files[0];
            this.setState({
                file: file,
                filename: file ? file.name : "Файл не выбран"
            }, function () {
                this.validate();
            });
        },

        render: function () {
            var comment = null;
            if (this.props.comment && this.state.valid) {
                comment = <span className="help-block">{this.props.comment}</span>;
            }

            var error = null;
            if (!this.state.valid) {
                error = <p className="text-danger">Нужно обязательно выбрать файл</p>;
            }

            return (
                <div className="form-group">
                    <label className="control-label">{this.props.label}</label>
                    <br />
                    <label htmlFor={this.id} className="btn btn-default btn-sm" >
                        <input id={this.id} type="file" accept="application/xml" style={{ display: "none" }} onChange={this.setFile} />
                        Выбрать файл...
                    </label>
                    &nbsp;
                    <span>{this.state.filename}</span>
                    {comment}
                    {error}
                </div>
            );
        }
    });
})();