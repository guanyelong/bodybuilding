var cc; //main Layout
var lbhelp; //top linkbutton help
var lblogout; //top linkbutton logout
var lbhome; //top linkbutton home
var lbadmin; //top linkbutton admin
var lDialog; //login dialog
var lForm; //login form
var lbPwd;
$(function () {
    //link buttons
    lbhelp = $('#lbhelp').linkbutton({
        plain: true,
        iconCls: 'icon-jhelp',
        onClick: function () {
            console.info('help linkbutton');
        }
    });

    lblogout = $('#lblogout').linkbutton({
        plain: true,
        iconCls: 'icon-logout',
        onClick: function () {
            $.messager.confirm('温馨提示', '确定要退出系统吗?', function (r) {
                if (r) {
                    $.ajax({
                        url: '/Home/Logout',
                        type: "post",
                        cache: false,
                        dataType: 'json',
                        success: function (r) {
                            if (r) {
                                window.location = "/Login/Index";
                            }
                        }
                    });
                }
            });
        }
    });

    lbPwd = $("#lbpwd").linkbutton({
        plain: true,
        iconCls: 'icon-settings',
        onClick: function () {
            ChangePassword();
        }
    });

    lbhome = $('#lbhome').linkbutton({
        plain: true,
        iconCls: 'icon-home',
        onClick: function () {
            centerTabs.tabs('select', '首页');
        }
    });

    lbadmin = $('#lbadmin').linkbutton({
        plain: true,
        iconCls: 'icon-smile',
        onClick: function () {
            centerTabs.tabs('select', '首页');
        }
    });
    cc = $('#cc').layout();
})

function ChangePassword() {
    if ($("#hidUid").val() == null) {
        $.messager.alert("提示", "用户信息已过期，请重新登录！");
        return;
    }
    $("#pwdDialog").dialog({
        title: "修改密码",
        width: 400,
        height: 250,
        href: "/User/ChangePasswordDlg?Id=" + $("#hidUid").val(),
        closed: false,
        cache: false,
        modal: true,
        buttons: [{
            text: '保存',
            handler: function () {
                SavePassword();
            }
        }, {
            text: '取消',
            handler: function () {
                $("#pwdDialog").dialog("close");
            }
        }]
    });
}

//ajax 请求保存密码
function SavePassword() {
    var pwd = $("#changepwd").val();
    var pwd1 = $("#changepwd1").val();
    if (pwd != pwd1) {
        $.messager.alert("提示", "两次输入密码不相同！");
        return;
    }

    $("#changeForm").form("submit", {
        url: "/User/ChangePassword",
        success: function (data) {
            var returnData = $.parseJSON(data)
            //form 提交返回的是字符串，需要转换成json
            if (returnData.result == "error") {
                $.messager.alert("提示", "修改密码失败 " + returnData.message);
                return;
            }
            //success
            $.messager.alert("提示", "用户密码修改成功");
            $("#pwdDialog").dialog("close");
        }
    });
}



var loadTree = function (text,url) {
    var node = new Object();
    node.text = text;
    node.attributes = new Object();
    node.attributes.url = url;
    addTab(node);
    //首页默认选中
    if (menuId == "01") {
        centerTabs.tabs('select', '首页');
    }

   
    
    $('#ulTree').tree({
        url: '/Home/LoadUserTree?id=' + menuId,
        lines: true,
        onClick: function (node) {
            if (node) {
                var isLeaf = $(this).tree('isLeaf', node.target);
                if (isLeaf) {
                    addTab(node);
                }
            }
        },
        onDblClick: function (node) {
            var isLeaf = $(this).tree('isLeaf', node.target);
            if (node) {
                if (!isLeaf) {
                    if (node.state == 'closed') {
                        $(this).tree('expand', node.target);
                    } else {
                        $(this).tree('collapse', node.target);
                    }
                }
            }
        }
    });
};