google.load('visualization', '1', { packages: ['corechart', 'line'] });
google.setOnLoadCallback(drawBasic);

function drawBasic() {
    var jsonData = $.ajax({
        url: 'History?username=' + username,
        dataType: "json",
        type: "GET",
        contentType: "application/json"
    }).done(function (historyData) {
        var data = new google.visualization.DataTable();
        data.addColumn('datetime', 'Date');
        data.addColumn('number', 'Balance');

        var rows = [];
        historyData.Balance.forEach(function (entry) {
            var date = new Date(parseInt(entry.Date.replace("/Date(", "").replace(")/", "")));
            rows.push([date, entry.Amount]);
        });
        data.addRows(rows);

        var options = {
            hAxis: {
                title: 'Date'
            },
            vAxis: {
                title: 'Balance'
            }
        };

        var chart = new google.visualization.LineChart(document.getElementById('chart_div'));

        chart.draw(data, options);
    });
}
