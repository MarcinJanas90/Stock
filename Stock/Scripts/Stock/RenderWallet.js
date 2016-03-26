$.connection.hub.start().done(function () {
    if ($(document).ready()) {
        if ($('#walletTable').length) {
            Stock.server.renderWallet();
            console.log('Server function "RenderWallet" invoked');
        };
    }
});

Stock.client.renderWallet = function (accountWallet, companyCodes, unitPrices, amounts, values) {
    console.log('Client method "renderWallet" invoked');
    $.each(companyCodes, function (index, value) {
        $('#walletTable').find('tbody')
            .append(
            '<tr>' +
                '<th style="vertical-align:middle">' + companyCodes[index] + '</th>' +
                '<th style="vertical-align:middle">' + unitPrices[index] + '</th>' +
                '<th style="vertical-align:middle">' + amounts[index] + '</th>' +
                '<th style="vertical-align:middle">' + values[index] + '</th>' +
                '<th><buttnon class="btn btn-primary btn-block">Sell</buttnon></th>' +
            '</tr>'
            );
    });
    $('#walletTable').find('tbody')
        .append(
            '<tr>' +
                '<th colspan="3" style="height:50px;background-color:white"></th>' +
            '</tr>'
        );
};