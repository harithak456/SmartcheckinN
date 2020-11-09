$(document).ready(function () {

    $('#upAmenitiesImg').change(function () {
        $('.txtDocName').val($('#upAmenitiesImg')[0].files[0].name);
    });

    function getParameterByName(name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, '\\$&');
        var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, ' '));
    }

    var paramid = getParameterByName('id');

    if (paramid != null && paramid != '' && paramid > 0) {
        btnAddNewAmenities.click();
    }

    $('.btnClosePopup').click(function () {
        location.href = "/Master/Amenities";
    });

    function Validate() {
        var flag = true;
        $(".requiredv").html("");
        var txtAmenitiesName = $('.txtAmenitiesName').val();

        if (txtAmenitiesName == null || txtAmenitiesName.trim() == "") {
            $("#validAmenitiesName").html("This field is required.");
            flag = false;
        }

        return flag;
    }

    $('.btnAddAmenities').click(function () {
        if (Validate() == true) {
            var AmenitiesId = $('#AmenitiesId').val();
            var txtAmenitiesName = $('.txtAmenitiesName').val();
            var txtDescription = $('.txtDescription')[0].innerText;
            var ddlStatus = $(".ddlStatus option:selected").val();
            var file = document.getElementById("upAmenitiesImg").files[0];
            var UploadFileID = 0;//$('#UploadFileID').val();

            var data = new FormData();
            data.append("AmenitiesId", AmenitiesId);
            data.append("AmenitiesName", txtAmenitiesName);
            data.append("Description", txtDescription);
            data.append("Status", ddlStatus);
            data.append("UploadImg", file);
            data.append("UploadFileID", UploadFileID);

            $.ajax({
                type: "POST",
                url: "/Master/SaveAmenities",
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
                location.href = "/Master/Amenities";
                if (data == "1") {
                    alert('Amenities save successfully');
                }
                else if (data == "2") {
                    alert('Amenities updated successfully');
                }
                else {
                    alert('Error occur');
                }
            }
            function OnErrorSaveCall() {
                alert('Error occur');
            }
        }
    });

    $('.btnAmenitiesDelete').click(function () {
        if (confirm("Are You Sure You Want To Delete?")) {
            var AmenitiesId = $(this).data('assigned-id');
            var UploadFileID = $(this).data('assigned-fileid');
            var DocName = $(this).data('assigned-filenameorig');

            var data = new FormData();
            data.append("AmenitiesId", AmenitiesId);
            data.append("UploadFileID", UploadFileID);
            data.append("DocName", DocName);

            $.ajax({
                type: "POST",
                url: "/Master/DeleteAmenities",
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

                if (data == "1") {
                    alert('Amenities deleted successfully');
                    location.href = "/Master/Amenities";
                }
                else {
                    alert('Error occur');
                }
            }
            function OnErrorSaveCall() {
                alert('Error occur');
            }
        }
    });




});