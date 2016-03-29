Stock.client.updateWalletValues = function () {
    console.log('Begun updating wallet values');
    $('th[id^="totalValueWallet"]').each(function (index, value) {
        $('th[id^="totalValueWallet"]').eq(index).empty();
        $('th[id^="totalValueWallet"]').eq(index).append(($('th[id^="valueWallet"]').eq(index).text() * $('th[id^="ammountWallet"]').eq(index).text()).toFixed(4));
        $('th[id^="totalValueWallet "]').eq(index).empty();
        $('th[id^="totalValueWallet "]').eq(index).append(($('th[id^="valueWallet "]').eq(index).text() * $('th[id^="ammountWallet "]').eq(index).text()).toFixed(4));
    });
    console.log('Ended updating wallet values');
};