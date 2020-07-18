//---------navbar------//

var lastscrolltop = 0;
$(document).ready(function () {
    $(window).scroll(function () {
        var scrollTop = $('html,body').scrollTop();
        if (scrollTop > lastscrolltop) {
            $('#navbar').css('top', '-80px');
        }
        else {
            $('#navbar').css('top', '0');
        }
        lastscrolltop = scrollTop;
        $(".banner").css('top', scrollTop * 0.5 + 'px');
    });


    if ($("#amounts").val() == "") {
        $("#amounts").val("1");
        $('#plus').click(function () {
            if ($("#amounts").val() == $('#amounts').data('max')) {
                alert("max cmmr ấn chi nữa");
            }
            else {
                var amountsnew = Number($("#amounts").val()) + Number(1);
                $('#amounts').val(amountsnew);
            }
        });
        $('#minus').click(function () {
            if ($("#amounts").val() == "1") {
                alert("trừ con c nữa éo có âm đâu");
            }
            else {
                var amountsnew = Number($("#amounts").val()) - Number(1);
                $("#amounts").val(amountsnew);
            }
        });
    }
});

$(function () {
    $('#click_update').click(function () {
        $('#suaform').css('visibility', 'visible');
        $('#suaform').css('opacity', '1');
        $('#suaform').css('position', 'relative');
        $('#suaform').css('transform', 'scale(1)');
        $('#click_update').attr('id', 'click_update_hide');
    });
    $('#click_update_hide').click(function () {
        $('#suaform').css('visibility', 'hidden');
        $('#suaform').css('opacity', '0');
        $('#suaform').css('position', 'absolute');
        $('#suaform').css('transform', 'scale(0)');
        $('#click_update_hide').attr('id', 'click_update');
    });
});

// search
$('#btnSearch').click(function () {
    $('#searchBg').css('visibility', 'visible');
    $('#searchBg').css('opacity', '1');
    $('.contentSearch').css('margin', '0 auto');
});
$('.closeSearch').click(function () {
    $('#searchBg').css('visibility', 'hidden');
    $('#searchBg').css('opacity', '0');
    $('.contentSearch').css('margin', '100px auto');
});
$('.closeSearch').click(function (event) {
    if (event.target == this) {
        $(this).$('#searchBg').css('display', 'none');
    }
});

// cart
$('#butt').click(function () {
    $('#question').css('visibility', 'visible');
    $('#question').css('overflow', 'hidden');
    $('#question').css('opacity', '1');
    $('.contentBuy').css('margin', '100px auto');
});
$('.close').click(function () {
    $('#question').css('visibility', 'hidden');
    $('#question').css('opacity', '0');
    $('.contentBuy').css('margin', '0 auto');
});
$('.close').click(function (event) {
    if (event.target == this) {
        $(this).$('#question').css('display', 'none');
    }
});

