$(document).ready(function () {
    $('#sellSharesForm').submit(function () {
        return confirm('Are you sure to sell these shares?');
    });
});

$(document).ready(function () {
    $('#buySharesForm').submit(function () {
        return confirm('Are you sure to buy these shares?');
    });
});