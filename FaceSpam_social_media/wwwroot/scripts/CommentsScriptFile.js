$(document).ready(function () {
});

function GetMessage(message, time) {
    messageObject =
        '<div style="display: flex; align-items: center; flex-direction: row">' +
        '<img src="../Images/W1ld3lf.png" class="UserImage" />' +
        '<label class="UserName"> W1ld3lf </label>' +
        '<label class="Time">' + time + '</label>' +
        '</div>' +
        '<div>' +
        '<label class="PostedMessage">' + message + '</label>' +
        '</div>';
    $("#comments").append(messageObject);
}

function AddMessage() {
    var text = document.getElementById("message").value;

    $.ajax({
        type: "GET",
        url: "/Home/AddComment",
        async: false,
        data: { message: text,},
    });

    document.getElementById("message").value = null;

    var time = GetDateTime();
    GetMessage(text, time);
}

function GetDateTime() {
    var date = new Date();
    var result = date.getDay() + '.' + date.getMonth() + '.' +
        date.getFullYear().toString().substr(2, 3) + ' ' +
        date.getHours() + ':' + date.getMinutes();
    return result;
}