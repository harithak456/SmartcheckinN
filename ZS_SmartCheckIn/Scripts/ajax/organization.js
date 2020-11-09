$(document).ready(function () {

    function Validate() {
        var flag = true;
        $(".requiredv").html("");
        var Name = $('#OrgName').val();
        var Address = $('#Address').val();
        var Phone = $('#Phone').val();
        var Email = $("#Email").val();         

        if (Name == null || Name.trim() == "") {
            $("#validName").html("This field is required.");
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
        return flag;
    }

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


    $('.btnAddOrganization').click(function () {
        if (Validate() == true) {           
            var OrgID = $('#OrgID').val();
            var Name = $('#OrgName').val();
            var Address = $('#Address').val();
            var ContactPerson = $('#ContactPerson').val();
            var Phone = $('#Phone').val();
            var City = $('#City').val();
            var State = $('#State').val();
            var Country = $('#Country').val();
            var Zip = $('#Zip').val();
            var Email = $('#Email').val();
            var Website = $('#Website').val();

            var data = new FormData();
            data.append("Organization_ID", OrgID);
            data.append("Organization_Name", Name);
            data.append("Organization_Address", Address);
            data.append("Organization_ContactPerson", ContactPerson);
            data.append("Organization_City", City);
            data.append("Organization_Phone", Phone);
            data.append("Organization_State", State);
            data.append("Organization_Country", Country);
            data.append("Organization_PinCode", Zip);
            data.append("organization_email", Email);
            data.append("organization_web", Website);

            $.ajax({
                type: "POST",
                url: "/Master/SaveOrganization",
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
                            location.href = "/Admin/Dashboard";
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

});