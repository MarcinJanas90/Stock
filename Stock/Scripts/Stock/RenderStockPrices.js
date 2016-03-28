$.connection.hub.start().done(function () {
    if ($(document).ready()) {
        if ($('#stockPricesTBody').length) {
            Stock.server.renderStockPrices();
            console.log('Server function "RenderStockPrices" invoked');
        };
    };
});

Stock.client.renderStockPrices = function (companyCodes, shareValues) {
    console.log('Client method "renderStockPrices" invoked');
    $.each(companyCodes, function (index, value) {
        $('#stockPricesTable').find('tbody')
            .append(
            '<tr>' +
                '<th style="vertical-align:middle">' + companyCodes[index] + '</th>' +
                '<th style="vertical-align:middle" id=value' + companyCodes[index] + '>' + shareValues[index] + '</th>' +
                '<th><a href=/Stock/BuyShares/' + companyCodes[index] + ' class="btn btn-primary btn-block" id="buySharesButton" name=buyShares' + companyCodes[index] + '>Buy</a></th>' +
            '</tr>'
            );
    });
    $('#stockPricesTable').find('tbody')
    .append(
    '<tr>' +
        '<th colspan="3" style="height:50px;background-color:white"></th>' +
    '</tr>'
    );
};