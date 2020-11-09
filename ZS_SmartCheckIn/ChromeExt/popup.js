document.addEventListener('DOMContentLoaded', function (r) {

    //http://localhost:4766
    //https://zscheckin.atintellilabs.live
    //https://smartcheckin.atintellilabs.live
    
    var btnAssignFrroLogin = document.getElementById('btnAssignFrroLogin');
    var btnAppLogin = document.getElementById('btnAppLogin'); 
    var messagebox = document.getElementById('messagebox');
    var frroid = document.getElementById('User_ID').value;
    var frropass = document.getElementById('User_Pass').value;
    var GustData = document.getElementById('btnGustData');
    var btnLogout = document.getElementById('btnLogout');
    var ddlBranchVal = $("#ddlBranch option:selected").val();
    var lblUsername = document.getElementById('lblUsername');
    var btnRefreshList = document.getElementById('btnRefreshList'); 

    var txtUserName = document.getElementById('txtUserName').value;
    var txtPassword = document.getElementById('txtPassword').value;

    messagebox.innerText = '';


    if (document.getElementById("divAppLogin").style.display == "") {
        $.ajax({
            type: "POST",
            url: "http://localhost:4766/api/GetBranch",
            data: '',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            processData: false,
            crossDomain: true,
            success: OnSuccessSaveCall1,
            error: OnErrorSaveCall1
        });

        function OnSuccessSaveCall1(data) {
            if (data != null && data.length > 0) {
                var appenddata;
                $.each(data, function (key, value) {
                    appenddata += "<option value = '" + value.Branch_ID + " '>" + value.Branch_Name + " </option>";
                });
                $('#ddlBranch').html(appenddata);
            }
            else {
                messagebox.innerText = "Please try later!";
            }
        }

        function OnErrorSaveCall1() {
            messagebox.innerText = "Please try later!";
        }
    }

    chrome.storage.local.get(['user_name', 'pass','branch'], function (data) {
        messagebox.innerText = '';
        if (data != null && data.user_name != null && data.pass != null && data.user_name != '' && data.pass != '') {
            txtUserName = data.user_name;
            txtPassword = data.pass;
            ddlBranchVal = data.branch;

            if (txtUserName != null && txtUserName != '' && txtPassword != null && txtPassword != '') {
                btnAppLogin.click();
            }
        }

    });

    

    $(document).on('click', '.singleData', function () {
        var id = $(this).data('assigned-id');
        var frroflag = $(this).data('assigned-frroflag');

        chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
            if (id != null && id != '') {
                if (frroflag != null && frroflag != 2) {

                    var applicant_surname = '';
                    var applicant_givenname = '';
                    var applicant_sex = '';
                    var dobformat = 'DY';
                    var applicant_dob = '';
                    var applicant_age = '';
                    var actualDOB = '';
                    var applicant_special_category = '9';
                    var applicant_nationality = '';

                    var applicant_permaddr = '';
                    var applicant_permcity = '';
                    var applicant_permcountry = '';

                    var applicant_refaddr = 'EDAPPILLY';
                    var applicant_refstate = '16';
                    var applicant_refstatedistr = '19D';
                    var applicant_refpincode = '683105';

                    var applicant_passpno = '';
                    var applicant_passplcofissue = '';
                    var passport_issue_country = '';
                    var applicant_passpdoissue = '';
                    var applicant_passpvalidtill = '';

                    var applicant_visano = '';
                    var applicant_visaplcoissue = '';
                    var visa_issue_country = '';
                    var applicant_visadoissue = '';
                    var applicant_visavalidtill = '';
                    var applicant_visatype = '';
                    var applicant_visa_subtype_code = '';

                    var applicant_arrivedfromcountry = '';
                    var applicant_arrivedfromcity = '';
                    var applicant_arrivedfromplace = '';
                    var applicant_doarrivalindia = '';
                    var applicant_doarrivalhotel = '';
                    var applicant_timeoarrivalhotel = '';
                    var applicant_intnddurhotel = '1';

                    var employed = 'N';
                    var applicant_purpovisit = '15';
                    var applicant_next_dest_country_flag_r = '';
                    var applicant_next_destination_state_IN = '';
                    var applicant_next_destination_city_district_IN = '';
                    var applicant_next_destination_place_IN = '';
                    var applicant_contactnoinindia = '9665858558';
                    var applicant_mcontactnoinindia = '9665858558';
                    var applicant_contactnoperm = '9665858558';
                    var applicant_mcontactnoperm = '9665858558';
                    var applicant_remark = '';


                    $.ajax({
                        type: "POST",
                        url: "http://localhost:4766/api/GuestDataForChrome",
                        data: "{'Guestdata_id':'" + id + "','Branch_ID':'0'}",
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        processData: false,
                        crossDomain: true,
                        success: OnSuccessSaveCall1,
                        error: OnErrorSaveCall1
                    });

                    function OnSuccessSaveCall1(data) {
                        if (data.length > 0) {
                            applicant_surname = data[0].Guest_Lastname.trim();
                            applicant_givenname = data[0].Guest_Firstname.trim();
                            applicant_sex = data[0].Guest_Gender == "Male" ? "M" : "F";
                            actualDOB = applicant_dob = data[0].Guest_DOB.trim();
                            validateDOBformat();
                            applicant_nationality = data[0].Guest_Nationality.trim();
                            applicant_permaddr = data[0].Guest_Address.replace(/[\r\n]+/gm, '');
                            applicant_arrivedfromplace = applicant_arrivedfromcity = applicant_permcity = data[0].Guest_Country.trim();//data[0].Guest_City.trim();
                            applicant_arrivedfromcountry = applicant_permcountry = data[0].Guest_Country.trim();

                            applicant_refaddr = data[0].Organization.Organization_Address.trim();
                            applicant_refstate = data[0].Organization.Organization_State.trim();
                            //applicant_refstatedistr  = data[0].Organization_Country.trim();
                            applicant_refpincode = data[0].Organization.Organization_PinCode.trim();

                            applicant_passpno = data[0].Guest_DocumentNo.trim();
                            applicant_passplcofissue = data[0].Guest_CountryofIssue.trim();
                            passport_issue_country = data[0].Guest_CountryofIssue.trim();
                            applicant_passpdoissue = data[0].Guest_DateOfIssue.trim();
                            applicant_passpvalidtill = data[0].Guest_ExpiryDate.trim();

                            applicant_visano = data[0].Guest_VisaNo.trim();
                            applicant_visaplcoissue = data[0].Guest_VisaPOICity.trim();
                            visa_issue_country = data[0].Guest_VisaPOICountry.trim();
                            applicant_visadoissue = data[0].Guest_VisaDateofIssue.trim();
                            applicant_visavalidtill = data[0].Guest_VisaValidTill.trim();
                            applicant_visatype = data[0].Guest_VisaType.trim();
                            //applicant_visa_subtype_code = data[0].Guest_VisaNo;

                            applicant_doarrivalindia = data[0].Arrival_Date.trim();
                            applicant_doarrivalhotel = data[0].Arrival_Date.trim();
                            applicant_timeoarrivalhotel = data[0].Arrival_Time.trim();

                            applicant_purpovisit = data[0].Guest_PurposeofVisit.trim();

                            function validateDOBformat() {
                                if (data[0].Guest_DOB == "") {
                                    return false;
                                }
                                var x = new Array();
                                var format = 0;
                                age = 0;
                                x = data[0].Guest_DOB.split("/");
                                if (x[0] != null && x[0].length != null && x[0].length == 1) {
                                    x[0] = "0" + x[0];
                                }
                                if (x[1] != null && x[1].length != null && x[1].length == 1) {
                                    x[1] = "0" + x[1];
                                }
                                DOBformat = "DY";
                                flag = true;

                                var d = new Date();
                                var n = d.getFullYear();

                                if (DOBformat == "DY") {
                                    if ((x[0] == null) || (x[1] == null) || (x[2] == null)) {
                                        applicant_age = "";
                                        return false;
                                    }
                                    format = 3;
                                    if ((x[0] != null) || (x[1] != null) || (x[2] != null))
                                        age = n - parseInt(x[2]);
                                    if ((x[0].length != 2) || ((parseInt(x[0]) > 31) && (parseInt(x[1]) == 1)) || ((parseInt(x[0]) > 28) && (parseInt(x[1]) == 2) && (parseInt(x[2]) % 4 !== 0)) || ((parseInt(x[0]) > 29) && (parseInt(x[1]) == 2) && (parseInt(x[2]) % 4 === 0)) || ((parseInt(x[0]) > 31) && (parseInt(x[1]) == 3)) || ((parseInt(x[0]) > 30) && (parseInt(x[1]) == 4)) || ((parseInt(x[0]) > 31) && (parseInt(x[1]) == 5)) || ((parseInt(x[0]) > 30) && (parseInt(x[1]) == 6)) || ((parseInt(x[0]) > 31) && (parseInt(x[1]) == 7)) || ((parseInt(x[0]) > 31) && (parseInt(x[1]) == 8)) || ((parseInt(x[0]) > 30) && (parseInt(x[1]) == 9)) || ((parseInt(x[0]) > 31) && (parseInt(x[1]) == 10)) || ((parseInt(x[0]) > 30) && (parseInt(x[1]) == 11)) || ((parseInt(x[0]) > 31) && (parseInt(x[1]) == 12))) {
                                        age = 0;
                                        flag = false;
                                    }
                                    if ((x[1].length != 2) || (parseInt(x[1]) > 12) || (x[1] == "00")) {
                                        age = 0;
                                        flag = false;
                                    }
                                    if ((x[2].length != 4) || (parseInt(x[2]) > 2999) || (parseInt(x[2]) < 1900)) {
                                        age = 0;
                                        flag = false;
                                    }
                                    applicant_age = age;
                                }

                                for (var i = 0; i < x.length; i++) {
                                    if (x[i] == "")
                                        flag1 = false;
                                }
                                if (flag == false) {
                                    applicant_age = "";
                                    //dob.setDate(01);
                                    //dob.setMonth(01);
                                    //dob.setYear(1900);
                                }
                                if (x != "") {
                                    if (x.length != format) {
                                        applicant_age = "";
                                    }

                                }
                            }

                            if (applicant_age == null || applicant_age <= 0) {
                                applicant_age = '';
                            }

                            var arr = '';
                            var file = null;
                            var filebase64 = '';

                            if (data[0].Guest_ProfilePic != null) {
                                arr = data[0].Guest_ProfilePic;
                                //document.getElementById("profileImage").src = "data:image/jpg;base64," + arr;
                                //file = dataURLtoFile(document.getElementById("profileImage").src, 'filename.jpg');

                                filebase64 = "data:image/jpg;base64," + arr;
                            }
                            //function dataURLtoFile(dataurl, filename) {
                            //    var arr = dataurl.split(','), mime = arr[0].match(/:(.*?);/)[1],
                            //        bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
                            //    while (n--) {
                            //        u8arr[n] = bstr.charCodeAt(n);
                            //    }
                            //    return new File([u8arr], filename, { type: mime });
                            //}
                            //if (file != null) {
                            //    var fileInput = document.getElementById("fileInput");

                            //    function FileListItem(a) {
                            //        a = [].slice.call(Array.isArray(a) ? a : arguments)
                            //        for (var c, b = c = a.length, d = !0; b-- && d;) d = a[b] instanceof File
                            //        if (!d) throw new TypeError('expected argument to FileList is File or array of File objects')
                            //        for (b = (new ClipboardEvent('')).clipboardData || new DataTransfer; c--;) b.items.add(a[c])
                            //        return b.files
                            //    }
                            //    fileInput.files = new FileListItem(file)
                            //}
                            //var ffgg = document.getElementById("fileInput");


                            //document.getElementsByClassName(\"style16\")[9].click();\

                            var c = "\
                        if('"+ filebase64 + "' != null && '" + filebase64 + "' != '' ){\
                        function dataURLtoFile(dataurl, filename) {\
                        var arr = dataurl.split(','), mime = arr[0].match(/:(.*?);/)[1],bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);\
                        while (n--) {u8arr[n] = bstr.charCodeAt(n);}\
                        return new File([u8arr], filename, { type: mime });}\
                        var filenew = dataURLtoFile('"+ filebase64 + "', 'filename.jpg');\
                        var fileInput = document.getElementById('file1');\
                        fileInput.files = new FileListItem(filenew);\
                        function FileListItem(a) {\
                            a = [].slice.call(Array.isArray(a) ? a : arguments);\
                            for (var c, b = c = a.length, d = !0; b-- && d;) d = a[b] instanceof File;\
                            if (!d) throw new TypeError('expected argument to FileList is File or array of File objects');\
                            for (b = (new ClipboardEvent('')).clipboardData || new DataTransfer; c--;) b.items.add(a[c]);\
                            return b.files}\
                        document.getElementsByClassName(\"style16\")[9].click();}\
                        var changeEvent = document.createEvent(\"HTMLEvents\");\
                        changeEvent.initEvent(\"change\", true, true);\
                        document.getElementsByName(\"applicant_surname\")[0].value = '"+ applicant_surname + "';\
                        document.getElementsByName(\"applicant_givenname\")[0].value = '"+ applicant_givenname + "';\
                        document.getElementsByName(\"applicant_sex\")[0].value = '"+ applicant_sex + "';\
                        document.getElementsByName(\"dobformat\")[0].value = '"+ dobformat + "';\
                        document.getElementsByName(\"applicant_dob\")[0].value = '"+ applicant_dob + "';\
                        document.getElementsByName(\"applicant_dob\")[0].dispatchEvent(changeEvent);\
                        document.getElementsByName(\"applicant_age\")[0].value = '"+ applicant_age + "';\
                        document.getElementsByName(\"actualDOB\")[0].value = '"+ actualDOB + "';\
                        document.getElementsByName(\"applicant_special_category\")[0].value = '"+ applicant_special_category + "';\
                        document.getElementsByName(\"applicant_nationality\")[0].value = '"+ applicant_nationality + "';\
                        document.getElementsByName(\"applicant_permaddr\")[0].value = '"+ applicant_permaddr + "';\
                        document.getElementsByName(\"applicant_permcity\")[0].value = '"+ applicant_permcity + "';\
                        document.getElementsByName(\"applicant_permcountry\")[0].value = '"+ applicant_permcountry + "';\
                        document.getElementsByName(\"applicant_refaddr\")[0].value = '"+ applicant_refaddr + "';\
                        document.getElementsByName(\"applicant_refstate\")[0].value = '"+ applicant_refstate + "';\
                        document.getElementsByName(\"applicant_refstate\")[0].dispatchEvent(changeEvent);\
                        document.getElementsByName(\"applicant_refstatedistr\")[0].value = '"+ applicant_refstatedistr + "';\
                        document.getElementsByName(\"applicant_refpincode\")[0].value = '"+ applicant_refpincode + "';\
                        document.getElementsByName(\"applicant_passpno\")[0].value = '"+ applicant_passpno + "';\
                        document.getElementsByName(\"applicant_passplcofissue\")[0].value = '"+ applicant_passplcofissue + "';\
                        document.getElementsByName(\"passport_issue_country\")[0].value = '"+ passport_issue_country + "';\
                        document.getElementsByName(\"applicant_passpdoissue\")[0].value = '"+ applicant_passpdoissue + "';\
                        document.getElementsByName(\"applicant_passpvalidtill\")[0].value = '"+ applicant_passpvalidtill + "';\
                        document.getElementsByName(\"applicant_visano\")[0].value = '"+ applicant_visano + "';\
                        document.getElementsByName(\"applicant_visaplcoissue\")[0].value = '"+ applicant_visaplcoissue + "';\
                        document.getElementsByName(\"visa_issue_country\")[0].value = '"+ visa_issue_country + "';\
                        document.getElementsByName(\"applicant_visadoissue\")[0].value = '"+ applicant_visadoissue + "';\
                        document.getElementsByName(\"applicant_visavalidtill\")[0].value = '"+ applicant_visavalidtill + "';\
                        document.getElementsByName(\"applicant_visatype\")[0].value = '"+ applicant_visatype + "';\
                        document.getElementsByName(\"applicant_visatype\")[0].dispatchEvent(changeEvent);\
                        document.getElementsByName(\"applicant_arrivedfromcountry\")[0].value = '"+ applicant_arrivedfromcountry + "';\
                        document.getElementsByName(\"applicant_arrivedfromcity\")[0].value = '"+ applicant_arrivedfromcity + "';\
                        document.getElementsByName(\"applicant_arrivedfromplace\")[0].value = '"+ applicant_arrivedfromplace + "';\
                        document.getElementsByName(\"applicant_doarrivalindia\")[0].value = '"+ applicant_doarrivalindia + "';\
                        document.getElementsByName(\"applicant_doarrivalhotel\")[0].value = '"+ applicant_doarrivalhotel + "';\
                        document.getElementsByName(\"applicant_timeoarrivalhotel\")[0].value = '"+ applicant_timeoarrivalhotel + "';\
                        document.getElementsByName(\"applicant_intnddurhotel\")[0].value = '"+ applicant_intnddurhotel + "';\
                        document.getElementsByName(\"employed\")[0].value = '"+ employed + "';\
                        document.getElementsByName(\"applicant_purpovisit\")[0].value = '"+ applicant_purpovisit + "';\
                        document.getElementsByName(\"applicant_next_dest_country_flag_r\")[0].value = '"+ applicant_next_dest_country_flag_r + "';\
                        document.getElementsByName(\"applicant_next_destination_state_IN\")[0].value = '"+ applicant_next_destination_state_IN + "';\
                        document.getElementsByName(\"applicant_next_destination_city_district_IN\")[0].value = '"+ applicant_next_destination_city_district_IN + "';\
                        document.getElementsByName(\"applicant_next_destination_place_IN\")[0].value = '"+ applicant_next_destination_place_IN + "';\
                        document.getElementsByName(\"applicant_contactnoinindia\")[0].value = '"+ applicant_contactnoinindia + "';\
                        document.getElementsByName(\"applicant_mcontactnoinindia\")[0].value = '"+ applicant_mcontactnoinindia + "';\
                        document.getElementsByName(\"applicant_contactnoperm\")[0].value = '"+ applicant_contactnoperm + "';\
                        document.getElementsByName(\"applicant_mcontactnoperm\")[0].value = '"+ applicant_mcontactnoperm + "';\
                        document.getElementsByName(\"applicant_remark\")[0].value = '" + applicant_remark + "';"


                            //showcities('"+ applicant_refstate + "', 'citydata', 'applicant_refstatedistr', 'false');\
                            //document.getElementsByName("country")[0].onchange();
                            //var changeEvent = document.createEvent("HTMLEvents");
                            //changeEvent.initEvent("change", true, true);
                            //document.getElementsByName("country")[0].dispatchEvent(changeEvent);C:\fakepath\IDCard-2.jpg

                            chrome.tabs.executeScript(
                                tabs[0].id,
                                { code: c });

                            chrome.runtime.sendMessage({ msg: "startFunc" });

                        }
                    }

                    function OnErrorSaveCall1() {
                        //alert("Failed");
                    }


                }
                else {

                    var nat_ion = '';
                    var appli_id = '';
                    var passport_no = '';
                    var vissa_no = '';
                    var applicant_arrivalhotel = '';

                    $.ajax({
                        type: "POST",
                        url: "http://localhost:4766/api/GuestDataForChrome",
                        data: "{'Guestdata_id':'" + id + "','Branch_ID':'0'}",
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        processData: false,
                        crossDomain: true,
                        success: OnSuccessSaveCall1,
                        error: OnErrorSaveCall1
                    });

                    function OnSuccessSaveCall1(data) {
                        if (data.length > 0) {
                            
                            nat_ion = data[0].Guest_Nationality.trim();
                            appli_id = data[0].guest_FrroChellan.trim();
                            passport_no = data[0].Guest_DocumentNo.trim();
                            vissa_no = data[0].Guest_VisaNo.trim();
                            applicant_arrivalhotel = data[0].Arrival_Date.trim();

                            var c = "\
                            var changeEvent = document.createEvent(\"HTMLEvents\");\
                            changeEvent.initEvent(\"change\", true, true);\
                            document.getElementsByName(\"nat_ion\")[0].value = '"+ nat_ion + "';\
                            document.getElementsByName(\"appli_id\")[0].value = '"+ appli_id + "';\
                            document.getElementsByName(\"passport_no\")[0].value = '"+ passport_no + "';\
                            document.getElementsByName(\"vissa_no\")[0].value = '"+ vissa_no + "';\
                            document.getElementsByName(\"applicant_arrivalhotel\")[0].value = '"+ applicant_arrivalhotel + "';"

                            chrome.tabs.executeScript(
                                tabs[0].id,
                                { code: c });

                            chrome.runtime.sendMessage({ msg: "startFunc" });

                        }
                    }

                    function OnErrorSaveCall1() {
                        //alert("Failed");
                    }


                }

            }
            else {
                messagebox.innerText = "Please try later!"
            }
        });

        function tt1() {messagebox.innerText = "test";}
    });



    btnAppLogin.addEventListener('click', function (e) {
        chrome.tabs.getSelected(null, function (tab) {
        });

        chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {

            document.getElementById("divFrrodetails").style.display = "none";
            $('#guestlist').empty();
            messagebox.innerText = '';

            if (txtUserName == null ||txtUserName == '') {
                txtUserName = document.getElementById('txtUserName').value;
            }

            if (txtPassword == null ||txtPassword == '') {
                txtPassword = document.getElementById('txtPassword').value;
            }

            if (ddlBranchVal == null || ddlBranchVal == '') {
                ddlBranchVal = $("#ddlBranch option:selected").val();
            }
            

            if (txtUserName != null && txtUserName != '' && txtPassword != null && txtPassword != '') {
                var loginData = new FormData();
                loginData.append("User_Username", "a");
                loginData.append("User_Password", "a");

                var user;
                var pass;

                $.ajax({
                    type: "POST",
                    url: "http://localhost:4766/api/ZeroLogin",
                    data: "{'User_Username':'" + txtUserName + "','User_Password':'" + txtPassword + "','Branch_ID':'" + ddlBranchVal + "'}", 
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    processData: false,
                    //headers: {'Content-Type': 'application/x-www-form-urlencoded'},
                    crossDomain: true,
                    success: OnSuccessSaveCall,
                    //error: function (ts) { alert(ts.responseText) }
                    error: OnErrorSaveCall
                });
                function OnSuccessSaveCall(data) {
                    if (data != null && data.User_Name != null && data.User_Name != '') {

                        lblUsername.innerText = data.User_Name;

                        chrome.storage.local.set({ 'user_name': txtUserName })
                        chrome.storage.local.set({ 'pass': txtPassword })
                        chrome.storage.local.set({ 'branch': ddlBranchVal })
                        chrome.storage.local.set({ 'UserID': data.User_ID })

                        document.getElementById("divFrrodetails").style.display = "";
                        document.getElementById("divAppLogin").style.display = "none";

                        frroid = data[0];
                        frropass = data[1];

                        BindList();
                    }
                    else {
                        ClearAll();
                        messagebox.innerText = "Wrong credentials!";
                    }
                }
                function OnErrorSaveCall() {
                    //alert("Failed");
                }
            }
            else {
                messagebox.innerText = "Please Enter user name and password!";
            }
        });
    }, false);

    function BindList() {

        $('#guestlist').empty();
        messagebox.innerText = '';

        var Branch_ID = 0;
        if (ddlBranchVal != '') {
            Branch_ID = ddlBranchVal;
        }

        $.ajax({
            type: "POST",
            url: "http://localhost:4766/api/GuestDataForChrome",
            data: "{'Guestdata_id':'0','Branch_ID':'" + Branch_ID + "'}",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            processData: false,
            crossDomain: true,
            success: OnSuccessSaveCall1,
            error: OnErrorSaveCall1
        });

        function OnSuccessSaveCall1(data) {
            if (data.length > 0) {
                $.each(data, function (k, v) {

                    //var txt = "<div style=\"border: 1px solid lightgreen; height: 30px;\">" +
                    //    "<a href=\"#\" data-assigned-id=\"" + v.Guestdata_id + "\" class=\"singleData\" style=\"text-decoration: none\">" + v.Guest_Firstname + ' ' + v.Guest_Lastname + "</a>" +
                    //    "<input type=\"text\" id=\"txtGuest" + v.Guestdata_id + "\" class=\"txt\" value=\"" + v.guest_FrroChellan + "\"> <button type=\"button\" data-assigned-id=\"" + v.Guestdata_id + "\" class=\"btn btnSaveAppId\" >Complete</button>"+
                    //    "</div>"


                    var txt = '';
                    txt = "<div style=\"border: 1px solid yellowgreen; height: 32px;margin-top: 4px;\" class=\"divguestlistAll\" >" +
                        "<a href=\"#\" data-assigned-id=\"" + v.Guestdata_id + "\" data-assigned-frroflag=\"" + v.Guest_PassToFRRO + "\" class=\"singleData\" style=\"text-decoration: none;text-transform: uppercase;padding-left: 12px;\">" + v.Guest_Firstname + ' ' + v.Guest_Lastname + "</a>";
                    if (v.Guest_PassToFRRO != 2)
                    {
                        txt = txt + "<input type=\"image\" src=\"images\\complete.png\" data-assigned-id=\"" + v.Guestdata_id + "\" class=\"btn btnShowAppId\" title=\"Complete Check-In\"  style=\"height: 32px;float: right;\"></input>" +
                            "<div id=\"divAppID\"  style=\"margin-left: 263px;margin-top: -18px\;display:none;\">" + "<input type=\"text\"  id=\"txtGuest" + v.Guestdata_id + "\" class=\"txt\" placeholder=\"Application ID\" value=\"" + v.guest_FrroChellan + "\"> <input type=\"image\" src=\"images\\complete.png\" data-assigned-id=\"" + v.Guestdata_id + "\" class=\"btn btnSaveAppId\" title=\"Complete Check-In\"  style=\"float: right;margin - top: 1px;\"></input>" +
                            "</div>";
                    }
                    else
                    {
                        txt = txt + "<input type=\"image\" src=\"images\\complete.png\" data-assigned-id=\"" + v.Guestdata_id + "\" class=\"btn btnCheckOut\" title=\"Complete Check-Out\"  style=\"height: 32px;float: right;\"></input>";
                    }
                    //"<input type=\"image\" src=\"images\\complete.png\" data-assigned-id=\"" + v.Guestdata_id + "\" class=\"btn btnShowAppId\" title=\"Complete Check-In\"  style=\"height: 32px;float: right;\"></input>" +
                    //"<div id=\"divAppID\"  style=\"margin-left: 263px;margin-top: -18px\;display:none;\">" + "<input type=\"text\"  id=\"txtGuest" + v.Guestdata_id + "\" class=\"txt\" placeholder=\"Application ID\" value=\"" + v.guest_FrroChellan + "\"> <input type=\"image\" src=\"images\\complete.png\" data-assigned-id=\"" + v.Guestdata_id + "\" class=\"btn btnSaveAppId\" title=\"Complete Check-In\"  style=\"float: right;margin - top: 1px;\"></input>" +
                    //"</div>" +
                    //"<input type=\"image\" style=\"display:" + chkout + ";\" src=\"images\\complete.png\" data-assigned-id=\"" + v.Guestdata_id + "\" class=\"btn btnCheckOut\" title=\"Complete Check-Out\"  style=\"height: 32px;float: right;\"></input>" +
                    txt = txt + "</div>";

                    $('#guestlist').append(txt);
                });
            }
        }

        function OnErrorSaveCall1() {

        }

    }


    btnRefreshList.addEventListener('click', function (e) {
        chrome.tabs.getSelected(null, function (tab) {
        });

        chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {

            BindList();

        });
    }, false);


    function ClearAll() {
        document.getElementById("divFrrodetails").style.display = "none";
        document.getElementById("divAppLogin").style.display = "";

        document.getElementById('txtUserName').value = '';
        document.getElementById('txtPassword').value = '';
        //$("#ddlBranch option:selected").val('');

        txtUserName = '';
        txtPassword = '';
        ddlBranchVal = '';

        $('#guestlist').empty();

        chrome.storage.local.set({ 'user_name': '' })
        chrome.storage.local.set({ 'pass': '' })
        chrome.storage.local.set({ 'branch': '' })

        chrome.storage.local.clear(function () {
            var error = chrome.runtime.lastError;
            if (error) {
                console.error(error);
            }
        })
    }


    btnLogout.addEventListener('click', function (e) {
        chrome.tabs.getSelected(null, function (tab) {
        });

        chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {

            ClearAll();

            //document.getElementById("divFrrodetails").style.display = "none";
            //document.getElementById("divAppLogin").style.display = "";

            //document.getElementById('txtUserName').value='';
            //document.getElementById('txtPassword').value = '';
            //$("#ddlBranch option:selected").val('');

            //txtUserName = '';
            //txtPassword = '';
            //ddlBranchVal = '';

            //$('#guestlist').empty();

            //chrome.storage.local.set({ 'user_name': '' })
            //chrome.storage.local.set({ 'pass': '' })
            //chrome.storage.local.set({ 'branch': '' })

            //chrome.storage.local.clear(function () {
            //    var error = chrome.runtime.lastError;
            //    if (error) {
            //        console.error(error);
            //    }
            //})

        });
    }, false);

    $(document).on('click', '.btnShowAppId', function () {
        var divAppID = $(this).closest('divguestlistAll').next().find('.divAppID').context.nextSibling;
        if (divAppID != null) {
            divAppID.style.display = "";
            this.style.display = "none";
        }
        //divAppID.style.display = "none";

        var appNo = '';
        chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
            //if (id != null && id != '') {

            //}
            //else {
            //    messagebox.innerText = "Please try later!"
            //}

        });

    });

    $(document).on('mousedown', '.txt', function () {
        var txtid = this.id;
      
        chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {

            if (txtid != null && txtid.value != null && txtid.value == '') {
                const { id: tabId } = tabs[0].url;
                let code = "\document.getElementsByTagName(\"h1\")[0].innerHTML;"

                chrome.tabs.executeScript(tabId, { code }, function (result) {
                    txtid.value = result;
                });
            }
        });
    });

    $(document).on('click', '.btnSaveAppId', function () {
        var id = $(this).data('assigned-id');

        var appNo = '';
        chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
            if (id != null && id != '') {

                var txtid = '';
                if (document.getElementById('txtGuest' + id) != null) {
                    txtid = document.getElementById('txtGuest' + id).value;
                }

                //if (txtid == null || txtid == '') {
                //    const { id: tabId } = tabs[0].url;
                //    let code = "\document.getElementsByTagName(\"h1\")[0].innerHTML;"

                //    chrome.tabs.executeScript(tabId, { code }, function (result) {
                //        appNo = result;
                //    });
                //}
                //else {
                //    appNo = txtid;
                //}
                if (txtid != null && txtid != '') {
                    appNo = txtid;
                }
                if (appNo != '') {
                    var createdBy = '0';
                    chrome.storage.local.get(['UserID'], function (data) {
                        if (data != null && data.UserID != null && data.UserID != '') {
                            createdBy = data.UserID;

                            $.ajax({
                                type: "POST",
                                url: "http://localhost:4766/api/UpdateFRROStatusForChrome",
                                data: "{'Guestdata_id':'" + id + "','guest_FrroChellan':'" + appNo + "','Guest_FrroEntryUser':'" + createdBy + "'}",
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json',
                                processData: false,
                                crossDomain: true,
                                success: OnSuccessSaveCall1,
                                error: OnErrorSaveCall1
                            });

                            function OnSuccessSaveCall1(data) {
                                if (data != null && data > 0) {
                                    BindList();
                                    messagebox.innerText = "Application Id saved successfully";
                                }
                                else {
                                    messagebox.innerText = "Please try later!";
                                }
                            }

                            function OnErrorSaveCall1() {
                                messagebox.innerText = "Please try later!";
                            }

                        }
                        else {
                            messagebox.innerText = "Please enter Application Id!";
                        }
                    });
                }
                else {
                    messagebox.innerText = "Please enter Application Id!";
                }
            }
            else {
                messagebox.innerText = "Please try later!"
            }

        });

    });

    $(document).on('click', '.btnCheckOut', function () {
        var id = $(this).data('assigned-id');

        if (id != null && id != '') {

            var createdBy = '0';
            chrome.storage.local.get(['UserID'], function (data) {
                if (data != null && data.UserID != null && data.UserID != '' ) {
                    createdBy = data.UserID; 

                    $.ajax({
                        type: "POST",
                        url: "http://localhost:4766/api/UpdateFRROCheckOutStatusChrome",
                        data: "{'Guestdata_id':'" + id + "','Guest_FrroCheckOutUser':'" + createdBy + "'}",
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        processData: false,
                        crossDomain: true,
                        success: OnSuccessSaveCall1,
                        error: OnErrorSaveCall1
                    });

                    function OnSuccessSaveCall1(data) {
                        if (data != null && data > 0) {
                            BindList();
                            messagebox.innerText = "Check out successfully";
                        }
                        else {
                            messagebox.innerText = "Please try later!";
                        }
                    }

                    function OnErrorSaveCall1() {
                        messagebox.innerText = "Please try later!";
                    }
                }
                else {
                    messagebox.innerText = "Please try later!"
                }
            });  
        }
        else {
            messagebox.innerText = "Please try later!"
        }
    });



    btnAssignFrroLogin.addEventListener('click', function (e) {
        chrome.tabs.getSelected(null, function (tab) {
       
        });
        frroid = "zerosnap123";
        frropass = "Zerosnap@1234";

        chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
            if (frroid != null && frroid != '' && frropass != null && frropass != '') {
                var c = "\
            document.getElementsByName(\"uid\")[0].value = '"+ frroid + "';\
            document.getElementsByName(\"pwd\")[0].value = '"+ frropass + "';"

                chrome.tabs.executeScript(
                    tabs[0].id,
                    { code: c });
            }
        });
    }, false); 


    GustData.addEventListener('click', function (e) {
        chrome.tabs.getSelected(null, function(tab) {  
            d = document;  
        }); 

    
        chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {

            var data = new FormData();
            data.append("User_Username", "arun");
            data.append("User_Password", "123");

            var user;
            var pass;
            
            $.ajax({
                type: "POST",
                url: "http://localhost:4766/api/ZeroLogin",
                data: JSON.stringify(data),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                processData: false,
                //dataType: "json",
                crossDomain: true,
                success: OnSuccessSaveCall,
                //error: function (ts) { alert(ts.responseText) }
                error: OnErrorSaveCall
            });
            function OnSuccessSaveCall(data) {
                if (data.length > 1) {
                    user = data[0];
                    pass = data[1];

                    var data1 = new FormData();
                    data1.append("User_Username", "arun");

                    $.ajax({
                        type: "POST",
                        url: "http://localhost:4766/api/GuestDataForChrome",
                        data: JSON.stringify(data1),
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        processData: false,
                        //dataType: "json",
                        crossDomain: true,
                        success: OnSuccessSaveCall1,
                        //error: function (ts) { alert(ts.responseText) }
                        error: OnErrorSaveCall1
                    });

                    function OnSuccessSaveCall1(data) {
                        if (data.length > 1) {
                            $.each(data, function (k, v) {
                               // $.each(this, function (k, v) {

                                    var txt = "<div style=\"border: 1px solid lightgreen; height: 30px;\">" +
                                        "<a href=\"#\" style=\"text-decoration: none\">" +
                                        "<div style=\"border: 1px solid red; width: 40%; margin-left: 30%; margin-right: 30%; height: 29px; display:table-caption;\">" +
                                        "<div style=\"padding-top: 6px; font-size: 15px; padding-left: 45%;\">" + v.Guest_Firstname+"</div>" +
                                        "</div>" +
                                        "</a>" +
                                        "</div>"

                                    $('#guestlist').append(txt);

                                //});
                            });
                        }
                    }

                    function OnErrorSaveCall1() {
                        //alert("Failed");
                    }

                    var c = "\
            document.getElementsByName(\"uid\")[0].value = '"+ user + "';\
            document.getElementsByName(\"pwd\")[0].value = '"+ pass + "';"

                    chrome.tabs.executeScript(
                        tabs[0].id,
                        { code: c });
                }
            }
            function OnErrorSaveCall() {
                //alert("Failed");
            }




            
        });
    }, false);  
}, false);

