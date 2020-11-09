$(document).ready(function () {

    $('#upCuisineImg').change(function () {
        $('.txtDocName').val($('#upCuisineImg')[0].files[0].name);
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
        btnAddNewCuisine.click();
    }

    $('.btnClosePopup').click(function () {
        location.href = "/Master/Cuisine";
    });

    function Validate() {
        var flag = true;
        $(".requiredv").html("");
        var txtCuisineName = $('.txtCuisineName').val();

        if (txtCuisineName == null || txtCuisineName.trim() == "") {
            $("#validCuisineName").html("This field is required.");
            flag = false;
        }

        return flag;
    }

    $('.btnAddCuisine').click(function () {
        if (Validate() == true) {
            var CuisineId = $('#CuisineId').val();
            var txtCuisineName = $('.txtCuisineName').val();
            var txtDescription = $('.txtDescription')[0].innerText;
            var ddlCuisinetype = $(".ddlCuisinetype option:selected").val();
            var ddlStatus = $(".ddlStatus option:selected").val();
            var file = document.getElementById("upCuisineImg").files[0];
            var UploadFileID = 0;// $('#UploadFileID').val();

            var data = new FormData();
            data.append("CuisineId", CuisineId);
            data.append("CuisineName", txtCuisineName);
            data.append("Description", txtDescription);
            data.append("CuisineType", ddlCuisinetype);
            data.append("Status", ddlStatus);
            data.append("UploadImg", file);
            data.append("UploadFileID", UploadFileID);

            $.ajax({
                type: "POST",
                url: "/Master/SaveCuisine",
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
                location.href = "/Master/Cuisine";
                if (data == "1") {
                    alert('Cuisine save successfully');
                }
                else if (data == "2") {
                    alert('Cuisine updated successfully');
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

    $('.btnCuisineDelete').click(function () {
        if (confirm("Are You Sure You Want To Delete?")) {
            var CuisineId = $(this).data('assigned-id');
            var UploadFileID = $(this).data('assigned-fileid');
            var DocName = $(this).data('assigned-filenameorig');

            var data = new FormData();
            data.append("CuisineId", CuisineId);
            data.append("UploadFileID", UploadFileID);
            data.append("DocName", DocName);

            $.ajax({
                type: "POST",
                url: "/Master/DeleteCuisine",
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
                    alert('Cuisine deleted successfully');
                    location.href = "/Master/Cuisine";
                    //$(window).scrollTop(0);
                    //$(".messagebox").append('<div class="well bg-success msg"><strong> Success!</strong>Cuisine deleted successfully.</div>');
                    //$(".msg").delay(8000).fadeOut(800);
                    //setTimeout(
                    //    function () {
                    //        location.href = "/Master/Cuisine";
                    //    }, 800);    
                }
                else {
                    alert('Error occur');
                    //$(window).scrollTop(0);
                    //$(".messagebox").append('<div class="well bg-primary msg"><strong> Error!</strong> Failed in delete.</div>');
                    //$(".msg").delay(4000).fadeOut(800);
                }
            }
            function OnErrorSaveCall() {
                alert('Error occur');
            }
        }
    });




});