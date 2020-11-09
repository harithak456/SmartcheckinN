$(document).ready(function () {
    /* rotator frequency in seconds */
    var frequency = 5;
    var currentImg = 0;
    var imgArray = new Array();

    /*  */
    jQuery(function ($) {
        $(document).ready(function () {
            //manually load the imgArray for JSFIDDLE testing
            imgArray[0] = imgLoader("http://farm4.staticflickr.com/3791/11739073686_33d6948ab9_o.jpg");
            imgArray[1] = imgLoader("http://farm6.staticflickr.com/5500/11739073636_3fb3928d81_o.jpg");
            imgArray[2] = imgLoader("http://farm4.staticflickr.com/3707/11738319545_a842a4262e_o.jpg");
            imgArray[3] = imgLoader("http://farm4.staticflickr.com/3701/11739073596_0ff10647a0_o.jpg");
            imgArray[4] = imgLoader("http://farm3.staticflickr.com/2877/11738319475_75829b823d_o.jpg");
            imgArray[5] = imgLoader("http://farm4.staticflickr.com/3772/11738319445_ce17daef08_o.jpg");

          
            //$.ajax({
            //    type: "GET",
            //    url: "/Home/GetAdvertisement",
            //    dataType: "json",
            //    success: function (data) {
            //        $(xml).find('banner').each(function () {
            //            var id = $(this).attr('id');
            //            var img = $(this).find('img').text();
            //            var url = $(this).find('url').text();
            //            ImagesBanner1[id] = '<a href="' + url + '"><img id="banner1" src="' + img + '" /></a>'
            //        });
            //    }
            //});

        });
    });

    function imgLoader(src) {
        return '<div id="banners" class="rotator"><img id="banner1" src="' + src + '"/></div>';
    }

    function rotator() {
        $('#banners').fadeOut(500, function () {
            $('#banners').html(imgArray[currentImg]).replaceAll('#banners');
        });
        $('#banners').fadeIn(500);
        currentImg = (currentImg == imgArray.length - 1) ? 0 : currentImg + 1;
        setTimeout("rotator()", frequency * 1000);
    }

    window.onload = function () {
        rotator();
    };
});