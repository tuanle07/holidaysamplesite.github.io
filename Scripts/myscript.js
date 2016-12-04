$(function () {
    $("#carousel-example-generic").carousel({
        interval: 4000,
        pause: false
    });
});

$(function () {
    $("div").find("li").children("a").each(function () {
        if ($(this).attr("href") === window.location.pathname) {
            $(this).parent().addClass("active");
        }
    });
});

$(function () {
    $.fn.visible = function (partial) {

        var $t = $(this),
            $w = $(window),
            viewTop = $w.scrollTop(),
            viewBottom = viewTop + $w.height(),
            _top = $t.offset().top,
            _bottom = _top + $t.height(),
            compareTop = partial === true ? _bottom : _top,
            compareBottom = partial === true ? _top : _bottom;

        return ((compareBottom <= viewBottom) && (compareTop >= viewTop));
    };
});

$(function () {
    var win = $(window);

    var allMods = $(".fadeInBlock");

    allMods.each(function (i, el) {
        var el = $(el);
        if (el.visible(true)) {
            el.addClass("already-visible");
        }
    });

    win.scroll(function (event) {

        allMods.each(function (i, el) {
            var el = $(el);
            if (el.visible(true)) {
                el.addClass("come-in");
            }
        });

    });
});

$(function () {
    $('#TripFrom').change(function () {
        var tripFrom = $('#TripFrom').val();
        $.ajax({
            url: '/Order/FillTo',
            type: "GET",
            dataType: "JSON",
            data: { from: tripFrom },
            error: function (error) {
                alert('error; ' + eval(error));
                alert('error; ' + error.responseText);
            },
            success: function (data) {
                $("#TripTo").html(""); // clear before appending new list 
                $("#TripTo").prepend("<option value='' selected='selected'>Select Destination</option>");
                $("#TripStartDate").html(""); // clear before appending new list 
                $("#TripStartDate").prepend("<option value='' selected='selected'>Select Trip Start Date</option>");
                $("#TripEndDate").html(""); // clear before appending new list 
                $("#TripEndDate").prepend("<option value='' selected='selected'>Select Trip End Date</option>");
                $.each(data, function (i, to) {
                    $("#TripTo").append(
                    $('<option></option>').val(to.tripTo).html(to.tripTo));
                });
            }
        });
    });
});

function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
};

$(function () {
    $('#TripTo').change(function () {
        var tripTo = $('#TripTo').val();
        var tripFrom = $('#TripFrom').val();
        $.ajax({
            url: '/Order/FillTripStartDate',
            type: "GET",
            dataType: "JSON",
            data: { from: tripFrom, to: tripTo },
            error: function (error) {
                alert('error; ' + eval(error));
                alert('error; ' + error.responseText);
            },
            success: function (data) {
                $("#TripStartDate").html(""); // clear before appending new list 
                $("#TripStartDate").prepend("<option value='' selected='selected'>Select Trip Start Date</option>");
                $("#TripEndDate").html(""); // clear before appending new list 
                $("#TripEndDate").prepend("<option value='' selected='selected'>Select Trip End Date</option>");
                $.each(data, function (i, startdate) {
                    $("#TripStartDate").append(
                    $('<option></option>').val(ToJavaScriptDate(startdate.tripStartDate)).html(ToJavaScriptDate(startdate.tripStartDate)));
                });
            }
        });
    });
});

$(function () {
    $('#TripStartDate').change(function () {
        var tripTo = $('#TripTo').val();
        var tripFrom = $('#TripFrom').val();
        var tripStartDate = $('#TripStartDate').val();
        $.ajax({
            url: '/Order/FillTripEndDate',
            type: "GET",
            dataType: "JSON",
            data: { from: tripFrom, to: tripTo, startdate: tripStartDate },
            error: function (error) {
                alert('error; ' + eval(error));
                alert('error; ' + error.responseText);
            },
            success: function (data) {
                $("#TripEndDate").html(""); // clear before appending new list 
                $("#TripEndDate").prepend("<option value='' selected='selected'>Select Trip End Date</option>");
                $.each(data, function (i, startdate) {
                    $("#TripEndDate").append(
                    $('<option></option>').val(startdate.tripId).html(ToJavaScriptDate(startdate.tripEndDate)));
                });
            }
        });
    });
});

$(function () {
    $("#btn-save").click(function (e) {
        e.preventDefault(); // Stops the form automatically submitting
        if ($('form').valid()) {
            $('form').submit();
        }
    })
});

$(function () {
    $('.table').addClass('table-striped');
})

$(function () {
    $('.table').addClass('table-hover');
})

$(function () {
    $('.table').dataTable({
        "bSort": true,
        "sDom": '<"row"<"col-sm-12"<"pull-left"l><"pull-right"i><"clearfix">>><"table-responsive"t><"row"<"col-sm-12"<"text-center"p>>>'
    });
})

$(function () {
    $('.table thead tr th').click(function () {
        if ($(this).find('i').hasClass('fa fa-sort')) {
            $(this).find('i').removeClass('fa fa-sort');
            $(this).find('i').addClass('fa fa-sort-amount-asc');
        }
        else {
            $(this).find('i').toggleClass('fa fa-sort-amount-asc fa fa-sort-amount-desc');
        }
        $('.table thead tr th').not(this).find('i').removeClass();
        $('.table thead tr th').not(this).find('i').addClass('fa fa-sort');
    })
})



$(function () {
    $('#txtDOB').datepicker({ dateFormat: 'dd/mm/yy' });
    $('#txttripstartdate').datepicker({ dateFormat: 'dd/mm/yy' });
    $('#txttripenddate').datepicker({ dateFormat: 'dd/mm/yy' });
    $('#searchStartDate').datepicker({ dateFormat: 'dd/mm/yy' });
})

//$(function () {
//    $('.dropdown').hover(
//            function () {
//                $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeIn();
//            },
//            function () {
//                $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeOut();
//            }
//    )
//})

//$(function () {
//    $('.dropdown-menu').hover(
//        function () {
//            $(this).stop(true, true);
//        },
//        function () {
//            $(this).stop(true, true).delay(200).fadeOut();
//        }
//    )
//})
