(function () {

    UI.Components.Forms.InputXmlFile = React.createClass({
        componentWillMount: function() {
            this.id = Services.UniqueId.generate("InputXmlFile_");
        },

        getInitialState: function () {
            return {
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
            this.setState({
                file: e.target.files[0]
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
                error = <p className="text-danger">Это обязательное поле</p>;
            }

            return (
                <div className="form-group">
                    <label htmlFor={this.id} className="control-label">{this.props.label}</label>
                    <input id={this.id} type="file" className="form-control" accept="application/xml" onChange={this.setFile} />
                    {comment}
                    {error}
                </div>
            );
        }
    });
})();