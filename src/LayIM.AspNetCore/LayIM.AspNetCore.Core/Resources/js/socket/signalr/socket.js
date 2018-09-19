layui.define(['layim','signalr'], function (exports) {
    var $ = layui.jquery;
    var layim = layui.layim;
  
    var call = {};
    /*如果服务器没连接上，用户直接发送数据，会将消息保存到queue中，之后连接成功， 执行发送
     *注意:如果用户直接关掉浏览器，则消息丢失,可以通过消息记录的形式保存。
     *正常情况下会有一些连接服务器延迟，所以，用此方案即可，不必存放于localstorage中
     */
    var msgQueue = [];
    //事件监听
    var listener = function (code) {
        code && (call['status'] ? call['status'](code) : log(code));
    };
    //全局配置
    var conf = {
        api: {},
        config: {},
        other: {},
        uid: ''
    };
    //记录日志
    var log = function (msg) {
        conf.other.log && console.log(msg);
    };

    //这里引用融云，但是socket接口都是一致的
    //事件有很多 open 
    var socket = {
        start: function (conf) {
            build.resetConf(conf);
            socket.init();
        },
        init: function () {
            layim.config(conf.config);
            this.open();
            this.register();
        },
        register: function () {
            //监听发送消息
            layim.on('sendMessage', function (data) {
                //调用socket方法，发送消息
                log(data);
                //layim.setChatStatus('<span style="color:#FF5722;">在线</span>');
                im.sendMsgWithQueue(data);
            });
            //监听ready事件
            layim.on('ready', function () {
                log('layim初始化完毕...');
            });
        },
        open: function () {
            im.connect();
        },
        on: function (event, callback) {
            call[event] ? '' : call[event] = callback;
        }
    };

    var build = {
        resetConf: function (c) {
            var conf0 = {
                init: {
                    url: c.api.base
                }
                , members: {
                    url: c.api.member
                }
            };
            $.extend(conf0, c.config);
            c.config.init = { url: c.api.base };
            c.config.members = { url: c.api.member };
            c.config.uploadFile && (c.config.uploadFile = { url: c.api.up_file });
            c.config.uploadImage && (c.config.uploadImage = { url: c.api.up_img });

            $.extend(conf, c);
            log('初始化完毕，配置信息为：');
          
            log(conf);
        }
    };
    //im部分
    var connection;
    var im = {
        connected: false,
        code: {
            init: { code: 0, msg: '配置初始化，尚未连接' },
            success: { code: 10000, msg: '连接成功' },
            usuccess: { code: 10000, msg: '用户连接成功' },
            connecting: { code: 10001, msg: '正在连接' },
            disconnect: { code: 10002, msg: '断开连接' },
            errorNetwork: { code: 10003, msg: '网络不可用' },
            errorOtherLogin: { code: 10004, msg: '异地登录' },
            errorDomain: { code: 10005, msg: '域名不正确' },
            errorToken: { code: 10006, msg: 'token 无效' },
            errorTimeout: { code: 10007, msg: '超时' },
            errorUnKnown: { code: 10008, msg: '未知错误' },
            errorVersion: { code: 10009, msg: '不可接受的协议版本' },
            errorAppkey: { code: 10010, msg: 'appkey 无效' },
            errorService: { code: 10011, msg: '服务器不可用' }
        },
        connect: function () {
            let hubRoute = "layimHub";
            let protocol = new signalR.JsonHubProtocol();
            var options = {};
            options.accessTokenFactory = () => "testToken";
            connection = new signalR.HubConnectionBuilder()
                .configureLogging(signalR.LogLevel.Trace)
                .withUrl(hubRoute, options)
                .withHubProtocol(protocol)
                .build();
            //receive message
            connection.on('Receive', im.handle);
            connection.onclose(function (e) {
                if (e) {
                    addLine('message-list', 'Connection closed with error: ' + e, 'red');
                }
                else {
                    addLine('message-list', 'Disconnected', 'green');
                }

                log('连接已关闭' + e ? e : '');
            });
            connection.start()
                .then(function () {
                    im.connectSuccess(10001);
                })
                .catch(function (err) {
                    log('服务器连接失败：' + err);
                });
        },
        connectSuccess: function (uid) {
            im.code.usuccess.uid = uid;
            im.connected = true;//连接成功
            listener(im.code.usuccess);

            if (msgQueue.length) {
                //队列中有消息，发送出去
                for (var i = 0; i < msgQueue.length; i++) {
                    im.sendMsg(msgQueue[i]);
                }
                msgQueue = [];
            }
        },
        sendMsgWithQueue: function (data) {
            if (!im.connected) {
                log('当前服务器未连接，将消息加入到未发送队列');
                msgQueue.push(data);
            }
            this.sendMsg(data);

        },
        saveMsg: function (uid, toid, type, msg) {
            if (conf.other.saveMsg) {
                $.post(conf.api.save, { "to": toid, "type": type, "msg": msg }, function (res) {
                    log('保存消息结果：' + res.code);
                });
            } else {
                log('未开启保存消息，消息将不被保存.若要开启，请将[options.OtherConfig.SaveMsgAfterSend]设置为true');
            }
        },
        sendMsg: function (data) {
            //根据layim提供的data数据，进行解析
            var mine = data.mine;
            var to = data.to;
            var id = conf.uid || mine.id;//当前用户id
            var group = to.type === 'group';
            if (group) {
                id = to.id;//如果是group类型，id就是当前groupid，切记不可写成 mine.id否则会出现一些问题。
            }
            //构造消息
            var msg = {
                username: mine.username
                , avatar: mine.avatar
                , id: id
                , type: to.type
                , content: mine.content
            };
            var targetId = to.id.toString();//这里的targetId必须是string类型，否则会报错
            //构造消息体，这里就是我们刚才已经注册过的自定义消息
            log(msg);

            if (im.connected) {
                this.invoke(connection, 'SendMessage', targetId, msg);
            } else {
                im.saveMsg(mine.id, targetId, msg.type, msg.content);
            }
        },
        invoke: function () {
            if (!im.connected) {
                return;
            }
            var argsArray = Array.prototype.slice.call(arguments);
            connection.invoke.apply(connection, argsArray.slice(1))
                .then(function (result) {
                    if (result) {
                        log(result);
                    }
                }).catch(function (err) {
                    log(err);
                });
        },
        handle: function (msg) {
            switch (msg.type) {
                case 0:
                    log(msg.msg.content);
                    break;
                case 1:
                    msg.msg.id = '10002';
                    layim.getMessage(msg.msg);
                    break;
            }
        }
    };

    var out = {
        info: 'LayIM.AspNetCore',
        version: '1.0',
        author: '645857874@qq.com',
        start: socket.start
    };
    exports('socket', out);
});