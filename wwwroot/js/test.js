$(document).ready(function () {
    $('.lang').on('click', function () {
        console.log("teeest");
        const lang = $(this).data('lang');
        $.ajax({
            url: '/Home/ChangeLanguage',
            type: 'POST',
            data: { lang: lang },
            success: function (result) {
                location.reload();
            }
        });
    });
    $('.currency').on('click', function () {
        const symbol = $(this).data('symbol');
        const multiplier = $(this).data('multiplier');
        $.ajax({
            url: '/Home/ChangeCurrency',
            type: 'POST',
            data: { symbol: symbol,multiplier:multiplier },
            success: function (result) {
                location.reload();
            }
        });
    });
});