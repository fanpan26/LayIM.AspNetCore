layui.define(['layim','rmlib', 'protobuf'], function (exports) {
    var lib = layui.rmlib;
    var $ = layui.jquery;
    var layer = layui.layer;
    var layim = layui.layim;
  
    //融云key
    //var rcKey = 'x4vkb1qpx41zk';
    var call = {};
    /*如果服务器没连接上，用户直接发送数据，会将消息保存到queue中，之后连接成功， 执行发送
     *注意:如果用户直接关掉浏览器，则消息丢失,可以通过消息记录的形式保存。
     *正常情况下会有一些连接服务器延迟，所以，用此方案即可，不必存放于localstorage中
     */
    var msgQueue = [];
    var userGroups = [];
    //事件监听
    var listener = function (code) {
        code && (call['status'] ? call['status'](code) : log(code));

    }
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
    }

    //这里引用融云，但是socket接口都是一致的
    //事件有很多 open 
    var socket = {
        start: function (conf) {
            build.resetConf(conf);
            socket.init();
        },
        init: function () {
            layim.config(conf.config);
            im.init();
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
                log('初始化群组信息...');
                userGroups = layim.cache().group || [];
               // request.apply(function (c) {
                //    layim.msgbox(c);
               // })

            });
        },
        open: function () {
            im.connectWithToken();
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

    var tryGetTokenTime = 0;//token重试次数
    //im部分
    var im = {
        connected: false,
        init: function () {
            //初始化融云设置
            log('初始化融云设置,key=' + conf.extend.appKey);
            if (conf.extend.appKey) {
                lib.RongIMClient.init(conf.extend.appKey);
                this.initListener();
                this.defineMessage();
            } else {
                layer.msg('无效的appKey');
            }
        },
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
        getToken: function (callback) {
            //从本地读取
            var t = token.get(conf.uid);

            if (t && t.length > 10) {
                log('从本地获取token');
                callback(t);
                return;
            }
            if (!conf.api.token) {
                layer.msg('请检查config.api.token配置');
                return;
            }
            //根据网络请求获取token
            log("从网络获取token");
            $.get(conf.api.token,function (res) {
                if (res.token && callback) {
                    token.save(res.token);
                    callback(res.token);
                } else {
                    layer.msg('token获取失败，请检查相应配置或者服务端代码');
                }
            });
        },
        connectWithToken: function () {
            im.getToken(function (t) {
                RongIMClient.connect(t, {
                    onSuccess: function (userId) {
                        im.connectSuccess(userId);
                    },
                    onTokenIncorrect: function (msg) {
                        console.log(msg);
                        listener(im.code.errorToken);
                        //重新获取
                        if (tryGetTokenTime <= 1) {
                            log("正在重试");
                            token.reset();
                            im.connectWithToken();
                            tryGetTokenTime++;
                        } else {
                            layer.msg('获取token失败');
                        }

                    },
                    onError: function (errorCode) {
                        var info = '';
                        var code = im.code.errorUnKnown;
                        switch (errorCode) {
                            case RongIMLib.ErrorCode.TIMEOUT:
                                code = im.code.errorTimeout;
                                break;
                            case RongIMLib.ErrorCode.UNKNOWN_ERROR:
                                code = im.code.errorUnKnown;
                                break;
                            case RongIMLib.ErrorCode.UNACCEPTABLE_PaROTOCOL_VERSION:
                                code = im.code.errorVersion;
                                break;
                            case RongIMLib.ErrorCode.IDENTIFIER_REJECTED:
                                code = im.code.errorAppkey;
                                break;
                            case RongIMLib.ErrorCode.SERVER_UNAVAILABLE:
                                code = im.code.errorService;
                                break;
                        }
                        listener(code);
                    }
                });
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
            //加入到群组
            userGroups.map(function (item) {
                im.joinGroup(item.id, item.groupname);
            });
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
                    log('保存消息结果：' + res);
                });
            } else {
                log('未开启保存消息，消息将不被保存，若要开启，请将')
            }
        },
        sendMsg: function (data) {
            //根据layim提供的data数据，进行解析
            var mine = data.mine;
            var to = data.to;
            var id = conf.uid || mine.id;//当前用户id
            var group = to.type == 'group';
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
            }
            //这里要判断消息类型
            var conversationType = group ? lib.ConversationType.GROUP : lib.ConversationType.PRIVATE; //私聊,其他会话选择相应的消息类型即可。
            var targetId = to.id.toString();//这里的targetId必须是string类型，否则会报错
            //构造消息体，这里就是我们刚才已经注册过的自定义消息
            log(msg);

            if (im.connected) {
                var detail = new RongIMClient.RegisterMessage.LAYIM_TEXT_MESSAGE(msg);
                //发送消息
                RongIMClient.getInstance().sendMessage(conversationType, targetId, detail, {
                    onSuccess: function (message) {
                        log('发送消息成功');
                        im.saveMsg(mine.id, targetId, msg.type, msg.content);
                    },
                    onError: function (errorCode) {
                        log(errorCode);
                    }
                });
            } else {
                im.saveMsg(mine.id, targetId, msg.type, msg.content);
            }
        },
        joinGroup: function (gid, gname) {
            var groupId = (gid || '0').toString(); // 群 Id 。
            var groupName = gname;// 群名称 。
            RongIMClient.getInstance().joinGroup(groupId, groupName, {
                onSuccess: function () {
                    log('加入群成功');
                },
                onError: function (error) {
                    // error => 加入群失败错误码。
                }
            });
        },
        //初始化监听
        initListener: function () {
            // 设置连接监听状态 （ status 标识当前连接状态 ）
            // 连接状态监听器
            log('注册服务连接监听事件');
            var code = im.code.errorUnKnown;
            RongIMClient.setConnectionStatusListener({
                onChanged: function (status) {
                    switch (status) {
                        case lib.ConnectionStatus.CONNECTED:
                            code = null;
                            break;
                        case lib.ConnectionStatus.CONNECTING:
                            code = im.code.connecting;
                            break;
                        case lib.ConnectionStatus.DISCONNECTED:
                            code = im.code.disconnect;
                            break;
                        case lib.ConnectionStatus.KICKED_OFFLINE_BY_OTHER_CLIENT:
                            code = im.code.errorOtherLogin;
                            break;
                        case lib.ConnectionStatus.DOMAIN_INCORRECT:
                            code = im.code.errorDomain;
                            break;
                        case lib.ConnectionStatus.NETWORK_UNAVAILABLE:
                            code = im.code.errorNetwork;
                            break;
                    }
                    listener(code);
                }
            });

            // 消息监听器
            RongIMClient.setOnReceiveMessageListener({
                // 接收到的消息
                onReceived: function (message) {
                    log(message);
                    // 判断消息类型
                    switch (message.messageType) {
                        case RongIMClient.MessageType.LAYIM_TEXT_MESSAGE:
                            //message.content.timestamp = message.sentTime;
                            layim.getMessage(message.content);
                            break;

                    }
                }
            });
        },
        //自定义消息
        defineMessage: function () {
            var defineMsg = function (obj) {
                RongIMClient.registerMessageType(obj.msgName, obj.objName, obj.msgTag, obj.msgProperties);
            }
            //注册普通消息
            var textMsg = {
                msgName: 'LAYIM_TEXT_MESSAGE',
                objName: 'LAYIM:CHAT',
                msgTag: new lib.MessageTag(false, false),
                msgProperties: ["username", "avatar", "id", "type", "content"]
            };
            //注册
            log('注册用户自定义消息类型：LAYIM_TEXT_MESSAGE');
            defineMsg(textMsg);

        }
    };
    //token
    var token = {
        save: function (t) {
            layui.data('layim_global', {
                key:'rong_token_'+ conf.uid,
                value: t
            });
        },
        get: function (uid) {
            return layui.data('layim_global')['rong_token_' + uid] || '';
        },
        reset: function () {
            log("重置token");
            this.save('reset');
        }
    };
    var out = {
        info: 'LayIM.AspNetCore',
        version: '1.0',
        author: '645857874@qq.com',
        start: socket.start
    }
    var request = {
        apply: function (callback) {
            $.get('/layim/apply/count?uid=' + conf.uid, function (res) {
                if (res.code == 0) {
                    res.data.apply && callback && callback(res.data.apply)
                } else {
                    layer.msg('获取申请信息失败');
                }
            })
        },
        config: function (callback) {
            console.info(out);
            $.get('/layim/config', function (conf) {
                callback && callback(conf);
            });
        }
    };
    exports('socket', out)
});