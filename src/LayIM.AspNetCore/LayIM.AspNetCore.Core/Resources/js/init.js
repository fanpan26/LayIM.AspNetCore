layui.use('jquery', function () {
    var $ = layui.jquery;
    $(function () {
        var callback = function (conf) {
            layui.extend(conf.extend);
            layui.use(conf.extend.init, function (socket) {
                socket.start(conf);
            });
        };
        var config = layui.data('layim_global')['config'];
        if (config) {
            callback(config);
        }
        $.get('/layim/config', function (conf) {
            layui.data('layim_global', {
                key: 'config',
                value: conf
            });
            if (!config) {
                callback(conf);
            }
        });
    });
});