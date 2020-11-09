$(document).ready(function () {

    $('input[type="radio"]').change(function () {
        var vals = this.value;

        if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)
            || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) {
            if (vals != "") {
                $('table tr').hide().filter(function () {
                    return vals.indexOf($(this).find('td:eq(2)').text()) > -1;
                }).show();
            }
            else { $('table tr').show(); }
        }
        else {
            var vals = this.value;
            if (vals != "") {
                $('table tr').hide().filter(function () {
                    return vals.indexOf($(this).find('td:eq(4)').text()) > -1;
                }).show();
            }
            else { $('table tr').show(); }
        }
    });

    $('.btnGuestReport').click(function () {
        var FromDate = $('#dtp_input1').val();
        var ToDate = $('#dtp_input2').val();
    

        $.ajax({
            type: "POST",
            url: "/Admin/GuestReportSearch",
            data: "{'FromDate':'" + FromDate + "','ToDate':'" + ToDate + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //beforeSend: function () { $("#loader").css("display", "block"); },
            //complete: function () { $("#loader").css("display", "none"); },
            success: function (data) {
                $('#reportBody').empty();
                if (data != "") {
                    var i = 0;                   
                    $.each(data, function (index, dbval) {
                        i++;
                        var nowDate = new Date(parseInt(dbval.Arrival_Date.substr(6)))
                        var cDate = DateFormat(nowDate);     
                        if (dbval.Is_Active == 1) {
                            var txt = "<tr id=" + dbval.Customer_Code + "><td>" + i + "</td><td>" + dbval.Guest_Firstname + "</td><td>" + cDate + "</td>" +
                                "<td>" + dbval.Guest_MobileNo + "</td><td style='display: none'>" + dbval.Is_Active + "</td>" +
                                "<td><span class='label label-pending arrowed -in -right arrowed -in'>New Guest</span></td></tr> ";
                        }
                        else if (dbval.Is_Active == 2) {
                            var txt = "<tr id=" + dbval.Customer_Code + "><td>" + i + "</td><td>" + dbval.Guest_Firstname + "</td><td>" + cDate+ "</td>" +
                                "<td>" + dbval.Guest_MobileNo + "</td><td style='display: none'>" + dbval.Is_Active + "</td>" +
                                "<td><span class='label label-terminated arrowed -in -right arrowed -in'>ID Not Verified</span></td></tr> ";
                        }
                        else if (dbval.Is_Active == 3) {
                            var txt = "<tr id=" + dbval.Customer_Code + "><td>" + i + "</td><td>" + dbval.Guest_Firstname + "</td><td>" + cDate + "</td>" +
                                "<td>" + dbval.Guest_MobileNo + "</td><td style='display: none'>" + dbval.Is_Active + "</td>" +
                                "<td><span class='label label-suspended arrowed -in -right arrowed -in'>ID Verified</span></td></tr> ";
                        }
                        else if (dbval.Is_Active == 4) {
                            var txt = "<tr id=" + dbval.Customer_Code + "><td>" + i + "</td><td>" + dbval.Guest_Firstname + "</td><td>" + cDate + "</td>" +
                                "<td>" + dbval.Guest_MobileNo + "</td><td style='display: none'>" + dbval.Is_Active + "</td>" +
                                "<td><span class='label label-paid arrowed -in -right arrowed -in'>Checked-In</span></td></tr> ";
                        }
                        $('#reportBody').append(txt);
                    });
                }
            },
            error: function () { }
        });
    });

    $('.btnFRROReport').click(function () {
        var FromDate = $('#dtp_input1').val();
        var ToDate = $('#dtp_input2').val();


        $.ajax({
            type: "POST",
            url: "/Admin/FRROReportSearch",
            data: "{'FromDate':'" + FromDate + "','ToDate':'" + ToDate + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //beforeSend: function () { $("#loader").css("display", "block"); },
            //complete: function () { $("#loader").css("display", "none"); },
            success: function (data) {
                $('#reportBody').empty();
                if (data != "") {
                    var i = 0;
                    $.each(data, function (index, dbval) {
                        i++;
                        if (dbval.Guest_PassToFRRO == 0 || dbval.Guest_PassToFRRO == 1) {
                            var txt = "<tr id=" + dbval.Guest_Code + "><td>" + i + "</td><td>" + dbval.Guest_Firstname + "</td><td>" + dbval.guest_FrroChellan + "</td>" +

                                "<td><span class='label label-terminated arrowed-in-right arrowed-in'>FRRO Pending</span></td>" +
                                "<td>" + dbval.Guest_FrroEntryDate + "</td><td>" + dbval.Guest_FrroEntryUserName + "</td><td>" + dbval.Guest_FrroCheckOutDate + "</td>" +
                                "<td>" + dbval.Guest_FrroCheckOutUserName + "</td></tr > ";
                        }
                        else if (dbval.Guest_PassToFRRO == 2) {
                            var txt = "<tr id=" + dbval.Guest_Code + "><td>" + i + "</td><td>" + dbval.Guest_Firstname + "</td><td>" + dbval.guest_FrroChellan + "</td>" +

                                "<td><span class='label label-pending arrowed-in-right arrowed-in'>Checked-In</span></td>" +
                                "<td>" + dbval.Guest_FrroEntryDate + "</td><td>" + dbval.Guest_FrroEntryUserName + "</td><td>" + dbval.Guest_FrroCheckOutDate + "</td>" +
                                "<td>" + dbval.Guest_FrroCheckOutUserName + "</td></tr > ";
                        }
                        else if (dbval.Guest_PassToFRRO == 3) {
                            var txt = "<tr id=" + dbval.Guest_Code + "><td>" + i + "</td><td>" + dbval.Guest_Firstname + "</td><td>" + dbval.guest_FrroChellan + "</td>" +

                                "<td><span class='label label-suspended arrowed-in-right arrowed-in'>Checked-Out</span></td>" +
                                "<td>" + dbval.Guest_FrroEntryDate + "</td><td>" + dbval.Guest_FrroEntryUserName + "</td><td>" + dbval.Guest_FrroCheckOutDate + "</td>" +
                                "<td>" + dbval.Guest_FrroCheckOutUserName + "</td></tr > ";
                        }

                        $('#reportBody').append(txt);
                    });
                }
            },
            error: function () { }
        });
    });

    function DateFormat(userDate) {

        date = new Date(userDate);
        year = date.getFullYear();
        month = date.getMonth() + 1;
        dt = date.getDate();

        if (dt < 10) {
            dt = '0' + dt;
        }
        if (month < 10) {
            month = '0' + month;
        }

        var newDate = dt + '/' + month + '/' + year;
        return newDate;
    }

});