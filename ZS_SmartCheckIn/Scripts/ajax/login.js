$(document).ready(function () {
    $("#btnLogin").click(function () {
        var Username = $("#Username").val();
        var Password = $("#Password").val();
        var Branch_ID = $("#branch").val();      
        if (Branch_ID == null)
            Branch_ID = 0;
        $.ajax({
            type: "POST",
            url: "/User/CreateLogin",
            data: "{'Username':'" + Username + "','Password':'" + Password + "','Branch_ID':'" + Branch_ID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "text",
            //beforeSend: function () { $("#loader").css("display", "block"); },
            //complete: function () { $("#loader").css("display", "none"); },
            success: OnSuccessCall,
            error: OnErrorCall
        });
        function OnSuccessCall(data) {
            if (data == "1") {              
                location.href = "/Admin/Dashboard";
            } else if (data == "-1") {
                alert("Please Check Your Username and Password");
            } else if (data == "0") {
                alert("Please Fill");
            }
        }
        function OnErrorCall() {
            alert("error");
        }
    });

    $("#btnGuestLogin").click(function () {
        var Username = $("#Username").val();
        var Password = $("#Password").val();

        $.ajax({
            type: "POST",
            url: "/User/CreateGuestLogin",
            data: "{'Username':'" + Username + "','Password':'" + Password + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "text",
            //beforeSend: function () { $("#loader").css("display", "block"); },
            //complete: function () { $("#loader").css("display", "none"); },
            success: OnSuccessCall,
            error: OnErrorCall
        });
        function OnSuccessCall(data) {
            if (data == "1") {
                location.href = "/Home/Dashboard";
            } else if (data == "-1") {
                alert("Please Check Your Username and Password");
            } else if (data == "0") {
                alert("Please Fill");
            }
        }
        function OnErrorCall() {
            alert("error");
        }
    });

    $(".btnLogout").click(function () {
        $.ajax({
            type: "POST",
            url: "/User/CreateLogout",
            data: "",
            contentType: "application/json; charset=utf-8",
            dataType: "text",
            //beforeSend: function () { $("#loader").css("display", "block"); },
            //complete: function () { $("#loader").css("display", "none"); },
            success: OnSuccessLogoutCall,
            error: OnErrorLogoutCall
        });
        function OnSuccessLogoutCall(data) {
            if (data == "1") {
                location.href = "/User/Login";
            } else {
                alert("Try Agine");
            }
        }
        function OnErrorLogoutCall() {
            alert("error");
        }
    });

    $(".btnGuestLogout").click(function () {
        $.ajax({
            type: "POST",
            url: "/User/CreateLogout",
            data: "",
            contentType: "application/json; charset=utf-8",
            dataType: "text",
            //beforeSend: function () { $("#loader").css("display", "block"); },
            //complete: function () { $("#loader").css("display", "none"); },
            success: OnSuccessLogoutCall,
            error: OnErrorLogoutCall
        });
        function OnSuccessLogoutCall(data) {
            if (data == "1") {
                location.href = "/User/GuestLogin";
            } else if (data == "-1") {
                alert("Please Check Your Username and Password");
            } else if (data == "0") {
                alert("Please Fill");
            }
        }
        function OnErrorLogoutCall() {
            alert("error");
        }
    });

    $('#btnForgotSubmit').click(function () {
        var username = $("#forgotusername").val();
        if (username != "" && username != null) {
            $.ajax({
                type: "POST",
                url: "/User/ForgotGuestPassword",
                data: "{'username':'" + username + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "text",
                beforeSend: function () { $('#loader').css("display", "block") },
                complete: function () { $('#loader').css("display", "none") },
                success: OnSuccessCall,
                error: OnErrorCall
            });
            function OnSuccessCall(data) {
                if ($.trim(data)) {
                    alert("Email sent to '" + data + "'..Please check your email and reset password");
                }
                else if (data == "") {
                    alert("Password reset failed");
                }
            }
            function OnErrorCall() {
                alert("Password Reset failed");
            }
        }
        else {
            alert("Enter Username");
        }
    });

    function Valid() {
        var result = true;
        msg = "";
        var Password = $("#password").val();
        var Con_Password = $("#conpassword").val();

        if (Password == "" || Password == null) {
            document.getElementById("password").focus();
            msg = "Enter Password";
        }
        else if (Con_Password == "" || Con_Password == null) {
            document.getElementById("conpassword").focus();
            msg = "Enter Confirm Password";
        }
        else if (Con_Password != Password) {
            document.getElementById("conpassword").focus();
            msg = "Password and Confirm Password does not match";
        }

        if (msg != "") {
            result = false;
        }
        return result;
    }

    $('#btnResetCustPasword').click(function () {
        if (Valid() == true) {
            var token = $("#token").val();
            var password = $("#password").val();
            var conpassword = $("#conpassword").val();

            $.ajax({
                type: "POST",
                url: "/Home/UpdatePassword",
                data: "{'Username':'','Password':'" + password + "','Token':'" + token + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () { $('#loader').css("display", "block") },
                complete: function () { $('#loader').css("display", "none") },
                success: OnSuccessSaveCall,
                error: OnErrorSaveCall
            });
            function OnSuccessSaveCall(data) {
                if (data > 0) {
                    alert("Your password has been reset.Please Login");
                    location.href = "/User/GuestLogin";
                }
                else {
                    alert("Password Updation Failed");
                }
            }
            function OnErrorSaveCall() {
                alert("Password Updation Failed");
            }
        }
        else {
            alert(msg);
        }
    });

});