$(document).ready(function() {
	//Date Range Picker
    $('#daterange').daterangepicker({
            startDate: moment().subtract('days', 29),
            endDate: moment(),
            minDate: '01/01/2012',
            maxDate: '12/31/2014',
            dateLimit: {
                days: 60
            },
            showDropdowns: true,
            showWeekNumbers: true,
            timePicker: false,
            timePickerIncrement: 1,
            timePicker12Hour: true,
            ranges: {
                'Today': [moment(), moment()],
                'Yesterday': [moment().subtract('days', 1), moment().subtract('days', 1)],
                'Last 7 Days': [moment().subtract('days', 6), moment()],
                'Last 30 Days': [moment().subtract('days', 29), moment()],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract('month', 1).startOf('month'), moment().subtract('month', 1).endOf('month')]
            },
            opens: 'left',
            buttonClasses: ['btn btn-primary'],
            applyClass: 'btn-sm btn-inverse',
            cancelClass: 'btn-sm',
            format: 'MM/DD/YYYY',
            separator: ' to ',
            locale: {
                applyLabel: 'Submit',
                fromLabel: 'From',
                toLabel: 'To',
                customRangeLabel: 'Custom Range',
                daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                firstDay: 1
            }
        },
        function(start, end) {
            console.log("Callback has been called!");
            $('#daterange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
        }
    );
    //Set the initial state of the picker label
    $('#daterange span').html(moment().subtract('days', 29).format('MMMM D, YYYY') + ' - ' + moment().format('MMMM D, YYYY'));
	

	
	//// Morris chart examples.
 //   var d = new Date();   
 //   n = d.getMonth() +1;
 //   y = d.getFullYear();

 //   const today = new Date()
 //   const yesterday  = new Date(today)  
 //   var WalkingCount = $('#WalkingCount').val();
 //   var splitW = WalkingCount.split(',');

 //   var BookingCount = $('#BookingCount').val();
 //   var splitB = BookingCount.split(',');


	//var day_data = [
	//	//{"elapsed": "1AM", "a": 0, "b": 3},  //modified by Haritha for admin dashboard on 17-06/2020
	//	//{"elapsed": "2AM", "a": 24, "b": 26},
	//	//{"elapsed": "7AM", "a": 3, "b": 5},
	//	//{"elapsed": "9AM", "a": 12, "b": 8},
	//	//{"elapsed": "11AM", "a": 13, "b": 17},
	//	//{"elapsed": "12AM", "a": 22, "b": 25},
	//	//{"elapsed": "9PM", "a": 5, "b": 7}

 //       //monthly
 //       //{ "elapsed": "1/"+ n, "a": 0, "b": 3 },
 //       //{ "elapsed": "5/" + n , "a": 24, "b": 26 },
 //       //{ "elapsed": "10/" + n , "a": 3, "b": 5 },
 //       //{ "elapsed": "15/" + n , "a": 12, "b": 8 },
 //       //{ "elapsed": "20/" + n , "a": 13, "b": 17 },
 //       //{ "elapsed": "25/" + n, "a": 22, "b": 25 },
 //       //{ "elapsed": "30/" + n , "a": 5, "b": 7 }

 //       //last 7 days
 //       { "elapsed": yesterday.getDate() - 7 + "/" + n, "a": splitB[6], "b": splitW[6] },
 //       { "elapsed": yesterday.getDate() - 6 + "/" + n, "a": splitB[5], "b": splitW[5] },
 //       { "elapsed": yesterday.getDate() - 5 + "/" + n, "a": splitB[4], "b": splitW[4] },
 //       { "elapsed": yesterday.getDate() - 4 + "/" + n, "a": splitB[3], "b": splitW[3] },
 //       { "elapsed": yesterday.getDate() - 3 + "/" + n, "a": splitB[2], "b": splitW[2] },
 //       { "elapsed": yesterday.getDate() - 2 + "/" + n, "a": splitB[1], "b": splitW[1] },
 //       { "elapsed": yesterday.getDate() - 1 + "/" + n, "a": splitB[0], "b": splitW[0] }
 //   ];

 //   new Morris.Line({
 //       element: 'morris-chart-1',
 //       data: day_data,
 //       xkey: 'elapsed',
 //       ykeys: ['a', 'b'],
 //       yLabelFormat: function (y) {          
 //           return y;
 //       },
 //       labels: ['Node 1', 'Node 2'],
 //       parseTime: false,
 //       lineColors: ['#72af46', '#466baf'],
 //       gridTextColor: ['#686868'],
 //       lineWidth: ['2px'],
 //       grid: true,
 //       smooth: true,
 //       resize: true,
 //       hideHover: 'auto'
 //   });

	//new Morris.Line({
	//element: 'morris-chart-1',
	//	data: day_data,
	//	xkey: 'elapsed',
	//	ykeys: ['a', 'b'],
	//		yLabelFormat: function(y) {
	//		return y + " %";
	//	},
	//	labels: ['Node 1', 'Node 2'],
	//	parseTime: false,
	//	lineColors: ['#72af46', '#466baf'],
	//	gridTextColor: ['#686868'],
	//	lineWidth: ['2px'],
	//	grid: true,
	//	smooth: true,
	//	resize: true,
	//	hideHover: 'auto'
	//});


	//For users List demo at dashboard
	$(function() {    
        $('#input-quicklist').on('keyup', function() {
          var rex = new RegExp($(this).val(), 'i');
            $('.quick-list .items').hide();
            $('.quick-list .items').filter(function() {
                return rex.test($(this).text());
            }).show();
        });
    });
	
	// tasks-widget sortable.
	$('#todo-sortlist').sortable({
		opacity:0.8,
		revert:true,
		forceHelperSize:true,
		axis: 'y',
		placeholder: 'draggable-placeholder',
		forcePlaceholderSize:true,
		stop: function( event, ui ) {//just for Chrome!!!! so that dropdowns on items don't appear below other items after being moved
			$(ui.item).css('z-index', 'auto');
			}
		}
	);
	
	//slimScroll Function for user's list
	//--------------------------------
	$(function () {
		$('.quick-list').slimScroll({
			height: '120px'
		});
	});
	
	//slimScroll Function for To do portlet 
	//--------------------------------
	$(function () {
		$('.task-widget').slimScroll({
			height: '95px',
			alwaysVisible: false,
			disableFadeOut: true,
			touchScrollStep: 50
		});
	});
	
	//slimScroll Function for Live chat
	//--------------------------------
	$(function () {
		$('.chat-history').slimScroll({
			height: '175px',
			touchScrollStep: 50
		});
	});
	
	//slimScroll Function for Internal chat 
	//--------------------------------
	$(function () {
		$('.in-chat').slimScroll({
			height: '195px',
			alwaysVisible: false,
			disableFadeOut: true,
			touchScrollStep: 50
		});
	});
	
	//gritter only for demo
    $(window).load(function () {
		//$.gritter.add({
  //          title: "Welcome back, John!",
  //          text: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  //          image: "../../assets/images/user-profile-1.jpg",
		//	class_name: "bg-success",
  //          sticky: false
		//})
	})
	//jvectormap data
    var visitorsData = {
        "US": 398, //USA
        //"SA": 400, //Saudi Arabia
        //"CA": 1000, //Canada
        //"DE": 500, //Germany
        //"FR": 760, //France
        //"CN": 300, //China
        //"AU": 700, //Australia
        //"BR": 600, //Brazil
        "IN": 800, //India
    };
    //visitors map by jvectormap
    $('#visitors-map').vectorMap({
        map: 'world_mill_en',
        backgroundColor: "#fff",
        regionStyle: {
            initial: {
                fill: '#797979',
                "fill-opacity": 1,
                stroke: 'none',
                "stroke-width": 0,
                "stroke-opacity": 1
            }
        },
        series: {
            regions: [{
                    values: visitorsData,
                    scale: ["#373737", "#466baf"],
                    normalizeFunction: 'polynomial'
                }]
        },
        onRegionLabelShow: function(e, el, code) {
            if (typeof visitorsData[code] != "undefined")
                el.html(el.html() + ': ' + visitorsData[code] + ' new visitors');
        }
    });	
});