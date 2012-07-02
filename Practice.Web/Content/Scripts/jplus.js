(function () {
    if (window.jPlus)
        return;

    function loadJPlus() {
        if (window && 'jPlus' in window) {
            try {
                loadModules();
            } catch (e) {
            }
        }
    }
    
    function loadModules() {
        
    }

    loadJPlus();
})();
/*

function output() {
    var text, args, i;
    if (arguments.length > 1) {
        args = new Array(arguments.length);
        for (i = 0; i < arguments.length; args[i] = arguments[i++]);
        text = args.join('');
    } else {
        text = arguments[0] || '';
    }
    document.write(text);
    document.close();
}

function outputln(s) {
    output(s, '<br/>');
}
*/