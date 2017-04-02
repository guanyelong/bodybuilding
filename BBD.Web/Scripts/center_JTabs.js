/* tab页右键菜单 */
//刷新tab页
function center_tabMRefresh(titleOrIndex) {
    //console.info('tabMRefresh:' + titleOrIndex);
    center_refreshTab(titleOrIndex);
}
//关闭
function center_tabMClose(titleOrIndex) {
    //console.info('tabMClose:' + titleOrIndex);
    center_Tabs.tabs('close', titleOrIndex);
}
//关闭其他
function center_tabMCloseOthers(tLength, index) {
    //console.info('tabMCloseAll:' + tLength);
    var ti = 0; //要删除tab页的索引
    for (var i = 0; i < tLength; i++) {
        if (i == index) {
            ti = 1;
        }
        center_tabMClose(ti); //关闭其他所有
    }
}
//关闭全部
function center_tabMCloseAll(tLength) {
    //console.info('tabMCloseAll:' + tLength);
    for (var i = 0; i < tLength; i++) {
        center_tabMClose(0); //关闭所有 首页tab也关闭
    }
}
/* center */
//刷新tab页
function center_refreshTab(title) {
    var tab = center_Tabs.tabs('getTab', title);
    if (tab != null) {
        center_Tabs.tabs('update', {
            tab: tab,
            options: tab.panel('options')
        });
    }
}

//新增tab页
function center_addTab(node) {
    //console.info('addTab');
    //console.info(node);
    //如果存在tab页 选中tab页
    if (center_Tabs.tabs('exists', node.text)) {
        center_Tabs.tabs('select', node.text);
    }
    else {
        //不存在的话 新增Tab页
        //加载Tab页 
        center_TabLoad(node);
    }
}
//tab页加载
function center_TabLoad(node) {
    /*//进度条
    $.messager.progress({
    title: 'JulieCT提示：',
    text: '页面加载中...',
    interval: 60
    });
    //600ms之后 进度条关闭
    setTimeout(function () {
    $.messager.progress('close');
    }, 600);*/
    //新增tab页
    center_Tabs.tabs('add', {
        title: node.text,
        closable: true,
        //content: '<iframe src="' + node.url + '" frameborder="0" style="border:0;width:100%;height:99.4%;"></iframe>',
        tools: [{
            iconCls: 'icon-mini-refresh', //刷新按钮
            handler: function () {
                center_refreshTab(node.text);
            }
        }]
    });
}

function center_addTab1(subtitle, url, icon) {
    if (!center_Tabs.tabs('exists', subtitle)) {
        center_Tabs.tabs('add', {
            title: subtitle,
            content: '<iframe src="' + url + '" frameborder="0" style="border:0;width:100%;height:99.4%;"></iframe>',
            closable: true,
            tools: [{
                iconCls: 'icon-mini-refresh', //刷新按钮
                handler: function () {
                    center_refreshTab(subtitle);
                }
            }],
            icon: icon
        });
    } else {
        center_Tabs.tabs('select', subtitle);
    }
}

function center_addTabContent(subtitle, content, icon) {
    if (!center_Tabs.tabs('exists', subtitle)) {
        center_Tabs.tabs('add', {
            title: subtitle,
            content: content,
            closable: true,
            tools: [{
                iconCls: 'icon-mini-refresh', //刷新按钮
                handler: function () {
                    center_refreshTab(subtitle);
                }
            }],
            icon: icon
        });
    } else {
        center_Tabs.tabs('select', subtitle);
    }
}
