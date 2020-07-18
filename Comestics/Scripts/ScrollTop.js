$(document).ready(function () {
    $(window).scroll(function () {
        /*scrolltoTop*/
        if ($(this).scrollTop() > 100) {
            $('#goTop').css("visibility", "visible");
            $('#goTop').css("opacity", ".6");
            $('#goTop').css("right", "0");
        } else {
            $('#goTop').css("visibility", "hidden");
            $('#goTop').css("opacity", "0");
            $('#goTop').css("right", "-40px");
        }
        $('#goTop').click(function () {
            $('html, body').scrollTop("0px");
        });
    });
});