$(document).ready(function () {

    function Validate() {
        var flag = true;
        $(".requiredv").html("");    
        var User_Name = $('#Name').val();
        var Username = $('#UserUsername').val();
        var Password = $('#UserPassword').val();   
      
        if (User_Name == null || User_Name.trim() == "") {
            $("#validName").html("This field is required.");
            flag = false;
        }
        if (Username == null || Username.trim() == "") {
            $("#validUser").html("This field is required.");
            flag = false;
        }
        if (Password == null || Password.trim() == "") {
            $("#validPassword").html("This field is required.");
            flag = false;
        }      
        return flag;
    }

    $('.btnAddUser').click(function () {
        if (Validate() == true) {
            var User_ID = $('#User_ID').val();
            var Branch_ID = $('#branch').val();
            var User_Name = $('#Name').val();
            var Username = $('#UserUsername').val();
            var Password = $('#UserPassword').val();
            var UserType = $('#UserType').val();

            var data = new FormData();
            data.append("User_ID", User_ID);
            data.append("Branch_ID", Branch_ID);
            data.append("User_Type", UserType);
            data.append("User_Name", User_Name);
            data.append("User_Username", Username);
            data.append("User_Password", Password);

            $.ajax({
                type: "POST",
                url: "/Master/SaveUser",
                data: data,
                contentType: false,
                processData: false,
                dataType: "json",
                beforeSend: function () { $("#loader").css("display", "block"); },
                complete: function () { $("#loader").css("display", "none"); },
                success: OnSuccessSaveCall,
                error: OnErrorSaveCall
            });

            function OnSuccessSaveCall(data) {
                if (data > "0") {
                    $(window).scrollTop(0);
                    $(".messagebox").append('<div class="well bg-success msg"><strong> Success!</strong> Save Successful.</div>');
                    $(".msg").delay(8000).fadeOut(800);
                    setTimeout(
                        function () {
                            location.href = "/Master/UserList";
                        }, 800);                        
                }
                else {
                    $(window).scrollTop(0);
                    $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Failed To Save.</div>');
                    $(".msg").delay(4000).fadeOut(800);  
                }
            }
            function OnErrorSaveCall() {
                $(window).scrollTop(0);
                $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Failed To Save.</div>');
                $(".msg").delay(4000).fadeOut(800);
            }
        }
    });

    $(".btnDeleteUser").click(function () {
        if (confirm("Are You Sure You Want To Delete?")) {
            var row = $(this).closest('tr');
            var userid = $(row).find('td:eq(4)').find('input').val();
            $.ajax({
                type: "POST",
                url: "/Master/DeleteUser",
                data: "{'User_ID':'" + userid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () { $("#loader").css("display", "block"); },
                complete: function () { $("#loader").css("display", "none"); },
                success: OnSuccessSaveCall,
                error: OnErrorSaveCall
            });
            function OnSuccessSaveCall(data) {
                if (data > 0) {
                    $("#" + userid).fadeOut("slow").remove();
                    $(window).scrollTop(0);
                    $(".messagebox").append('<div class="well bg-success msg"><strong> Success!</strong> Delete Successful.</div>');
                    $(".msg").delay(4000).fadeOut(800);                      
                }
                else
                {
                    $(window).scrollTop(0);
                    $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Failed To Delete.</div>');
                    $(".msg").delay(4000).fadeOut(800);
                }
            }
            function OnErrorSaveCall()
            {
                $(window).scrollTop(0);
                $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Failed To Delete.</div>');
                $(".msg").delay(4000).fadeOut(800);
            }
        }
    });
});