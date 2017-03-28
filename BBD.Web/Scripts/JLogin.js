var cc; //main Layout
var lbhelp; //top linkbutton help
var lblogout; //top linkbutton logout
var lbhome; //top linkbutton home
var lbadmin; //top linkbutton admin
var lDialog; //login dialog
var lForm; //login form

$(function () {
    if ($.cookie("HHYS_COOKIE_LOGIN") != null && $.cookie("HHYS_COOKIE_LOGIN").length>0) {
        $("#loginForm input[textboxname='uname']").textbox("setValue", $.cookie("HHYS_COOKIE_LOGIN"));
    }
    $(".submit_btn").click(function () {
        bsub();
    })

    function bsub() {
        $('#loginForm').form('submit', {
            onSubmit: function () {
                var isValid = $(this).form('enableValidation').form('validate');
                if (isValid) {
                    //登录
                    var name = $("#loginForm input[textboxname='uname']").textbox("getValue");
                    var pass = $("#loginForm input[textboxname='upass']").textbox("getValue");
                    $.ajax({
                        type: "post",
                        url: "/Login/Login",
                        data: { username: name, password: pass },
                        datatype: "json",
                        success: function (data) {
                            if (data.ifOk) {
                                window.parent.location.href = "/Home/Main";
                            } else {
                                $.messager.alert('登录提示', data.errorMsg);
                            }
                        }
                    });
                }
            }
        });
    }
    //login form
    lForm = $('#loginForm').form();

    /* 增加回车提交功能 */
    lForm.find('input').on('keyup', function (event) {
        $(this).next(":hidden").val($(this).val());
        if (event.keyCode == '13') {
            bsub();
        }
    });


    function validateCode() {
        var inputCode = document.getElementById("inpCode").value.toUpperCase();
        var codeToUp = code.toUpperCase();

        if (inputCode.length <= 0) {
            $.messager.alert("提示", "请输入验证码！");
            return false;
        }
        else if (inputCode != codeToUp) {
            $.messager.alert("提示", "验证码输入错误！");
            createCode();
            $("#inpCode").val('').focus();
            return false;
        } else {
            return true;
        }
    }

    //login dialog
    //lDialog = $('#loginDialog').dialog({
    //    closable: false,
    //    modal: true, //主面板禁止操作
    //    buttons: [{
    //        text: ' 登 录 ',
    //        handler: function () {
    //            bsub();
    //        }
    //    }]
    //});
});

