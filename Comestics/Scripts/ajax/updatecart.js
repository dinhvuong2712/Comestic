function plus_click(idpr) {
    var plus = '#plus-' + idpr;
    var amounts = '#amounts-' + idpr;
    var price = '#price-' + idpr;
    var intoModey = '#intoMoney-' + idpr;
    var tota_amounts = $('#total_amounts').html();
    var tota_price = $('#total_price').html();
    if ($(amounts).val() == $(amounts).attr('data-max')) {
        alert("max cmmr ấn chi nữa");
        $(plus).attr('disabled', 'disabled');
    }
    else {
        var amountsnew = Number($(amounts).val()) + Number(1);
        $(amounts).val(amountsnew);
        var intoMoneyNew = Number($(intoModey).html()) + Number($(price).html());
        $(intoModey).html(intoMoneyNew);
        var tota_amountsnew = Number(tota_amounts) + Number(1);
        $('#total_amounts').html(tota_amountsnew);
        var total_pricenew = Number(tota_price) + Number($(price).html());
        $('#total_price').html(total_pricenew);
    }
};
function minus_click(idpr) {
    var minus = '#minus-' + idpr;
    var amounts = '#amounts-' + idpr;
    var price = '#price-' + idpr;
    var intoModey = '#intoMoney-' + idpr;
    var tota_amounts = $('#total_amounts').html();
    var tota_price = $('#total_price').html();
    if ($(amounts).val() == "1") {
        alert("trừ con c nữa éo có âm đâu");
        $(minus).attr('disabled', 'disabled');
    }
    else {
        var amountsnew = Number($(amounts).val()) - Number(1);
        $(amounts).val(amountsnew);
        var intoMoneyNew = Number($(intoModey).html()) - Number($(price).html());
        $(intoModey).html(intoMoneyNew);
        var tota_amountsnew = Number(tota_amounts) - Number(1);
        $('#total_amounts').html(tota_amountsnew);
        var total_pricenew = Number(tota_price) - Number($(price).html());
        $('#total_price').html(total_pricenew);
    }
}

$(function () {
    $("#update_all").click(function () {
        $(document).ready(function () {
           // var UpdateListModel = {};
            var objects = [];
            
            $('#form_update').each(function () {
                var i = 0;
                var idpr = $(this).find('.id_cart').val();
                var amountspr = $(this).find('.amounts_cart').val();
                objects[i] = { id: idpr, amounts: amountspr };
                i++;
            });
            objects = JSON.stringify({ 'objects': objects });
            $.ajax({
                url: "Cart/UpdateCart",
                type: "POST",
                data: objects ,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    alert(data);
                }
            });
        });
    });
});