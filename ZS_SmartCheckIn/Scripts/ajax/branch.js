$(document).ready(function () {
    function ValidateEmail(inputText) {
        var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        if (!inputText.match(mailformat)) {
            return (true)
        }
        else {
            return (false)
        }
    }

    function phonenumber(inputtxt) {

        var phoneno = /^\d{10}$/;
        if (!inputtxt.match(phoneno)) {
            return (true)
        }
        else {
            return false;
        }
    }

    function Validate() {
        var flag = true;
        $(".requiredv").html("");
        var Name = $('#Name').val();
        var Code = $('#Code').val();
        var Address = $('#Address').val();
        var ContactPerson = $('#ContactPerson').val();
        var Phone = $('#Phone').val();           
   
        var Email = $('#Email').val();

        if (Name == null || Name.trim() == "") {
            $("#validName").html("This field is required.");
            flag = false;
        }
        if (Code == null || Code.trim() == "") {
            $("#validCode").html("This field is required.");
            flag = false;
        }
        if (Address == null || Address.trim() == "") {
            $("#validAddress").html("This field is required.");
            flag = false;
        }
        if (ValidateEmail(Email)) {
            $("#validMail").html("Enter a Valid Email Id.");
            flag = false;
        }
        if (phonenumber(Phone)) {
            $("#validMobile").html("Enter a Valid Mobile No.");
            flag = false;
        }
        if (ContactPerson == null || ContactPerson.trim() == "") {
            $("#validPerson").html("This field is required.");
            flag = false;
        }
        return flag;
    }

    $('.btnAddBranch').click(function () {
        if (Validate() == true) {
            var BranchID = $('#BranchID').val();
            var OrgID = $('#OrgID').val();
            var Name = $('#Name').val();
            var Code = $('#Code').val();
            var Address = $('#Address').val();
            var ContactPerson = $('#ContactPerson').val();
            var Phone = $('#Phone').val();
            var City = $('#City').val();
            var State = $('#State').val();
            var Country = $('#Country').val();
            var Zip = $('#Zip').val();
            var Email = $('#Email').val();

            var data = new FormData();
            data.append("Organization_ID", OrgID);
            data.append("Branch_ID", BranchID);
            data.append("Branch_Name", Name);
            data.append("Branch_Code", Code);
            data.append("Branch_Address", Address);
            data.append("Branch_ContactPerson", ContactPerson);
            data.append("Branch_City", City);
            data.append("Branch_Phone", Phone);
            data.append("Branch_State", State);
            data.append("Branch_Country", Country);
            data.append("Branch_PinCode", Zip);
            data.append("Branch_email", Email);

            $.ajax({
                type: "POST",
                url: "/Master/SaveBranch",
                data: data,
                contentType: false,
                processData: false,
                dataType: "json",
                beforeSend: function () { $('#loader').css("display", "block") },
                complete: function () { $('#loader').css("display", "none") },
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
                            location.href = "/Master/BranchList";
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

    $(".btnDeleteBranch").click(function () {
        if (confirm("Are You Sure You Want To Delete?")) {
            var row = $(this).closest('tr');
            var branchid = $(row).find('td:eq(5)').find('input').val();
            $.ajax({
                type: "POST",
                url: "/Master/DeleteBranch",
                data: "{'Branch_ID':'" + branchid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () { $("#loader").css("display", "block"); },
                complete: function () { $("#loader").css("display", "none"); },
                success: OnSuccessSaveCall,
                error: OnErrorSaveCall
            });
            function OnSuccessSaveCall(data) {
                if (data > 0) {
                    $("#" + branchid).fadeOut("slow").remove();
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
            function OnErrorSaveCall() {
                $(window).scrollTop(0);
                $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Failed To Delete.</div>');
                $(".msg").delay(4000).fadeOut(800);  
            }
        }
    });
});