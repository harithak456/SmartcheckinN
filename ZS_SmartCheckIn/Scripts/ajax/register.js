$(document).ready(function () {   
  
    function Validate() {
        var flag = true;

        $(".requiredv").html("");

        var FirstName = $("#FirstName").val();
        var LastName = $("#LastName").val();
        var Email = $("#Email").val();
        var Username = $("#UserName").val();
        var arrivaldate = $("#arrivaldate").val();        
        var MobileNo = $("#MobileNo").val();
        var portal = $("#portal").val();
        var arrivaltime = $("#arrivaltime").val();

        if (FirstName == null || FirstName.trim() == "" )
        {
            $("#validFName").html("This field is required.");         
            flag = false;
        }
        if (LastName == null || LastName.trim() == "") {
            $("#validLName").html("This field is required.");         
            flag = false;
        }       
        if (ValidateEmail(Email))
        {
            $("#validMail").html("Enter a Valid Email Id.");         
            flag = false;
        }
        if (Username == null || Username.trim() == "") {
            $("#validUser").html("This field is required.");
            flag = false;
        }    
        if (phonenumber(MobileNo))
        {          
            $("#validMobile").html("Enter a Valid Mobile No.");
            flag = false;
        } 

        if (portal == null || portal.trim() == "") {
            $("#validPortal").html("This field is required.");
            flag = false;
        }

        if (arrivaltime == null || arrivaltime.trim() == "") {
            $("#validArrival").html("This field is required.");
            flag = false;
        }   

        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1;

        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        var ToDate = yyyy + '-' + mm + '-' + dd;

        if (arrivaldate == null || arrivaldate.trim() == "") {
            $("#validArrival").html("This field is required.");
            flag = false;
        }      
        else if (arrivaldate < ToDate) {
            $("#validArrival").html("Select a Valid Date.");
            return false;
        }
        else if (arrivaldate == ToDate) {
            timeParts = document.getElementById("arrivaltime").value.split(':');
            if (today.getHours() >= timeParts[0]) {
                $("#validArrival").html("Select a Valid Time.");
                return false;
            }
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
        if (!inputtxt.match(phoneno))
        {          
            return (true)
        }
        else {           
            return false;
        }
    }

   
    $('.btnAddGuest').click(function () {
        if (Validate() == true) {
            
            var FirstName = $("#FirstName").val();
            var LastName = $("#LastName").val();
            var Email = $("#Email").val();
            var PhoneNo = $("#PhoneNo").val();
            var MobileNo = $("#MobileNo").val();
            var GuestAccompany = $("#GuestAccompany").val();
            var arrivaldate = $("#arrivaldate").val();
            var arrivaltime = $("#arrivaltime").val();
            var Username = $("#UserName").val();
            var portal = $("#portal").val();
            var customercode = $("#customercode").val();
            var sendsms = $("#sendsms").is(":checked");
            var sendmail = $("#sendmail").is(":checked");
            var frequentVisitor = $("#frequentVisitor").val();
            var password = $('#password').val();
        
            var output = arrivaldate.split("/").reverse().join("-");
            var data = new FormData();
            data.append("Customer_Code", customercode);
            data.append("Guest_Firstname", FirstName);
            data.append("Guest_Lastname", LastName);
            data.append("Guest_Email", Email);
            data.append("Guest_PhoneNo", PhoneNo);
            data.append("Guest_MobileNo", MobileNo);
            data.append("Guest_Accompany", GuestAccompany);
            data.append("Guest_Username", Username);
            data.append("Arrival_Date", output);
            data.append("Arrival_Time", arrivaltime);
            data.append("Booking_Portal", portal);
            data.append("sendmail", sendmail);
            data.append("sendsms", sendsms);         
            data.append("FrequentVisitor", frequentVisitor);  
            data.append("Guest_Password", password);  
          
            $.ajax({
                type: "POST",
                url: "/Guest/SaveNewGuest",
                data: data,
                contentType: false,
                processData: false,
                dataType: "json",
                beforeSend: function () { $("#loader").css("display", "block"); },
                complete: function () { $("#loader").css("display", "none"); },
                success: OnSuccessSaveCall,
                //error: function (ts) { alert(ts.responseText) }
                error: OnErrorSaveCall
            });
            function OnSuccessSaveCall(data) {               
                if (data != "")
                {
                    if (data == "-1")
                    {
                        $(window).scrollTop(0);
                        $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Username Already Exists.</div>');
                        $(".msg").delay(4000).fadeOut(800);                                             
                    }
                    else if (data == "-2") {
                        $(window).scrollTop(0);
                        $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Email Already Exists.</div>');
                        $(".msg").delay(4000).fadeOut(800);
                    }
                    else
                    {
                        var result = data.split(',');
                        if (result[0] > 0)
                        {                         
                            $(window).scrollTop(0);
                            if (sendmail==true)
                                $(".messagebox").append('<div class="well bg-success msg"><strong> Success!</strong> Guest Added Successfully! Email Sent Successfully .</div>');
                            else
                                $(".messagebox").append('<div class="well bg-success msg"><strong> Success!</strong> Guest Added Successfully!</div>');
                            $(".msg").delay(8000).fadeOut(800);
                            setTimeout(
                                function () {
                                    location.href = "/Guest/GuestList?flag=1";
                                }, 800);             
                            //document.getElementById('customercode').value = result[1];
                        }
                        else
                        {
                            $(window).scrollTop(0);
                            $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Failed To Save.</div>');
                            $(".msg").delay(4000).fadeOut(800);   
                        }
                    }
                }
                else
                {
                    $(window).scrollTop(0);
                    $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Failed To Save.</div>');
                    $(".msg").delay(4000).fadeOut(800);   
                }
            }
            function OnErrorSaveCall() {               
                $(window).scrollTop(0);
                $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Failed To Save..</div>');
                $(".msg").delay(4000).fadeOut(800);   
            }
        }
      
    });

    $('.btnSendMail').click(function () {
        var customercode = $('#customercode').val();      
        $.ajax({
            type: "POST",
            url: "/Guest/SendPasswordMail",
            data: "{'customercode':'"+customercode+"'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () { $("#loader").css("display", "block"); },
            complete: function () { $("#loader").css("display", "none"); },
            success: OnSuccessSaveCall,
            error: OnErrorSaveCall
        });
        function OnSuccessSaveCall(data) {           
            if (data == "1") {
                $(window).scrollTop(0);
                $(".messagebox").append('<div class="well bg-success msg"><strong> Success!</strong> Mail Sent Successfully.</div>');
                $(".msg").delay(8000).fadeOut(800);
                setTimeout(
                    function () {
                        location.href = "/Guest/GuestList?flag=1";
                    }, 800);
            }
            else {
                $(window).scrollTop(0);
                $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Mail Not Delivered..</div>');
                $(".msg").delay(4000).fadeOut(800);  }
        }
        function OnErrorSaveCall() {
            $(window).scrollTop(0);
            $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Mail Not Delivered..</div>');
            $(".msg").delay(4000).fadeOut(800);   
        }
    });

    $('.btnSendSms').click(function () {
        var customercode = $('#customercode').val();
        $.ajax({
            type: "POST",
            url: "/Guest/SendPasswordSMS",
            data: "{'customercode':'" + customercode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () { $("#loader").css("display", "block"); },
            complete: function () { $("#loader").css("display", "none"); },
            success: OnSuccessSaveCall,
            error: OnErrorSaveCall
        });
        function OnSuccessSaveCall(data) {
            if (data == "1") {
                $(window).scrollTop(0);
                $(".messagebox").append('<div class="well bg-success msg"><strong> Success!</strong> Message Sent Successfully.</div>');
                $(".msg").delay(8000).fadeOut(800);
                setTimeout(
                    function () {
                        location.href = "/Guest/GuestList?flag=1";
                    }, 800);
            }
            else {
                $(window).scrollTop(0);
                $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Message Not Delivered..</div>');
                $(".msg").delay(4000).fadeOut(800);
            }
        }
        function OnErrorSaveCall() {
            $(window).scrollTop(0);
            $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Message Not Delivered..</div>');
            $(".msg").delay(4000).fadeOut(800);
        }
    });
});