Stock.client.updateStockPrices = function (companyCodes, shareValues) {
    console.log('Begin updating stock values');
    $.each(companyCodes, function (index, value) {
        $('#value' + value).empty();
        $('#value' + value).append(shareValues[index]);
        $('#valueWallet' + value).empty();
        $('#valueWallet' + value).append(shareValues[index]);
    });
    console.log('Ended updating stock values');
};