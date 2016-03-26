var Stock = $.connection.StockHub;
Stock.logging = true;

$.connection.hub.start().done(function () {
    if ($(document).ready()) {
        Stock.server.establishConnection();
    }
});

Stock.client.establishConnection = function () {
    console.log("Connection to Client Established");
}