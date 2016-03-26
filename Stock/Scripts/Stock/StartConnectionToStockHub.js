var Stock = $.connection.StockHub;
Stock.logging = true;

$.connection.hub.start().done(function () {
    console.log("Connection Started");
    if ($(document).ready()) {
        Stock.server.establishConnection();
    }
});

Stock.client.EstablishConnection = function () {
    console.log("Connection to Client Established");
}