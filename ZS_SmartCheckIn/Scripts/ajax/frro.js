$(document).ready(function () {

    function Validate() {
        var flag = true;
        $(".requiredv").html("");
        var Username = $('#FRROUsername').val();
        var Password = $('#FRROPassword').val();

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

    $('.btnAddFRRO').click(function () {
        if (Validate() == true) {
            var Username = $('#FRROUsername').val();
            var Password = $('#FRROPassword').val();

            var data = new FormData();
            data.append("FRRO_Username", Username);
            data.append("FRRO_Password", Password);

            $.ajax({
                type: "POST",
                url: "/Master/SaveFRRO",
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
                    //setTimeout(
                    //    function () {
                    //        location.href = "/Master/UserList";
                    //    }, 800);
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

});