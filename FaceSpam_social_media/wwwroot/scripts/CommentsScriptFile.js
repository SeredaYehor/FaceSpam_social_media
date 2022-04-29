$(document).ready(function () {
});
    function GetMessage(message, time, user, image) {
        messageObject =
            '<div style="display: flex; align-items: center; flex-direction: row">' +
            '<img src="' + image + '" class="UserImage" />' +
            '<label class="UserName">' + user + '</label>' +
            '<label class="Time">' + time + '</label>' +
            '</div>' +
            '<div>' +
            '<label class="PostedMessage">' + message + '</label>' +
            '</div>';
        $("#comments").append(messageObject);
    }

    function AddMessage() {
        var text = document.getElementById("message").value;
        var time = GetTime();

        var userName = "";
        var image = "";

        $.ajax({
            url: "/Home/GetUser",
        }).done(function (user) {
            userName = user["name"].toString();
            image = user["imageReference"].toString();
        });

        $.ajax({
            type: "GET",
            url: "/Home/AddComment",
            async: false,
            data: { message: text, },
        });

        document.getElementById("message").value = null;

        GetMessage(text, time, userName, image);
    }

    function GetTime() {
        var date = new Date();

        var day = date.toLocaleDateString();

        if (day.includes('/')) {
            day = day.substr(3, 5) + '.' + day.substr(0, 2) + '.' + day.substr(-2);
        }
        else {
            day = day.substr(0, 6) + day.substr(-2);
        }

        var result = day + ' ' +
            date.getHours() + ':' + date.getMinutes();

        return result;
    }