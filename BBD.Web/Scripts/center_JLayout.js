var center_Tabs;
var center_ctabsMenu;
var nodes = [];
$(function () {
    var obj1 = new Object();
    obj1.url = "/Customer/Index";
    obj1.text = "会员信息";
    var obj2 = new Object();
    obj2.url = "/BuyRec/Index";
    obj2.text = "购买记录";
    var obj3 = new Object();
    obj3.url = "/ConsumeLog/Index";
    obj3.text = "消费记录";
    var obj4 = new Object();
    obj4.url = "/DislikeFood/Index";
    obj4.text = "客户不喜食物";
    nodes.push(obj1);
    nodes.push(obj2);
    nodes.push(obj3);
    nodes.push(obj4);
    center_ctabsMenu = $('#center_ctabsMenu').menu({
        onClick: function (item) {
            console.info($(this));
            console.info(item);
            var curTabTitle = $(this).data('tabTitle');//当前tab页标题
            if (item.text == "刷新Tab页") {
                center_tabMRefresh(curTabTitle);
            }
            else
                if (item.text == "关闭Tab页") {
                    center_tabMClose(curTabTitle);
                }
                else
                    if (item.text == "关闭其他Tab页") {
                        var tlen = center_Tabs.tabs('tabs').length;
                        var tab = center_Tabs.tabs('getTab', curTabTitle); //当前点击tab
                        var index = center_Tabs.tabs('getTabIndex', tab); //获取指定tab的索引
                        console.info(index);
                        center_tabMCloseOthers(tlen, index);
                    }
                    else
                        if (item.text == "关闭所有Tab页") {
                            var tlen = center_Tabs.tabs('tabs').length;
                            center_tabMCloseAll(tlen);
                        }
        }
    });
    center_Tabs = $('#ctabs_center').tabs({
        fit: true,
        border: false,
        onContextMenu: function (e, title) {
            //右键操作
            e.preventDefault();
            center_ctabsMenu.menu('show', {
                left: e.pageX,
                top: e.pageY
            }).data('tabTitle', title);
        }
    });
    for (var i = 0; i < nodes.length; i++) {
        center_addTab(nodes[i]);
    }
    var tabs = center_Tabs.tabs().tabs('tabs');
    for (var i = 0; i < tabs.length; i++) {
        tabs[i].panel('options').tab.unbind().bind('click', { index: i }, function (e) {
            center_Tabs.tabs('select', nodes[e.data.index].text);
            var selTab = center_Tabs.tabs('getSelected');
            if (center_Tabs.tabs('exists', nodes[e.data.index].text)) {
                center_Tabs.tabs('update', {
                    tab: selTab,
                    title: nodes[e.data.index].url.text,
                    options: {
                        content: createFrame(nodes[e.data.index].url)
                    }
                })
            }
        });
    }
    //默认选中
    center_Tabs.tabs('select', '会员信息');
    var selTab = center_Tabs.tabs('getSelected');
    center_Tabs.tabs('update', {
        tab: selTab,
        title: nodes[0].url.text,
        closable: true,
        options: {
            content: createFrame(nodes[0].url)
        }
    })
})

function createFrame(url) {
    var param = "?uid={0}";
    if ($("#g_uid")) param = param.replace("{0}", $("#g_uid").val());
    else  param.replace("{0}","0");
    return '<iframe  src="' + url + param + '" frameborder="0" style="border:0;width:100%;height:99.4%;overflow:scroll;"></iframe>';
}