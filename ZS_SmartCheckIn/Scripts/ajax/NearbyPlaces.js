$(document).ready(function () {

    $('#upNearbyPlacesImg').change(function () {
        $('.txtDocName').val($('#upNearbyPlacesImg')[0].files[0].name);
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
        btnAddNewNearbyPlaces.click();
    }

    $('.btnClosePopup').click(function () {
        location.href = "/Master/NearbyPlaces";
    });

    function Validate() {
        var flag = true;
        $(".requiredv").html("");
        var txtNearbyPlacesName = $('.txtNearbyPlacesName').val();

        if (txtNearbyPlacesName == null || txtNearbyPlacesName.trim() == "") {
            $("#validNearbyPlacesName").html("This field is required.");
            flag = false;
        }

        return flag;
    }

    function htmlEntities(str) {
        return String(str).replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;');
    }

    $('.btnAddNearbyPlaces').click(function () {
        if (Validate() == true) {

            var NearbyPlacesId = $('#NearbyPlacesId').val();
            var txtNearbyPlacesName = $('.txtNearbyPlacesName').val();
            var txtDescription = $('.txtDescription')[0].innerText;
            var txtDistance = $('.txtDistance').val();
            var txtLocationMap = htmlEntities($('.txtLocationMap').val());
            var ddlStatus = $(".ddlStatus option:selected").val();
            var file = document.getElementById("upNearbyPlacesImg").files[0];
            var UploadFileID = 0;//$('#UploadFileID').val();

            var data = new FormData();
            data.append("NearbyPlacesId", NearbyPlacesId);
            data.append("NearbyPlacesName", txtNearbyPlacesName);
            data.append("Description", txtDescription);
            data.append("Distance", txtDistance);
            data.append("LocationMap", txtLocationMap);
            data.append("Status", ddlStatus);
            data.append("UploadImg", file);
            data.append("UploadFileID", UploadFileID);

            var data1 = {
                "NearbyPlacesId": NearbyPlacesId, "NearbyPlacesName": txtNearbyPlacesName,
                "Description": txtDescription, "Distance": txtDistance, "LocationMap": txtLocationMap, "Status": ddlStatus,
                "UploadImg": file, "UploadFileID": UploadFileID
            }

            $.ajax({
                type: "POST",
                url: "/Master/SaveNearbyPlaces",
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
                location.href = "/Master/NearbyPlaces";
                if (data == "1") {
                    alert('NearbyPlaces save successfully');
                }
                else if (data == "2") {
                    alert('NearbyPlaces updated successfully');
                }
                else {
                    alert('Error occur');
                }
            }
            function OnErrorSaveCall(d) {
                alert('Error occur');
            }
        }
    });

    $('.btnNearbyPlacesDelete').click(function () {
        if (confirm("Are You Sure You Want To Delete?")) {
            var NearbyPlacesId = $(this).data('assigned-id');
            var UploadFileID = $(this).data('assigned-fileid');
            var DocName = $(this).data('assigned-filenameorig');

            var data = new FormData();
            data.append("NearbyPlacesId", NearbyPlacesId);
            data.append("UploadFileID", UploadFileID);
            data.append("DocName", DocName);

            $.ajax({
                type: "POST",
                url: "/Master/DeleteNearbyPlaces",
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
                    alert('NearbyPlaces deleted successfully');
                    location.href = "/Master/NearbyPlaces";
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