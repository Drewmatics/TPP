google.load('visualization', '1', { packages: ['AnnotationChart', 'line'] });
google.setOnLoadCallback(drawGraph);

function drawGraph() {
    $.ajax({
        url: 'History?username=' + username,
        dataType: "json",
        type: "GET",
        contentType: "application/json"
    }).done(function (historyData) {
        var data = new google.visualization.DataTable();
        data.addColumn('datetime', 'Date');
        data.addColumn('number', 'Balance');

        var rows = [];
        historyData.Balances.forEach(function (entry) {
            var date = new Date(parseInt(entry.Date.replace("/Date(", "").replace(")/", "")));
            rows.push([date, entry.Balance]);
        });
        data.addRows(rows);

        var options = {
            colors: ['#5C002E'],
            thickness: 2,
            fill: 50,
            displayRangeSelector: false,
            annotationsWidth: 50
        };

        var chart = new google.visualization.AnnotationChart(document.getElementById('chart-div'));

        chart.draw(data, options);

        historyData.MaxPayouts.forEach(function(entry) {
            $("#payouts-table").append('<tr><td>'
                + entry.Key + '</td><td>'
                + entry.Value + '</td><td>'
                + "<a href=\"http://twitchplaysleaderboard.info/results/pbr/" + entry.Key + "/\">Pic</a>"
                + '</td></tr>');
        });
    });
}
