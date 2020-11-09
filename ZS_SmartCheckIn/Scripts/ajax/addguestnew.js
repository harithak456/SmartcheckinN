$(document).ready(function () {


    GetGuestsLIst();

    function GetGuestsLIst() {
        var CustomerCode = $("#CustomerCode").val();
        $.ajax({
            type: "POST",
            url: "/GuestData/GetGuestsList",
            data: "{'CustomerID':'" + CustomerCode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //$('#tableBody').empty();
                var txt = "";
                var i = 0;
                if (Object.keys(data).length > 0) {
                    $.each(data, function (index, dbval) {
                        $('#listGallery').empty();

                        txt = "<li id='" + dbval.Guest_Document + "'>" +
                            "<img alt='' src='../../CardImages/" + dbval.Guest_Document + "' style='width:120px; height:90px;'> " +
                            "<div class='tags'><span class='label-holder'><span class='label label-inverse arrowed'>Passport</span>" +
                            "</span><span class='label-holder'><span class='label label-danger arrowed-in'>" + dbval.Guest_Firstname + "</span>" +
                            " </span></div></li>";
                        $('#listGallery').append(txt);

                        if (dbval.Guest_DocumentVisa != "" && dbval.Guest_DocumentVisa != null) {
                            txt = "<li id='" + dbval.Guest_DocumentVisa + "' >" +
                                "<img alt='' src='../../CardImages/" + dbval.Guest_DocumentVisa + "' style='width:120px; height:90px;'> " +
                                "<div class='tags'><span class='label-holder'><span class='label label-inverse arrowed'>Visa</span>" +
                                "</span><span class='label-holder'><span class='label label-danger arrowed-in'>" + dbval.Guest_Firstname + "</span>" +
                                " </span></div></li>";
                            $('#listGallery').append(txt);
                        }
                        i++;

                        if (i = Object.keys(data).length) {
                            document.getElementById('idcard').src = "../../CardImages/" + dbval.Guest_Document;
                            document.getElementById('GuestCode').value = dbval.Guest_Code;
                            document.getElementById('GuestDataID').value = dbval.Guestdata_id;
                        }

                        //$("#tableBody").append("<tr id=" + dbval.Guest_Code + ">" +
                        //    "<td> " + dbval.Guest_Firstname + "</td > <td><span class='label label-paid arrowed-in-right arrowed-in'>Uploaded</span></td> <td class='col-small center'>" +
                        //    " <button type='button' title='Edit Details' class='btn btn-warning btnEditGuest'  data-toggle='modal' data-target='#add-Guest'><i class='fa fa-edit icon-only'></i></button>" +
                        //    "</td></tr>");

                    });
                    document.getElementById("addedcount").value = i;

                    var Guest_Accompany = $('#Guest_Accompany').val();
                    while (i < Guest_Accompany) {                       
                        i++;

                        //$("#tableBody").append("<tr>" +
                        //    "<td>Guest " + i + "</td> <td><span class='label label-pending arrowed-in-right arrowed-in'>Not Uploaded</span></td> <td class='col-small center'>" +
                        //    "<a href='#' class=' btn btn-primary btnAddGuest' title='Add ID Document ' data-toggle='modal' data-target='#add-Guest'><i class='fa fa-upload icon-on-right'></i></a>" +
                        //    "</td></tr>");
                    }
                }
                else {
                    var i = 0;
                    var Guest_Accompany = $('#Guest_Accompany').val();
                    while (i <= Guest_Accompany) {
                        txt = "<li>" +
                            "<img alt='' src='../../assets/images/user-1.jpg' style='width:120px; height:90px;'> " +
                            "<div class='tags'><span class='label-holder'><span class='label label-inverse arrowed-in'>" +
                            "name</span></span></div></li>";
                        $('#listGallery').append(txt);
                        i++;
                        //$("#tableBody").append("<tr><td style='width:40px;'><img src='../../assets/images/user-1.jpg'  style='width:38px;object-fit:cover; border: 5px solid #555;'></td>" +
                        //    "<td>Guest " + i + "</td> <td><span class='label label-pending arrowed-in-right arrowed-in'>Not Uploaded</span></td> <td class='col-small center'>" +
                        //    "<a href='#' class=' btn btn-primary btnAddGuest' title='Add ID Document ' data-toggle='modal' data-target='#add-Guest'><i class='fa fa-upload icon-on-right'></i></a>" +
                        //    "</td></tr>");
                    }
                }

                var addedcount = $('#addedcount').val();
                if (addedcount == '' || addedcount == null) {
                    document.getElementById('id-btn-dialog1').style.display = 'none';
                }
                else {
                    document.getElementById('id-btn-dialog1').style.display = 'block';
                }
            },
            error: function () { }
        });
    }

    //function GetGuestsLIst() {
    //    var CustomerCode = $("#CustomerCode").val();
    //    $.ajax({
    //        type: "POST",
    //        url: "/GuestData/GetGuestsList",
    //        data: "{'CustomerID':'" + CustomerCode + "'}",
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (data) {
    //            $('#listGallery').empty();
    //            var txt = "";
    //            var i = 0;
    //            if (Object.keys(data).length > 0) {
    //                $.each(data, function (index, dbval) {
    //                    txt = "<li id='" + dbval.Guest_Code + "'>" +
    //                        "<img alt='' src='../../CardImages/" + dbval.Guest_Document + "' style='width:120px; height:90px;'> " +
    //                        "<div class='tags'><span class='label-holder'><span class='label label-inverse arrowed-in'>" +
    //                        "" + dbval.Guest_Firstname + "</span></span></div><div class='tools'> <a href='#'><i class='fa fa-paperclip'></i>" +
    //                        "</a><a href='#'><i class='fa fa-pencil'></i></a><a href='#'> " +
    //                        "<i class='fa fa-times text-danger'></i></a></div></li>";
    //                    $('#listGallery').append(txt);
    //                    i++;
    //                });
    //                document.getElementById("addedcount").value = i;

    //                var Guest_Accompany = $('#Guest_Accompany').val();
    //                while (i <= Guest_Accompany) {
    //                    txt = "<li>" +
    //                        "<img alt='' src='../../assets/images/user-1.jpg' style='width:120px; height:90px;'> " +
    //                        "<div class='tags'><span class='label-holder'><span class='label label-inverse arrowed-in'>" +
    //                        "name</span></span></div></li>";
    //                    $('#listGallery').append(txt);
    //                    i++;
    //                }
    //            }
    //            else {
    //                var i = 0;
    //                var Guest_Accompany = $('#Guest_Accompany').val();
    //                while (i <= Guest_Accompany) {
    //                    txt = "<li>" +
    //                        "<img alt='' src='../../assets/images/user-1.jpg' style='width:120px; height:90px;'> " +
    //                        "<div class='tags'><span class='label-holder'><span class='label label-inverse arrowed-in'>" +
    //                        "name</span></span></div></li>";
    //                    $('#listGallery').append(txt);
    //                    i++;
    //                }
    //            }

    //            var addedcount = $('#addedcount').val();
    //            if (addedcount == '' || addedcount == null) {
    //                document.getElementById('id-btn-dialog1').style.display = 'none';
    //            }
    //            else {
    //                document.getElementById('id-btn-dialog1').style.display = 'block';
    //            }

    //        },
    //        error: function () { }
    //    });
    //}

    //function ClearControls() {
    //    document.getElementById('FirstName').value = "";
    //    document.getElementById('GuestCode').value = "";
    //    document.getElementById('GuestDataID').value = "";
    //    document.getElementById('LastName').value = "";
    //    //document.getElementById('Father').value = "";
    //    document.getElementById('DOB').value = "";
    //    document.getElementById('Country').value = "IND";
    //    document.getElementById('Nationality').value = "IND";
    //    $('li.docTab').nextAll('li').hide();
    //    $('.docTab').addClass('active');
    //    $('.visaTab').removeClass('active');
    //    $('#tab6').addClass('active');
    //    $('#tab7').removeClass('active');       
    //    document.getElementById('Purpose').value = "16";
    //    document.getElementById('State').value = "";
    //    //document.getElementById('City').value = "";
    //    document.getElementById('Address').innerHTML = "";
    // //   document.getElementById('PhoneNo').value = "";
    //  //  document.getElementById('Email').value = "";
    //    document.getElementById('Gender').value = "Male";
    //    document.getElementById('frontInput').value = "";
    //    document.getElementById('backInput').value = "";
    //    document.getElementById('visaInput').value = "";
    //    document.getElementById('CountryofIssue').value = "";
    //    document.getElementById('DateOfIssue').value = "";
    //    document.getElementById('ExpiryDate').value = "";
    //    document.getElementById('DocumentNo').value = "";
    //    document.getElementById('idcard').src = "../../assets/images/user-1.jpg";
    //    document.getElementById('idcard1').src = "../../assets/images/user-1.jpg";
    //    document.getElementById('idcard2').src = "../../assets/images/user-1.jpg";    
    //    document.getElementById('imgFront').src = "../../assets/images/user-1.jpg";
    //    document.getElementById('imgBack').src = "../../assets/images/user-1.jpg";
    //    document.getElementById('imgVisa').src = "../../assets/images/user-1.jpg";
    //    document.getElementById('frontImgName').value = "";
    //    document.getElementById('backImgName').value = "";
    //    document.getElementById('visaImgName').value = "";

    //    document.getElementById('VisaNo').value = "";
    //    document.getElementById('VisaCity').value = "";
    //    document.getElementById('VisaCountry').value = "";
    //    document.getElementById('VisaType').value = "";
    //    document.getElementById('ValidTill').value = "";
    //    document.getElementById('IssueDate').value = "";

    //}

   
    function ClearControls() {
        $(".requiredv").html("");
        document.getElementById('CardType').value = "";
        document.getElementById("iddetails").style.display = "none";      
        document.getElementById('GuestCode').value = "";
        document.getElementById('GuestDataID').value = "";
        document.getElementById('FirstName').value = "";
        document.getElementById('LastName').value = "";
        //document.getElementById('Father').value = "";
        document.getElementById('DOB').value = "";
        document.getElementById('Country').value = "IND";
        document.getElementById('Purpose').value = "16";
        document.getElementById('State').value = "";
        //$('li.docTab').nextAll('li').hide();
        $('li.docTab').nextAll('li').attr("style", "display: none !important");
        $('.docTab').addClass('active');
        $('.visaTab').removeClass('active');
        $('#tab6').addClass('active');
        $('#tab7').removeClass('active');
        document.getElementById('Nationality').value = "IND";
        //document.getElementById('City').value = "";

        document.getElementById('Address').innerHTML = "";
        document.getElementById('PhoneNo').value = "";
        document.getElementById('Email').value = "";
        document.getElementById('Gender').value = "Male";
        document.getElementById('frontInput').value = "";
        document.getElementById('backInput').value = "";
        document.getElementById('visaInput').value = "";
        //document.getElementById('CountryofIssue').value = "";
        document.getElementById('DateOfIssue').value = "";
        document.getElementById('ExpiryDate').value = "";
        document.getElementById('DocumentNo').value = "";
        document.getElementById('idcard').src = "../../assets/images/user-1.jpg";
        document.getElementById('imgFront').src = "../../assets/images/user-1.jpg";
        document.getElementById('imgBack').src = "../../assets/images/user-1.jpg";
        document.getElementById('imgVisa').src = "../../assets/images/user-1.jpg";
        document.getElementById('frontImgName').value = "";
        document.getElementById('backImgName').value = "";
        document.getElementById('visaImgName').value = "";
        document.getElementById('VisaNo').value = "";
        document.getElementById('VisaCity').value = "";
        document.getElementById('VisaCountry').value = "";
        document.getElementById('VisaType').value = "";
        document.getElementById('ValidTill').value = "";
        document.getElementById('IssueDate').value = "";
    }

    $('#tableBody').on('click', 'tr', function () {
        var Guest_Code = this.id;
        console.log(Guest_Code);
        if (Guest_Code != "") {
            ClearControls();
            FillGuestData(Guest_Code);
        }
        else {
            ClearControls();
            $('#listGallery').empty();
            var txt = "<li id=''>" +
                "<img alt='' src='../../assets/images/user-1.jpg' style='width:120px; height:90px;'> " +
                "</li>";
            $('#listGallery').append(txt);
        }
    });

    $("#id-btn-dialog1").on('click', function (e) {
        e.preventDefault();

        var dialog = $("#dialog-message").removeClass('hide').dialog({
            modal: true,
            //title: "<i class='fa fa-check'></i> jQuery UI Dialog",
            title: "<i class='fa fa-check'></i> Confirm Details",
            title_html: true,
            buttons: [
                {
                    text: "Cancel",
                    "class": "btn btn-danger btn-sm",
                    click: function () {
                        $(this).dialog("close");
                    }
                },
                {
                    text: "OK",
                    "class": "btn btn-success btn-sm",
                    click: function () {
                        $(this).dialog("close");

                        var CustomerCode = $("#CustomerCode").val();
                        $.ajax({
                            type: "POST",
                            url: "/GuestData/ConfirmDatas",
                            data: "{'Customer_Code':'" + CustomerCode + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () { $("#loader1").css("display", "block"); },
                            complete: function () { $("#loader1").css("display", "none"); },
                            success: OnSuccessSaveCall,
                            error: OnErrorSaveCall
                        });
                        function OnSuccessSaveCall(data) {
                            if (data > 0) {

                                $(window).scrollTop(0);
                                $(".messagebox").append('<div class="well bg-success msg"><strong> Success!</strong> ID Card Details Sent to the Hotel Successfully.</div>');
                                $(".msg").delay(8000).fadeOut(800);
                                setTimeout(
                                    function () {
                                        location.href = "/Home/Dashboard";
                                    }, 800);
                            }
                            else {
                                $(window).scrollTop(0);
                                $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Confirmation Failed.</div>');
                                $(".msg").delay(4000).fadeOut(800);
                            }
                        }
                        function OnErrorSaveCall() {
                            $(window).scrollTop(0);
                            $(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Confirmation Failed.</div>');
                            $(".msg").delay(4000).fadeOut(800);
                        }

                    }
                }
            ]
        });
    });

    $('.btnAddGuest').click(function () {
        $('#btnSaveGuest').show();
        var addedcount = $('#addedcount').val();
        var Guest_Accompany = $('#Guest_Accompany').val();
        Guest_Accompany = Number(Guest_Accompany) + 1;
        if (Guest_Accompany > addedcount) {
            ClearControls();
        }
    });

    function FillGuestData(Guest_Code) {
        $.ajax({
            type: "POST",
            url: "/GuestData/SelectGuestData",
            data: "{'Guest_Code':'" + Guest_Code + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    document.getElementById("iddetails").style.display = "block";
                    document.getElementById('GuestDataID').value = data.entGuestData.Guestdata_id;
                    document.getElementById('GuestCode').value = data.entGuestData.Guest_Code;
                    document.getElementById('FirstName').value = data.entGuestData.Guest_Firstname;
                    document.getElementById('LastName').value = data.entGuestData.Guest_Lastname;
                    //document.getElementById('Father').value = data.entGuestData.Guest_Father;
                    document.getElementById('DOB').value = data.entGuestData.Guest_DOB;
                    document.getElementById('Country').value = data.entGuestData.Guest_Country;
                    //document.getElementById('City').value = data.entGuestData.Guest_City;
                    document.getElementById('State').value = data.entGuestData.Guest_State;
                    document.getElementById('Email').value = data.entGuestData.Guest_Email;
                    document.getElementById('PhoneNo').value = data.entGuestData.Guest_PhoneNo;
                    document.getElementById('Nationality').value = data.entGuestData.Guest_Nationality;
                    if (document.getElementById('Nationality').value == "IND") {
                        $('li.docTab').nextAll('li').attr("style", "display: none !important");
                    }
                    else { $('li.docTab').nextAll('li').show(); }
                    document.getElementById('Address').innerHTML = data.entGuestData.Guest_Address;
                    document.getElementById('Purpose').value = data.entGuestData.Guest_PurposeofVisit;
                    document.getElementById('CardType').value = data.entGuestData.Guest_CardType;
                    document.getElementById('DocumentNo').value = data.entGuestData.Guest_DocumentNo;
                    //document.getElementById('CountryofIssue').value = data.entGuestData.Guest_CountryofIssue;
                    document.getElementById('DateOfIssue').value = data.entGuestData.Guest_DateOfIssue;
                    document.getElementById('ExpiryDate').value = data.entGuestData.Guest_ExpiryDate;
                    document.getElementById('Gender').value = data.entGuestData.Guest_Gender;
                    document.getElementById('VisaNo').value = data.entGuestData.Guest_VisaNo;
                    document.getElementById('VisaCity').value = data.entGuestData.Guest_VisaPOICity;
                    document.getElementById('VisaCountry').value = data.entGuestData.Guest_VisaPOICountry;
                    document.getElementById('VisaType').value = data.entGuestData.Guest_VisaType;
                    document.getElementById('ValidTill').value = data.entGuestData.Guest_VisaValidTill;
                    document.getElementById('IssueDate').value = data.entGuestData.Guest_VisaDateofIssue;
                    

                    if (data.entGuestData.Guest_Document != '' && data.entGuestData.Guest_Document != null) {                     
                        document.getElementById('frontImgName').value = data.entGuestData.Guest_Document;
                        document.getElementById('imgFront').value = "../../CardImages/" + data.entGuestData.Guest_Document;
                    }                  

                    if (data.entGuestData.Guest_DocumentBack != '' && data.entGuestData.Guest_DocumentBack != null) {                       
                        document.getElementById('backImgName').value = data.entGuestData.Guest_DocumentBack;
                        document.getElementById('imgBack').value = "../../CardImages/" + data.entGuestData.Guest_DocumentBack;
                    }                   

                    if (data.entGuestData.Guest_DocumentVisa != '' && data.entGuestData.Guest_DocumentVisa != null) {                      
                        document.getElementById('visaImgName').value = data.entGuestData.Guest_DocumentVisa;
                        document.getElementById('imgVisa').value = "../../CardImages/" + data.entGuestData.Guest_DocumentVisa;
                    }                 
                }
                else {
                }
            },
            error: function () { }
        });
    }

    $('.btnEditGuest').click(function () {
        $('#btnSaveGuest').show();
        var Guest_Code = $(this).closest('tr').attr('id');
        if (Guest_Code != "") {
            FillGuestData(Guest_Code);
        }
    });

    $('.btnViewGuest').click(function () {
        $('#btnSaveGuest').hide();
        var Guest_Code = $(this).closest('tr').attr('id');
        if (Guest_Code != "") {
            FillGuestData(Guest_Code);
        }
    });
    
    $('#Nationality').change(function () {
        var Nationality = $('#Nationality').val();
        if (Nationality == "IND") {            
            $('li.docTab').nextAll('li').attr("style", "display: none !important");
        }
        else { $('li.docTab').nextAll('li').show(); }
    });

    //galleryclick
    $('#divGallery').on('click', 'li', function () {       
        var imageName = this.id; 
        document.getElementById('idcard').src = "../../CardImages/" + imageName;       
    });

    //$('#divGallery').on('click', 'li', function () {
    //    console.log("dd");
    //    var Guest_Code = this.id;    
    //    if (Guest_Code != "") {
    //        $.ajax({
    //            type: "POST",
    //            url: "/GuestData/SelectGuestData",
    //            data: "{'Guest_Code':'" + Guest_Code + "'}",
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            success: function (data) {
    //                if (data != "") {
                     
    //                    if (data.entGuestData.Guest_Document != '' && data.entGuestData.Guest_Document != null) {
    //                        document.getElementById('idcard').src = "../../CardImages/" + data.entGuestData.Guest_Document;
    //                        //document.getElementById('frontImgName').value = data.entGuestData.Guest_Document;                     
    //                    }
    //                    else {
    //                        document.getElementById('idcard').src = "../../assets/images/user-1.jpg";
    //                    }

    //                    if (data.entGuestData.Guest_DocumentBack != '' && data.entGuestData.Guest_DocumentBack != null) {
    //                        document.getElementById('idcard1').src = "../../CardImages/" + data.entGuestData.Guest_DocumentBack;
    //                        //document.getElementById('backImgName').value = data.entGuestData.Guest_DocumentBack;                                         
    //                    }
    //                    else {                       
    //                        document.getElementById('idcard1').src = "../../assets/images/user-1.jpg";
    //                    }

    //                    if (data.entGuestData.Guest_DocumentVisa != '' && data.entGuestData.Guest_DocumentVisa != null) {
    //                        document.getElementById('idcard2').src = "../../CardImages/" + data.entGuestData.Guest_DocumentVisa;
    //                        //document.getElementById('visaImgName').value = data.entGuestData.Guest_DocumentVisa;        
    //                    }
    //                    else {                         
    //                        document.getElementById('idcard2').src = "../../assets/images/user-1.jpg";
    //                    }                        
    //                }
    //                else {
    //                }
    //            },
    //            error: function () { }
    //        });
    //    }
    //    else {
    //        //var addedcount = $('#addedcount').val();
    //        //var Guest_Accompany = $('#Guest_Accompany').val();
    //        //Guest_Accompany = Number(Guest_Accompany) + 1;
    //        //if (Guest_Accompany > addedcount) {
    //        //    ClearControls();
    //        //}
    //        //else {
    //        //    $(window).scrollTop(0);
    //        //    $(".messagebox").append('<div class="well bg-waring msg"><strong> Error!</strong> Already added ' + Guest_Accompany + ' members".</div>');
    //        //    $(".msg").delay(4000).fadeOut(800);
    //        //}
    //    }
    //});

    $('#id-btn-dialog1').click(function () {
        var addedcount = $('#addedcount').val();
        var Guest_Accompany = $('#Guest_Accompany').val();
        Guest_Accompany = Number(Guest_Accompany) + 1;

        document.getElementById('adultcount').innerText = addedcount;
        document.getElementById('childcount').innerText = 0;

        var per = ((parseInt(addedcount) / parseInt(Guest_Accompany)) * 100)
        per = per + '%';
        $('.progressbar').width(per);
        $('.spanpercentage').width(per);
        document.getElementsByClassName('spanpercentage').innerHTML = per;
        $('.progressper').attr('data-percent', per)

    });

});