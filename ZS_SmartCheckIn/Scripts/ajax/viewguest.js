$(document).ready(function () {     
     
    $('.btnConfirmCheckIn').click(function () {
        var CustomerCode = document.getElementById('CustomerCode').innerText;
        var ConfirmationCode = document.getElementById('ConfirmationCode').value;
        var confirmcode = document.getElementById('confirmcode').value;
        if (confirmcode == "") {
            alert("Enter Confirmation Code");
                  
        }
        if (ConfirmationCode == confirmcode) {
            $.ajax({
                type: "POST",
                url: "/Guest/UpdateCheckIn",
                data: "{'Customer_Code':'" + CustomerCode + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () { $("#loader").css("display", "block"); },
                complete: function () { $("#loader").css("display", "none"); },
                success: OnSuccessSaveCall,
                error: OnErrorSaveCall
            });
            function OnSuccessSaveCall(data) {
                if (data > "0") {

                    $(window).scrollTop(0);
                    $(".messagebox").append('<div class="well bg-success msg"><strong> Success!</strong> Check-In Successful.</div>');
                    $(".msg").delay(8000).fadeOut(800);
                    setTimeout(
                        function () {
                            location.href = "/Guest/GuestList?flag=3";
                        }, 800);                                
                }
                else {
                    $(window).scrollTop(0);
                    $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Failed To Check-In.</div>');
                    $(".msg").delay(4000).fadeOut(800);   
                }
            }
            function OnErrorSaveCall() {
                $(window).scrollTop(0);
                $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Failed To Check-In.</div>');
                $(".msg").delay(4000).fadeOut(800);  
            }
        }
        else {
            alert("Invalid Confirmation Code");                     
        }
    });

    $('.btnVerifyData').click(function () {
        var CustomerCode = document.getElementById('CustomerCode').innerText;      
        $.ajax({
            type: "POST",
            url: "/Guest/ConfirmGuestDetail",
            data: "{'Customer_Code':'" + CustomerCode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () { $("#loader").css("display", "block"); },
            complete: function () { $("#loader").css("display", "none"); },
            success: OnSuccessSaveCall,
            error: OnErrorSaveCall
        });
        function OnSuccessSaveCall(data) {
            if (data > "0") {
                $(window).scrollTop(0);
                $(".messagebox").append('<div class="well bg-success msg"><strong> Success!</strong> Id Verification Successful.</div>');
                $(".msg").delay(8000).fadeOut(800);

                setTimeout(
                    function () {
                        location.href = "/Guest/GuestList?flag=2";
                    }, 800);                 
            }
            else {
                $(window).scrollTop(0);
                $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Id Verification Failed.</div>');
                $(".msg").delay(4000).fadeOut(800);  
            }
        }
        function OnErrorSaveCall() {
            $(window).scrollTop(0);
            $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Id Verification Failed.</div>');
            $(".msg").delay(4000).fadeOut(800);  
        }
    });

  

    $('.btnDeleteguest').click(function () {        
        var rowCount = $('#SampleD tr').length;   
        if (rowCount > 2) {
            var row = $(this).closest('tr');
            var Guest_Code = $(row).find('td:eq(2)').find('input').val();
            if (confirm("Are You Sure You Want To Delete?")) {
                $.ajax({
                    type: "POST",
                    url: "/GuestData/DeleteGuestData",
                    data: "{'Guest_Code':'" + Guest_Code + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () { $("#loader").css("display", "block"); },
                    complete: function () { $("#loader").css("display", "none"); },
                    success: OnSuccessSaveCall,
                    error: OnErrorSaveCall
                });
                function OnSuccessSaveCall(data) {
                    if (data > 0) {
                        row.remove();
                        $(window).scrollTop(0);
                        $(".messagebox").append('<div class="well bg-success msg"><strong> Success!</strong> Delete Successful.</div>');
                        $(".msg").delay(4000).fadeOut(800);
                    }
                    else {
                        $(window).scrollTop(0);
                        $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Failed To Delete.</div>');
                        $(".msg").delay(4000).fadeOut(800);
                    }
                }
                function OnErrorSaveCall() {
                    $(window).scrollTop(0);
                    $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Failed To Delete.</div>');
                    $(".msg").delay(4000).fadeOut(800);
                }
            }
        }
        else {
            alert(" Cannot Delete This Guest.");
            
        }
    });     
    

});