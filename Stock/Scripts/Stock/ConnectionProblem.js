Stock.client.connectionEnabled = function () {
    console.log('Remote server connection OK. Buying and selling shares possible');
    $('#alertDiv').html('');
};

Stock.client.connectionDisabled = function () {
    console.log('Remote server connection problem. Buying and selling shares impossible');
    $('#alertDiv').html('<h3><font color="red">Remote server connection problem. Buying and selling shares impossible!</font></h3>');
};

Stock.client.renderConnectionStatus = function (isRemoteServerAvalaible) {
    if (!isRemoteServerAvalaible == true) {
        Stock.client.connectionDisabled();
    }
};