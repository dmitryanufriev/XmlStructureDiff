(function () {
    window["{DB73E881-866D-4ABB-8755-6684E2D64D15}"] = window["{DB73E881-866D-4ABB-8755-6684E2D64D15}"] || 0;
    var globalCounter = window["{DB73E881-866D-4ABB-8755-6684E2D64D15}"];

    Services.UniqueId = {
        generate: function (prefix) {
            globalCounter++;
            var id = globalCounter.toString();
            if (prefix) {
                return prefix.toString() + id;
            }
            return id;
        }
    };
})();