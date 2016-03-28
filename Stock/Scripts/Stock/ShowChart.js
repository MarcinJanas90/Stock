$.connection.hub.start().done(function () {
    $(document).ready(function () {
        $('#showHideChart').click(function () {
            console.log('Show/Hide Chart clicked');
            if ($('#myChart').length) {
                $('#chartCell').remove();
            }
            else {
                Stock.server.showChart();
            }
        });
    });
});


Stock.client.showChart = function (dataJson, namesJson) {
    console.log('Started chart rendering');
    $('#stockPricesTable').find('tbody')
    .append(
        '<tr>' +
            '<th colspan="3" id="chartCell"></th>' +
        '</tr>'
    );

    $('#chartCell').append('<canvas id="myChart" width="500" height="auto"></canvas>');
    var dataArray = $.parseJSON(dataJson);
    var namesArray = $.parseJSON(namesJson);
    var labels = [];
    var datasetValue = [];
    var count = dataArray.length;

    var randomColorGenerator = function () {
        return '#' + (Math.random().toString(16) + '0000000').slice(2, 8);
    };

    for (var i = dataArray[0].length; i > 0; i--) {
        labels[dataArray[0].length - i] = i;
    }

    for (var i = 0; i < count; i++) {
        var color = randomColorGenerator();
        datasetValue[i] = {
            label: namesArray[i],
            strokeColor: color,
            pointColor: color,
            data: dataArray[i]
        }
    }

    var mydata = {
        labels: labels,
        datasets: datasetValue
    }

    var ctx = $("#myChart").get(0).getContext("2d");
    new Chart(ctx).Line(mydata, { bezierCurve: false, datasetFill: false, scaleShowLabels: true, multiTooltipTemplate: "<%= datasetLabel %> - <%= value %>" });
    console.log('Ended chart rendering');
};