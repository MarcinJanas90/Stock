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
                '<th style="vertical-align:middle" id=valueWallet' + companyCodes[index] + '>' + unitPrices[index] + '</th>' +
                '<th style="vertical-align:middle" id=ammountWallet' + companyCodes[index] + '>' + amounts[index] + '</th>' +
                '<th style="vertical-align:middle" id=totalValueWallet' + companyCodes[index] + '>' + values[index] + '</th>' +
                '<th><a href=/Stock/SoldShares/' + companyCodes[index]+ ' class="btn btn-primary btn-block">Sell</a></th>' +
            '</tr>'
            );
    });
    $('#walletTable').find('tbody')
        .append(
            '<tr>' +
                '<th colspan="5" style="height:50px;background-color:white"></th>' +
            '</tr>'+
            '<tr>' +
                '<th colspan="5" style="height:100px;background-color:white;vertical-align:middle" id="avalaibleMoney"> Avalaible money: ' + accountWallet + ' PLN</th>' +
            '</tr>'
        );
};