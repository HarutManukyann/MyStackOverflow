// JavaScript source code
$(document).ready(function () {
    $("#up").click(function () {
        var number = parseInt($("#counter").text())
        number++
        $("#counter").text(number);
    });

    $("#dwn").click(function () {
        var number = parseInt($("#counter").text())
        number--
        $("#counter").text(number);
    });

    $.get("https://app-interns.azurewebsites.net/api/StackOverflow", function (data) {
        $("#getTitle").text(data.title)
        $("#getText").text(data.text)
        $("#getId").text("Question ID: " + data.id)
        $("#ansId").text("Answer for Question ID:" + data.id)
    });
    $.get("https://app-interns.azurewebsites.net/api/StackOverflow/7207", function (data) {
        $.each(data.answers, function (key, value) {
            $("#td"+key).text(value)
        });
    });

    $("#post").click(function(){
        if ($("#userName").val() == 0 && $("#text").val() == 0)
        {
            alert("Error");
        }
        else
        {
            
                $("#answers").append('<tr><td>' +"User Name:"+ $("#userName").val() + '</tr></td>')
                $("#answers").append('<tr><td>' + $("#text").val() + '</tr></td>')
        }
    });

});