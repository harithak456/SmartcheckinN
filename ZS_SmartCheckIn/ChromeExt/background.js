
chrome.runtime.onInstalled.addListener(function () {
    console.log("Extension is loaded.");
});


chrome.runtime.onMessageExternal.addListener(
    function (request, sender, sendResponse) {
        if (sender.url == blocklistedWebsite)
            return;  // don't allow this web page access
        if (request.openUrlInEditor)
            openUrl(request.openUrlInEditor);
    });



chrome.runtime.onMessage.addListener(
    function (request, sender, sendResponse) {
        if (request.msg == "startFunc") {
            //ajaxFileUpload();
            var txtElement = document.getElementsByName("applicant_surname")[0];
            //txtElement.addEventListener('keyup', function () {

            //});
        }
    }
);

//function ajaxFileUpload() {
//    var lock = 0;
//    //var d = new Date();
//    //var n = d.getTime();
//    if (lock == 1)
//        return false;
//    lock = 1;
//    //document.getElementById("pict").innerHTML = "";
//    var j = 'H1DF';

//    var token = getRandom();
//    var form = $('<form />').attr({ method: 'post', action: '/frro/FormC/fupserv', enctype: 'multipart/form-data' }).hide().appendTo('body');

//    var fileInput = new File("D:\Projects\Smart chekcin\CardImages\IDCard-2.jpg", "filename");
//    //var fileInput = document.getElementById('file1');
//    var file = fileInput.files[0];

//    var formData = new FormData();
//    formData.append('file', file);
//    formData.append('t4g', token);

//    $.ajax({
//        url: '/frro/FormC/fupserv?t4g=' + token,
//        data: formData,
//        type: 'POST',
//        contentType: false, // NEEDED, DON'T OMIT THIS (requires jQuery 1.6+)
//        processData: false,
//        success: function (data) {
//            if (data != 'undefined') {
//                if (data == 'redirect') {
//                    window.location.href = 'sessionexpired.jsp';
//                    return false;
//                } else {
//                    alert(data);
//                    showpic();
//                }
//            }
//        },
//        error: function (jqXHR,
//            textStatus,
//            errorThrown) {
//            alert(textStatus,
//                errorThrown);
//        }
//    });
//    return false;
//}