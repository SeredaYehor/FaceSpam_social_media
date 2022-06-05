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
            $.ajax({
                type: "POST",
                url: "/Home/AddComment",
                async: false,
                data: { message: text, },
            });

            document.getElementById("message").value = null;

            GetMessage(text, time, userName, image);
        });
    }

    function GetTime() {
        var date = new Date();

        var hours = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
        var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes(); 
        var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate(); 
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var year = date.getFullYear().toString().substr(-2);

        return day + '.' + month + '.' + year + ' ' + hours + ':' + minutes;
    }